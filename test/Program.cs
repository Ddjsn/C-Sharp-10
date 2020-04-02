using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace test
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("test1:");

        //    for (var i = 1; i < 10; i++)
        //    { test1(); }
        //    //Console.WriteLine("test2:");
        //    //for (var i = 1; i < 10; i++)
        //    //{ test2(); }
        //}

        static void test1()
        {
            var concurentDictionary = new ConcurrentDictionary<int, int>();

            var w = new ManualResetEvent(false);
            int timedCalled = 0;
            var threads = new List<Thread>();
            for (int i = 0, count = Environment.ProcessorCount; i < count; i++)
            {
                threads.Add(new Thread(() =>
                {
                    w.WaitOne();
                    var a = concurentDictionary.GetOrAdd(1, i1 =>
                    {
                        Interlocked.Increment(ref timedCalled);
                        Console.WriteLine($"delegate: {timedCalled}");
                        return timedCalled;
                    });
                }));
                threads.Last().Start();
            }

            w.Set();//release all threads to start at the same time         
            Thread.Sleep(100);
            Console.WriteLine(timedCalled);// output is 4, means call initial 4 times
                                           //Console.WriteLine(concurentDictionary.Keys.Count);
        }

        static void test2()
        {
            var concurentDictionary = new ConcurrentDictionary<int, int>();

            var w = new ManualResetEvent(false);
            int timedCalled = 0;
            var threads = new List<Thread>();
            Lazy<int> lazy = new Lazy<int>(() => { Interlocked.Increment(ref timedCalled); return 1; });
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                threads.Add(new Thread(() =>
                {
                    w.WaitOne();
                    var a = concurentDictionary.GetOrAdd(1, i1 =>
                    {
                        return lazy.Value;
                    });
                    //Console.WriteLine(a);
                }));
                threads.Last().Start();
            }

            w.Set();//release all threads to start at the same time         
            Thread.Sleep(100);
            //Console.WriteLine(concurentDictionary.Count);
            Console.WriteLine(timedCalled);// output is 4, means call initial 4 times
                                           //Console.WriteLine(concurentDictionary.Keys.Count);
        }

        const int count = 10;
        //赋值为false也就是没有信号
        static ManualResetEvent myResetEvent = new ManualResetEvent(false);
        static int number;
        static void Main(string[] args)
        {
            Thread thread = new Thread(funThread);
            thread.Name = "QQ";
            thread.Start();
            for (int i = 1; i < count; i++)
            {
                Console.WriteLine("first number: {0}", i);
                number = i;
                //这里是设置为有信号
                myResetEvent.Set();
                Thread.Sleep(2000);
            }
            thread.Abort();
            Console.ReadKey();
        }
        
        static void funThread()
        {
            while (true)
            {
                //执行到这个地方时，会等待set调用后改变了信号才接着执行
                myResetEvent.WaitOne();
                Console.WriteLine("end {0} number: {1}", Thread.CurrentThread.Name, number);
                //myResetEvent.Reset();//#此处取消信号 注意
            }
        }
    }
}
