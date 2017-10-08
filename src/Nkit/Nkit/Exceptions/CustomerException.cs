using System;

namespace Nkit.Exceptions
{
    public class CustomerException
    {
        /// <summary>
        /// 自定义异常,除数不能为零
        /// </summary>
        public class Divisor : System.Exception
        {
            public const string ExceptionInfo = "除数不能为零";
        }
    }
}
