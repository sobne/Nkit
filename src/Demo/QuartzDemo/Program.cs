using Nkit.job.Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start.");
            QuartzHelper.ExecuteInterval<DemoInterval>(10);

            string cronExpression = "0 25 16,18 * * ? ";　　//每天的9点和16点执行任务
            QuartzHelper.ExecuteByCron<DemoByCron>(cronExpression);

            Console.ReadLine();
        }
    }
}
