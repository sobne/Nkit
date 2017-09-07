using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MyCom
{
    [ComVisible(true)]
    [Guid("7B0D594B-D0C4-445c-8476-65947A6CBACE")]
    public interface IMyCom
    {
        void Initialize(string b);
        void Dispose();
        int Add(int x, int y);
    }
    [ComVisible(true)]
    [Guid("07AFD50E-9057-4f38-87AC-13AA7490FB9F")]
    //[ClassInterface(ClassInterfaceType.None)]
    [ProgId("MyCom.MyCom")]
    public class MyCom:IMyCom
    {
        public string a { get; set; }
        public void Initialize(string b)
        {
            a = b;
        }
        public void Dispose()
        {
            a = "00";
        }
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
