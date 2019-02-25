using Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lock
{
    class Program
    {
        static RedisStackExchangeHelper _redis = new RedisStackExchangeHelper();
        static void Main(string[] args)
        {
            //新建一个Task
            Task t1 = new Task(() => {
                Lock();
            });
            //启动Task
            t1.Start();
            Console.WriteLine("UI线程完成！");
            //新建一个Task
            Task t2 = new Task(() => {
                Lock();
            });
            //启动Task
            t2.Start();
            Console.WriteLine("UI2线程完成！");
            Lock();

        }

        static void Lock()
        {
            Console.WriteLine("Start..........");
            var db = _redis.GetDatabase();
            RedisValue token = Environment.MachineName;
            //实际项目秒杀此处可换成商品ID
            if (db.LockTake("test", token, TimeSpan.FromSeconds(10000)))
            {
                try
                {
                    var res = db.StringGet("test1");
                    Console.WriteLine("Working..........{0}", res);
                    Thread.Sleep(1000);

                    Console.WriteLine("秒杀成功..........{0}", res);
                }
                finally
                {
                    db.LockRelease("test", token);
                }
            }

            Console.WriteLine("Over..........");
            Console.ReadLine();
        }
    }
}
