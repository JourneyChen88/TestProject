using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace RabbitMQ.Core.Service
{
    /// <summary>
    /// 声明处理接受信息的委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    public delegate void ReceiveMessageDelegate<T>(T value);

    /// <summary>
    ///  接受消息的服务 
    /// </summary>
    public class RabbitReceiveMessageService : ReceiveServerBase
    {

        public RabbitReceiveMessageService(RabbitConfigModel config) : base(config)
        {

        }

        #region 接受消息，使用委托进行处理
        /// <summary>
        /// 接受消息，使用委托进行处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiveMethod"></param>
        public void Receive<T>(ReceiveMessageDelegate<T> receiveMethod)
        {
            try
            {
                using (var channel = this.GetConnection().CreateModel())
                {
                    //是否使用路由
                    if (!string.IsNullOrWhiteSpace(this.RabbitConfig.Exchange))
                    {
                        //声明路由
                        channel.ExchangeDeclare(this.RabbitConfig.Exchange, this.RabbitConfig.ExchangeType.ToString(), this.RabbitConfig.DurableQueue);

                        //声明队列且与交换机绑定
                        channel.QueueDeclare(this.RabbitConfig.QueueName, this.RabbitConfig.DurableQueue, false, false, null);
                        channel.QueueBind(this.RabbitConfig.QueueName, this.RabbitConfig.Exchange, this.RabbitConfig.RoutingKey);
                    }
                    else
                        channel.QueueDeclare(this.RabbitConfig.QueueName, this.RabbitConfig.DurableQueue, false, false, null);

                    //输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                    channel.BasicQos(0, 1, false);  ///告诉RabbitMQ同一时间给一个消息给消费者  
                    //在队列上定义一个消费者
                    var consumer = new QueueingBasicConsumer(channel);
                    //消费队列，并设置应答模式为程序主动应答
                    channel.BasicConsume(this.RabbitConfig.QueueName, false, consumer);

                    while (true)
                    {
                        //阻塞函数，获取队列中的消息
                        ProcessingResultsEnum processingResult = ProcessingResultsEnum.Retry;
                        ulong deliveryTag = 0;
                        try
                        {
                            Thread.Sleep(500);//暂停0.5秒，防止CPU爆满的问题

                            //获取信息
                            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            deliveryTag = ea.DeliveryTag;
                            byte[] bytes = ea.Body;
                            string str = Encoding.UTF8.GetString(bytes);
                            T v = JsonConvert.DeserializeObject<T>(str);
                            receiveMethod(v);

                            processingResult = ProcessingResultsEnum.Accept; //处理成功
                        }
                        catch (Exception)
                        {
                            processingResult = ProcessingResultsEnum.Reject; //系统无法处理的错误
                        }
                        finally
                        {
                            switch (processingResult)
                            {
                                case ProcessingResultsEnum.Accept:
                                    //回复确认处理成功
                                    channel.BasicAck(deliveryTag, false);
                                    break;
                                case ProcessingResultsEnum.Retry:
                                    //发生错误了，但是还可以重新提交给队列重新分配
                                    channel.BasicNack(deliveryTag, false, true);
                                    break;
                                case ProcessingResultsEnum.Reject:
                                    //发生严重错误，无法继续进行，这种情况应该写日志或者是发送消息通知管理员
                                    channel.BasicNack(deliveryTag, false, false);

                                    //写日志
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region 获取单条消息
        /// <summary>
        /// 获取单条消息
        /// </summary>
        /// <param name="basicGetMethod">回调函数</param>
        /// <param name="data">传过来，再传回去</param>
        public void BasicGet(ReceiveMessageDelegate<ValueTuple<bool, string, Dictionary<string, object>>> basicGetMethod, Dictionary<string, object> data=null)
        {
            try
            {
                using (var channel = this.GetConnection().CreateModel())
                {
                    BasicGetResult res = channel.BasicGet(RabbitConfig.QueueName, false);
                    if (res != null)
                    {
                        //普通使用方式BasicGet
                        //noAck = true，不需要回复，接收到消息后，queue上的消息就会清除
                        //noAck = false，需要回复，接收到消息后，queue上的消息不会被清除，直到调用channel.basicAck(deliveryTag, false); queue上的消息才会被清除 而且，在当前连接断开以前，其它客户端将不能收到此queue上的消息

                        IBasicProperties props = res.BasicProperties;
                        bool t = res.Redelivered;
                        t = true;
                        string result = Encoding.UTF8.GetString(res.Body);
                        channel.BasicAck(res.DeliveryTag, false);
                        basicGetMethod(new ValueTuple<bool, string, Dictionary<string, object>>(true, result, data));                
                    }
                    else
                    {
                        basicGetMethod(new ValueTuple<bool, string, Dictionary<string, object>>(false, "未找到所需数据", data));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
