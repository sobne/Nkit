using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class DemoInterval:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("DemoInterval..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
    public class DemoByCron : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("DemoByCron..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        }
    }
}
