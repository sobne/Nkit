using System;
using System.Collections.Generic;
using System.Text;

namespace Nkit.Core.Utils
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateHelper
    {

        #region //C#里内置的DateTime
        /* 
           
            //今天
            DateTime.Now.Date.ToShortDateString();
            //昨天，就是今天的日期减一
            DateTime.Now.AddDays(-1).ToShortDateString();
            //明天，同理，加一
            DateTime.Now.AddDays(1).ToShortDateString();

            //本周(要知道本周的第一天就得先知道今天是星期几，从而得知本周的第一天就是几天前的那一天，要注意的是这里的每一周是从周日始至周六止
            DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToShortDateString();
            DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek)))).ToShortDateString();
            //如果你还不明白，再看一下中文显示星期几的方法就应该懂了
            //由于DayOfWeek返回的是数字的星期几，我们要把它转换成汉字方便我们阅读，有些人可能会用switch来一个一个地对照，其实不用那么麻烦的              
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            Day[Convert.ToInt16(DateTime.Now.DayOfWeek)];

            //上周，同理，一个周是7天，上周就是本周再减去7天，下周也是一样
            DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();
            DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();
            //下周
            DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) + 7).ToShortDateString();
            DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) + 7).ToShortDateString();
            //本月,很多人都会说本月的第一天嘛肯定是1号，最后一天就是下个月一号再减一天。当然这是对的
            //一般的写法
            DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "1"; //第一天
            DateTime.Parse(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "1").AddMonths(1).AddDays(-1).ToShortDateString();//最后一天

            //巧用C#里ToString的字符格式化更简便
            DateTime.Now.ToString("yyyy-MM-01");
            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToShortDateString();

            //上个月，减去一个月份
            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToShortDateString();
            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //下个月，加去一个月份
            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).ToShortDateString();
            DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(2).AddDays(-1).ToShortDateString();
            //7天后
            DateTime.Now.Date.ToShortDateString();
            DateTime.Now.AddDays(7).ToShortDateString();
            //7天前
            DateTime.Now.AddDays(-7).ToShortDateString();
            DateTime.Now.Date.ToShortDateString();

            //本年度，用ToString的字符格式化我们也很容易地算出本年度的第一天和最后一天
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).ToShortDateString();
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(1).AddDays(-1).ToShortDateString();
            //上年度，不用再解释了吧
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(-1).ToShortDateString();
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddDays(-1).ToShortDateString();
            //下年度
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(1).ToShortDateString();
            DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(2).AddDays(-1).ToShortDateString();

            //本季度，很多人都会觉得这里难点，需要写个长长的过程来判断。其实不用的，我们都知道一年四个季度，一个季度三个月
            //首先我们先把日期推到本季度第一个月，然后这个月的第一天就是本季度的第一天了
            DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
            //同理，本季度的最后一天就是下季度的第一天减一
            DateTime.Parse(DateTime.Now.AddMonths(3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //下季度，相信你们都知道了。。。。收工
            DateTime.Now.AddMonths(3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
            DateTime.Parse(DateTime.Now.AddMonths(6 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
            //上季度
            DateTime.Now.AddMonths(-3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
            DateTime.Parse(DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString(); 
             */
        #endregion

        /// <summary>
        /// 返回指定日期是当月第几个星期函数
        /// </summary>
        /// <param name="dt">指定日期</param>
        /// <returns>当月第几个星期</returns>
        public static int GetWeekOfMonth(DateTime dt)
        {
            return (dt.Day - 1 + (int)dt.AddDays(1 - dt.Day).DayOfWeek) / 7;
        }
        /// <summary>
        /// 将英文的星期几转为中文
        /// </summary>
        /// <param name="dw"></param>
        /// <returns></returns>
        public static string ConvertDayOfWeekToZH(System.DayOfWeek dw)
        {
            string DayOfWeekZh = "";
            switch (dw.ToString("D"))
            {
                case "0":
                    DayOfWeekZh = "日";
                    break;
                case "1":
                    DayOfWeekZh = "一";
                    break;
                case "2":
                    DayOfWeekZh = "二";
                    break;
                case "3":
                    DayOfWeekZh = "三";
                    break;
                case "4":
                    DayOfWeekZh = "四";
                    break;
                case "5":
                    DayOfWeekZh = "五";
                    break;
                case "6":
                    DayOfWeekZh = "六";
                    break;
            }
            return DayOfWeekZh;
        }
        /// <summary>
        /// 某年某月的最后一天
        /// </summary>
        /// <param name="iYear">年</param>
        /// <param name="iMonth">月</param>
        /// <returns></returns>
        public static DateTime LastDate(int iYear, int iMonth)
        {
            try
            {
                DateTime d1, d2;
                if (iMonth == 12)
                    d1 = new DateTime(iYear + 1, 1, 1);
                else
                    d1 = new DateTime(iYear, iMonth + 1, 1);
                d2 = d1.AddDays(-1);
                return d2;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 当月的第一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetFirstDateOfThisMonth()
        {
            try
            {
                DateTime d1, d2;
                d1 = DateTime.Today;
                d2 = new DateTime(d1.Year, d1.Month, 1);
                return d2;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 当月的最后一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastDateOfThisMonth()
        {
            try
            {
                DateTime d1, d2;
                d1 = DateTime.Today;
                d2 = LastDate(d1.Year, d1.Month);
                return d2;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 当月的中间那天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMiddleDateOfThisMonth()
        {
            try
            {
                DateTime d1, d2;
                d1 = DateTime.Today;
                d2 = new DateTime(d1.Year, d1.Month, 15);
                return d2;
            }
            catch
            {
                throw;
            }
        }
        public static DateTime GetBeginDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime GetEndDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
    }
}
