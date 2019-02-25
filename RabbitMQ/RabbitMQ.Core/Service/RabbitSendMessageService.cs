using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Core.Model;
using System;
using System.Text;

namespace RabbitMQ.Core.Service
{
    /// <summary>
    /// 发送消息的服务 
    /// </summary>
    public class RabbitSendMessageService : ReceiveServerBase
    {
        public RabbitSendMessageService(RabbitSendConfigModel config) : base(config)
        {

        }

        /// <summary>
        /// 发送消息，泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send<T>(T message)
        {
            string value = JsonConvert.SerializeObject(message);
            return this.Send(value);
        }

        #region 发送消息给队列服务器

        /// <summary>
        /// 发送消息给队列服务器
        /// </summary>
        /// <returns></returns>
        public bool Send(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return false;

            try
            {
                using (var channel = this.GetConnection().CreateModel())
                {
                    //推送消息
                    byte[] bytes = Encoding.UTF8.GetBytes(message);

                    //声明一个交换机和队列，然后绑定在一起。 
                    //if (!string.IsNullOrWhiteSpace(this.RabbitConfig.Exchange))
                    //    //使用自定义的路由
                    //    channel.ExchangeDeclare(this.RabbitConfig.Exchange, this.RabbitConfig.ExchangeType.ToString(), false, false, null);
                    ////channel.ExchangeDeclare(this.RabbitConfig.Exchange, this.RabbitConfig.ExchangeType);
                    //else
                    //    //声明消息队列，且为可持久化的  ，如果队列的名称不存在，系统会自动创建，有的话不会覆盖
                    //    channel.QueueDeclare(this.RabbitConfig.QueueName, this.RabbitConfig.DurableQueue, false, false, null);

                    channel.ExchangeDeclare(this.RabbitConfig.Exchange, this.RabbitConfig.ExchangeType.ToString(), false, false, null);
                    channel.QueueDeclare(this.RabbitConfig.QueueName, this.RabbitConfig.DurableQueue, false, false, null);

                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = Convert.ToByte(this.RabbitConfig.DurableMessage ? 2 : 1); //支持持久化数据   
                    channel.QueueBind(this.RabbitConfig.QueueName, RabbitConfig.Exchange, RabbitConfig.RoutingKey);

                    //将详细写入队列
                    if (string.IsNullOrEmpty(this.RabbitConfig.Exchange))
                        //没有配置路由，使用系统默认的路由
                        //推送消息
                        channel.BasicPublish("", this.RabbitConfig.QueueName, properties, bytes);
                    else
                        //推送消息
                        channel.BasicPublish(this.RabbitConfig.Exchange, this.RabbitConfig.RoutingKey, properties, bytes);

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
