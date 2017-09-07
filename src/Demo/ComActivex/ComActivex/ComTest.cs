using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ComActivex
{
    [Guid("FB864848-1514-4402-B4BE-FF8A88945B50")]
    public interface ComTest_Interface
    {
        [DispId(1)]
        int add(int a, int b);
    }

    [Guid("E5474CBA-0197-4a3a-A960-72A548A1E5F4"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ComTest_Events
    { }
    [Guid("AE504361-B1E4-40c6-8EB7-F681E392F542"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(ComTest_Events))]
    public class ComTest:ComTest_Interface
    {
        public ComTest()
        { }
        public int add(int a, int b)
        {
            return a + b;
        }
    }
}
