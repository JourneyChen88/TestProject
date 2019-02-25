using Newtonsoft.Json;
using RabbitMQ.Core.Model;
using RabbitMQ.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQ.WindowsFormsApp
{
    public partial class FmMain : Form
    {
        #region 变量

        private const string IP = "localhost";
        private const string UserName = "guest";
        private const string Password = "guest";
        private const int Port = 5672;

        static RabbitSendConfigModel RabbitSendConfig = new RabbitSendConfigModel
        {
            IP = IP,
            UserName = UserName,
            Password = Password,
            Port = Port,
            VirtualHost = "/",
            DurableQueue = true,
            QueueName = "Prize",
            Exchange = "PrizeTest",
            ExchangeType = ExchangeTypeEnum.direct,
            DurableMessage = true,
            RoutingKey = "Prize"
        };
        static RabbitReceiveConfigModel RabbitReceiveConfig = new RabbitReceiveConfigModel
        {
            IP = IP,
            UserName = UserName,
            Password = Password,
            Port = Port,
            VirtualHost = "/",
            DurableQueue = true,
            QueueName = "Prize",
            Exchange = "PrizeTest",
            ExchangeType = ExchangeTypeEnum.direct,
            DurableMessage = true,
            RoutingKey = "Prize"
        };

        /// <summary>
        /// 发送者
        /// </summary>
        private static RabbitSendMessageService RabbitSend = new RabbitSendMessageService(RabbitSendConfig);

        /// <summary>
        /// 接收者
        /// </summary>
        private static RabbitReceiveMessageService RabbitReceive = new RabbitReceiveMessageService(RabbitReceiveConfig);

        /// <summary>
        /// 用户池(还未开始抽奖)
        /// </summary>
        private const string UserQueueName = "User";

        /// <summary>
        /// 奖品池信息
        /// </summary>
        private const string PrizeQueueName = "Prize";

        /// <summary>
        /// 中奖人
        /// </summary>
        private const string PrizeUserQueueName = "Prize_User";

        /// <summary>
        /// 奖品总数
        /// </summary>
        private const int PrizeCount = 300;
        private List<ExchangeEntity> userExchanges;
        private List<QueueEntity> queues;
        private List<BindingEntity> bindings;

        /// <summary>
        /// 无参构造
        /// </summary>
        public FmMain()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);   // 打开控件的双缓冲
            InitializeComponent();
        }
        #endregion

        #region 投放奖品
        /// <summary>
        /// 投放奖品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                SetSendfigModel(PrizeQueueName);  //设置队列信息(奖品池)
                new Thread(SetPrize) { IsBackground = true }.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错了", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetPrize()
        {
            string value = string.Empty;
            for (int i = 1; i <= PrizeCount; i++)
            {
                PrizeInfo prize = new PrizeInfo
                {
                    Id = i,
                    Name = "我是奖品" + i,
                    Type = 1,
                    PrizeNo = DateTime.Now.ToString("hhmmssfff"),
                    Total = PrizeCount,
                    Balance = PrizeCount
                };
                value = JsonConvert.SerializeObject(prize);
                RabbitSend.Send(prize);
                ShowSysMessage($"我骄傲，我是奖品：{i}/{PrizeCount}");
            }
            ShowSysMessage("奖品投放完成");
        }
        #endregion

        #region 模拟多用户页面请求
        /// <summary>
        /// 模拟多用户页面请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                SetSendfigModel(UserQueueName);  //设置队列信息(用户池)
                ShowSysMessage("开始模拟多用户页面请求...");
                new Thread(ThreadFunction) { IsBackground = true }.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错了", MessageBoxButtons.OK);
            }
        }

        private const int threadLength = 8;
        private static CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        /// 
        /// </summary>
        private void ThreadFunction()
        {
            cts = new CancellationTokenSource();
            TaskFactory taskFactory = new TaskFactory();
            Task[] tasks = new Task[threadLength];

            for (int i = 0; i < threadLength; i++)
            {
                Task t1 = Task.Factory.StartNew(delegate { ParallelFunction(cts.Token); });
                tasks.SetValue(t1, i);
            }
            taskFactory.ContinueWhenAll(tasks, TasksEnded, CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        void TasksEnded(Task[] tasks)
        {
            ShowSysMessage("所有任务已完成/或已取消！");
        }

        /// <summary>
        /// 
        /// </summary>
        private void ParallelFunction(CancellationToken ct)
        {
            Parallel.For(0, 1000, item =>
            {
                if (!ct.IsCancellationRequested)
                {
                    string value = string.Empty;
                    UsersInfo user = new UsersInfo
                    {
                        Id = item,
                        Name = "我是：" + item
                    };
                    value = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    ShowSysMessage($"进来了一位用户：{value}");
                    RabbitSend.Send(user);
                }
            });
        }
        #endregion

        #region 停止所有用户访问
        /// <summary>
        /// 停止所有用户访问
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
        #endregion

        #region 设置队列信息

        private static object SetReceivefigModelObj = new object();
        private static object SetSendfigModelObj = new object();
        /// <summary>
        /// 设置队列信息(Receive)
        /// </summary>
        /// <param name="queueName"></param>
        private void SetReceivefigModel(string queueName)
        {
            lock (SetReceivefigModelObj)
            {
                RabbitReceiveConfig.QueueName = queueName;
                RabbitReceiveConfig.RoutingKey = queueName;
                RabbitReceive = new RabbitReceiveMessageService(RabbitReceiveConfig);
            }
        }

        /// <summary>
        /// 设置队列信息(Send)
        /// </summary>
        /// <param name="queueName"></param>
        private void SetSendfigModel(string queueName)
        {
            lock (SetSendfigModelObj)
            {
                RabbitSendConfig.QueueName = queueName;
                RabbitSendConfig.RoutingKey = queueName;
                RabbitSend = new RabbitSendMessageService(RabbitSendConfig);
            }
        }
        #endregion

        #region 模拟多用户抽奖

        /// <summary>
        /// 模拟多用户抽奖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            //1:先去用户池中取出一个人 2 拿用户去抽一个奖品 3:将中奖人塞入中奖队列
            new Thread(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    SetReceivefigModel(UserQueueName);//设置队列信息(用户池)  
                    RabbitReceive.BasicGet(LockUser);
                }

                //Parallel.For(0, 200000, item =>
                //{
                //    RabbitReceive.BasicGet(LockUser);
                //});
            })
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// 先去用户池中取出一个人
        /// </summary>
        /// <param name="tp"></param>
        private void LockUser(ValueTuple<bool, string, Dictionary<string, object>> tp)
        {
            try
            {
                if (tp.Item1)
                {
                    ShowSysMessage($"锁定到一个用户：{tp.Item2}");
                    UsersInfo user = JsonConvert.DeserializeObject<UsersInfo>(tp.Item2);
                    if (null != user)
                    {
                        Thread.Sleep(50);
                        LockPrize(user);//拿用户去抽一个奖品
                    }
                }
                else
                {
                    ShowSysMessage(tp.Item2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错了", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 拿用户去抽一个奖品
        /// </summary>
        /// <param name="user"></param>
        private void LockPrize(UsersInfo user)
        {
            SetReceivefigModel(PrizeQueueName);//设置队列信息(奖品池)
            Dictionary<string, object> data = new Dictionary<string, object> { { "User", user } };
            RabbitReceive.BasicGet(LockPrize, data);
        }

        /// <summary>
        /// 锁定奖品
        /// </summary>
        /// <param name="value"></param>
        private void LockPrize(ValueTuple<bool, string, Dictionary<string, object>> tp)
        {
            try
            {
                if (tp.Item1)
                {
                    UsersInfo user = tp.Item3["User"] as UsersInfo;
                    PrizeInfo prize = JsonConvert.DeserializeObject<PrizeInfo>(tp.Item2);
                    if (null != user && null != prize)
                    {
                        user.PrizeInfo = prize;
                        ShowSysMessage($"用户{user.Name}锁定到一个奖品：{tp.Item2}");
                        PrizeUser(user);// 将中奖人塞入中奖队列
                    }
                }
                else
                {
                    ShowSysMessage(tp.Item2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错了", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 将中奖人塞入中奖队列
        /// </summary>
        /// <param name="user"></param>
        private void PrizeUser(UsersInfo user)
        {
            SetSendfigModel(PrizeUserQueueName);  //设置队列信息(中奖人)
            RabbitSend.Send(user);
            Thread.Sleep(50);
        }
        #endregion

        #region 显示消息
        private delegate void ShowDelegate(string strshow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void ShowSysMessage(string message)
        {
            if (cts.IsCancellationRequested)
            {
                return;
            }
            if (this.txtSysMessage.InvokeRequired)
            {
                Action<string> action = (data) =>
                {
                    ShowMessageInfo(message);
                };
                txtSysMessage.Invoke(action, new string[] { message });
            }
            else
            {
                ShowMessageInfo(message);
            }
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessageInfo(string message)
        {
           
            txtSysMessage.Invoke((MethodInvoker)delegate ()
            {
                txtSysMessage.AppendText(message + Environment.NewLine);// 获取为此环境定义的换行字符串。(对于非 Unix 平台为包含“\r\n”的字符串，对于 Unix 平台则为包含“\n”的字符串。)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSysMessage_TextChanged(object sender, EventArgs e)
        {
            this.txtSysMessage.SelectionStart = this.txtSysMessage.Text.Length;
            this.txtSysMessage.SelectionLength = 0;
            this.txtSysMessage.ScrollToCaret();
        }

        #endregion

        #region 清空消息
        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.txtSysMessage.Text = "";
        }
        #endregion

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FmMain_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(delegate { LoadData(); });
        }
        #endregion

        #region 处理队列中数据

        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            System.Timers.Timer t = new System.Timers.Timer(3000);   //实例化Timer类，设置间隔时间为10000毫秒；   
            t.Elapsed += new System.Timers.ElapsedEventHandler(InitRabbit); //到达时间的时候执行事件；   
            t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；   
        }

        /// <summary>
        /// 初始化队列中已有的数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void InitRabbit(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.IsHandleCreated)
            {
                Invoke(new Action(() =>
                {
                    ShowLbUserUserExchanges(RabbitSendConfig.ExchangesApi);
                    ShowLbQueues(RabbitSendConfig.QueuesApi);
                    ShowLbBindings(RabbitSendConfig.BingdingsApi);
                    ShowSysMessage($"[{DateTime.Now}]数据已更新....................");
                }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        private async void ShowLbUserUserExchanges(string apiUrl)
        {
            userExchanges = await GetListModel<List<ExchangeEntity>>(apiUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        private async void ShowLbQueues(string apiUrl)
        {
            queues = await GetListModel<List<QueueEntity>>(apiUrl);
            if (queues != null && queues.Any())
            {
                lbQueues.Items.Clear();
                lbPrize.Text = "0";
                lbUser.Text = "0";
                lbPrizeUser.Text = "0";
                foreach (var queueEntity in queues)
                {
                    lbQueues.Items.Add(queueEntity.name);
                    if (queueEntity.name == PrizeQueueName)
                    {
                        lbPrize.Text = queueEntity.messages_ready.ToString();  //奖品剩余数量
                    }
                    if (queueEntity.name == UserQueueName)
                    {
                        lbUser.Text = queueEntity.messages_ready.ToString();  //用户数量
                    }
                    if (queueEntity.name == PrizeUserQueueName)
                    {
                        lbPrizeUser.Text = queueEntity.messages_ready.ToString();  //中奖人数
                    }
                }
            }
            else
            {
                lbQueues.Items.Clear();
                lbPrize.Text = "0";
                lbUser.Text = "0";
                lbPrizeUser.Text = "0";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        private async void ShowLbBindings(string apiUrl)
        {
            bindings = await GetListModel<List<BindingEntity>>(apiUrl);
            if (bindings != null)
            {
                lbBindings.Items.Clear();
                foreach (var bindingEntity in bindings)
                {
                    lbBindings.Items.Add(string.Format("交换机:{0}---队列:{1}---Key:{2}", string.IsNullOrWhiteSpace(bindingEntity.source) ? "默认" : bindingEntity.source, bindingEntity.destination, bindingEntity.routing_key));
                }
            }
            else
            {
                lbBindings.Items.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private async Task<T> GetListModel<T>(string apiUrl)
        {
            string jsonContent = await ShowApiResult(apiUrl);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private async Task<string> ShowApiResult(string apiUrl)
        {
            var response = await ShowHttpClientResult(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> ShowHttpClientResult(string Url)
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", RabbitReceiveConfig.UserName, RabbitReceiveConfig.Password));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpResponseMessage response = await client.GetAsync(Url);
            return response;
        }
        #endregion

        #region 窗体关闭前事件
        /// <summary>
        /// 窗体关闭前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        } 
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public short Type { get; set; } = 1;

        public DateTime CreateTime = DateTime.Now;

        public short Status { get; set; } = 1;
    }

    /// <summary>
    /// 奖品信息表
    /// </summary>
    public class PrizeInfo : BaseModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string PrizeNo { get; set; }

        /// <summary>
        /// 使用人
        /// </summary>
        public string UseUsersId { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseDateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 总数
        /// </summary>
        public short Total { get; set; }

        /// <summary>
        /// 剩余
        /// </summary>
        public short Balance { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class UsersInfo : BaseModel
    {
        /// <summary>
        /// 中的奖
        /// </summary>
        public PrizeInfo PrizeInfo { get; set; }

    }

    public class ExchangeEntity
    {
        public MessageStatsEntity message_stats { get; set; }
        public string name { get; set; }
        public string vhost { get; set; }
        public string type { get; set; }
        public bool durable { get; set; }
        [JsonProperty("internal")]
        public bool internalFlag { get; set; }
        public bool auto_delete { get; set; }

    }


    public class MessageStatsEntity
    {
        public int publish_in { get; set; }
        public PublishindetailsEntity publish_in_details { get; set; }
        public int publish_out { get; set; }
        public PublishoutdetailsEntity publish_out_details { get; set; }
    }

    public class PublishindetailsEntity
    {
        public double rate { get; set; }
    }


    public class PublishoutdetailsEntity
    {
        public double rate { get; set; }
    }

    public class QueueEntity
    {
        public int memory { get; set; }
        public int messages { get; set; }
        public int messages_ready { get; set; }
        public int messages_unacknowledged { get; set; }
        public string idle_since { get; set; }
        public string consumers { get; set; }
        public string state { get; set; }
        public string name { get; set; }
        public string vhost { get; set; }
        public bool durable { get; set; }
        public bool auto_delete { get; set; }
        public string node { get; set; }
    }

    public class BindingEntity
    {
        public string source { get; set; }
        public string vhost { get; set; }
        public string destination { get; set; }
        public string destination_type { get; set; }
        public string routing_key { get; set; }
        public string properties_key { get; set; }

    }
}
