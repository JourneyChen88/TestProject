using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //获取Redis操作接口
                IRedisClient Redis = RedisManager.GetClient();
                //放入内存

                User u = new User();
                u.Name = "小张";
                u.Pwd = "123";
                Redis.Set<User>("my_name", u);
                Redis.Set<int>("my_age", 12);
                //保存到硬盘
                Redis.Save();
                //释放内存
                Redis.Dispose();
                //取出数据
                Console.WriteLine("取出刚才存进去的数据 \r\n 我的Name:{0}; 我的Age:{1}.",
                                                                 Redis.Get<string>("my_name"), Redis.Get<int>("my_age"));

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadKey();
            }
        }
    }
}
