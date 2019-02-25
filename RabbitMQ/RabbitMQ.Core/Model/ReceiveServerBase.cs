using RabbitMQ.Client;
using RabbitMQ.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Model
{
    public abstract class ReceiveServerBase
    {
        /// <summary>
        /// 服务器配置
        /// </summary>
        public RabbitConfigModel RabbitConfig { get; set; }

        public static IConnection Connection;

        public ReceiveServerBase(RabbitConfigModel config)
        {
            this.RabbitConfig = config;
        }

        /// <summary>
        /// 获取队列服务器的链接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            if (Connection == null)
                Connection = RabbitBaseService.GetConnection(this.RabbitConfig.IP, this.RabbitConfig.Port, this.RabbitConfig.UserName, this.RabbitConfig.Password, this.RabbitConfig.VirtualHost, 60);

            return Connection;
        }

    }
}
