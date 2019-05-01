﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerTest
{
    class Program
    {
        static object b1 = new object();
        static int b = 0;
        static void Main(string[] args)
        {
            TaskFactory fac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(10));

            //TaskFactory fac = new TaskFactory();
            for (int i = 0; i < 1000; i++)
            {

                fac.StartNew(s =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("启动线程：" +s);
                    //Write_txt("启动线程：" + b);
                    b++;
                    Thread.Sleep(20000);
                    Console.WriteLine("Current Index {0}, ThreadId {1}", s, Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("结束线程：" +s);
                    //Write_txt("结束线程：" + b);
                    
                }, i);
            }

            //Console.ReadLine();
        }

        private static void Write_txt(string log)
        {
            lock (b1)
            {
                string path = "D:\\YSJS-BK-Finance\\logs1\\";//文件路径
                string logFileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd") + "PEQ4004BLError.log");//全部路径
                if (!Directory.Exists(path))//若文件夹不存在则新建文件夹   
                    Directory.CreateDirectory(path); //新建文件夹   

                File.AppendAllText(logFileName, DateTime.Now.ToString() + " ");
                File.AppendAllText(logFileName, log);
                File.AppendAllText(logFileName, Convert.ToChar(13).ToString());
                File.AppendAllText(logFileName, Convert.ToChar(10).ToString());
            }
            

        }

    }
}
