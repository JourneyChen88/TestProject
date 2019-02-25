using RabbitMQ.Client;
using System;

namespace RabbitMQ.Core.Service
{
    /// <summary>
    /// RabbitMQ基础服务
    /// </summary>
    public class RabbitBaseService
    {

        /// <summary>
        /// 获取队列服务器的连接对象
        /// </summary>
        /// <param name="ip">服务器ip</param>
        ///  <param name="port">服务器的端口</param>
        /// <param name="userName">登录账户</param>
        /// <param name="password">登录密码</param>
        /// <param name="virtualHost">虚拟主机</param>
        /// <param name="heartbeat">心跳检测时间</param>
        /// <returns></returns>
        public static IConnection GetConnection(string ip,int port, string userName, string password, string virtualHost, ushort heartbeat)
        {
            try
            {
                ConnectionFactory cf = new ConnectionFactory()
                {
                    Port = port,
                    Endpoint = new AmqpTcpEndpoint(new Uri("amqp://" + ip + "/")),
                    UserName = userName,
                    Password = password,
                    VirtualHost = virtualHost,
                    RequestedHeartbeat = heartbeat
                };
                return cf.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
