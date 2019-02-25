using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreaterAndCustum
{

    /// <summary>
    /// 生产者  消息  消费者；  这个三个应该是相互独立的对象；
    /// 在这里例子中，我们只有一个消费者； 后面我们可以将起改造成多个线程去消费我们的任务；
    /// 去处理其中的任务；
    /// 当队列中有的数据之后，我们要想法通知我们的消费者；
    /// 还要去看更多的实例；
    /// 看别人的各种设计想法和思路；
    /// To ensure thread safety, we used a lock to protect access to the Queue<string> collection.
    /// 
    /// 这个生成这的消费者模式；produce 和 consume 并不是同步的，也就是生产一个 消费一个
    /// 生产者由主线程不断的生成，然后加入到队列中， 消费者一个个的从队列中去取出数据，进行消费；
    /// 实现方式有多中滴呀；
    /// 线程的进入是同步的，    线程的消费也是同步的，而且只有一个线程；
    /// 如何做到多个线程异步生成  然后进入队列， 然后多个线程异步消费我们的额队列呢；
    /// </summary>

    public class ProducerConsumerQueue : IDisposable
    {

        EventWaitHandle _wh = new AutoResetEvent(false);
        Thread _worker;
        readonly object _locker = new object();     //这个锁，是用来同步读和写的；集合的读和写的；
        Queue<string> _tasks = new Queue<string>();     //一旦队列有了消息就要通知我们的线程去消费；


        /// <summary>
        /// 让线程跑起来
        /// </summary>
        public ProducerConsumerQueue()
        {
            _worker = new Thread(Work);
            _worker.Start();
        }

        /// <summary>
        /// 往线程中添加任务；一旦有了任务，就通知我们的消费者；
        /// 这里例子有很多问题滴呀；
        /// </summary>
        /// <param name="task"></param>
        public void EnqueueTask(string task)
        {
            lock (_locker) { _tasks.Enqueue(task); }
            _wh.Set();
        }

        public void Work()
        {
            while (true)  //相当于就是在不断的去轮询我们的队列中的信息；让这个线程一直处于执行的状态，应为只有一个线程，不能让它执行一完一次任务只有就停止了
            {
                string task = null;
                lock (_locker)   //这个锁是用来同步读和写的；
                {
                    if (_tasks.Count > 0) //队列中有数据，我们就从中取出数据；
                    {
                        task = _tasks.Dequeue();// 取出任务；
                        if (task == null) return;//任务为空就停止了；
                    }
                }
                if (task != null)
                {
                    //就取执行我们的任务；
                    Console.WriteLine("始终只有一个消费者：Task:{0} ThreadID{1} and now Quen length:{2}", task, Thread.CurrentThread.ManagedThreadId, _tasks.Count);
                    Thread.Sleep(1000);

                }
                else
                {
                    _wh.WaitOne(); //队列为空就等待

                }

            }
        }

        public void Dispose()
        {
            EnqueueTask(null);     // Signal the consumer to exit.
            _worker.Join();         // Wait for the consumer's thread to finish.
            _wh.Close();            // Release any OS resources.
            Console.WriteLine("对象销毁完毕...");

        }

    }


    class Program
    {

        static void Test()
        {
            //主线程在往我们的队列中添加消息。相当于就是我们的生产者； 往我们的队列中添加消息；
            using (ProducerConsumerQueue pc = new ProducerConsumerQueue())
            {
                pc.EnqueueTask("Hello");
                for (int i = 0; i < 10; i++) pc.EnqueueTask("Say " + i);
                pc.EnqueueTask("Goodbye!");
            }
        }

        static void Main(string[] args)
        {
            Test();
            Console.ReadLine();


        }
    }
}