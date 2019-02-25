namespace RabbitMQ.Core.Model
{
    /// <summary>
    /// 链接服务器的配置项
    /// </summary>
    public class RabbitConfigModel
    {
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 服务器端口，默认是 5672
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// 路由的类型枚举 当type = "fanout"时不需要指定routing key，设置了也没有用.
        /// </summary>
        public ExchangeTypeEnum ExchangeType { get; set; }

        /// <summary>
        /// 路由的关键字
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 虚拟主机名称
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 是否持久化该队列
        /// </summary>
        public bool DurableQueue { get; set; }

        /// <summary>
        /// 是否持久化队列中的消息
        /// </summary>
        public bool DurableMessage { get; set; }

        /// <summary>
        /// 管理后台链接地址
        /// </summary>
        public string HostUrl => $"http://{IP}:15672";

        /// <summary>
        /// 查询Exchanges信息接口
        /// </summary>
        public string ExchangesApi => HostUrl + "/api/exchanges";

        /// <summary>
        /// 查询queues信息接口
        /// </summary>
        public string QueuesApi => HostUrl + "/api/queues";

        /// <summary>
        /// 查询Bingdings信息接口
        /// </summary>
        public string BingdingsApi => HostUrl + "/api/bindings";

    }
}
