using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace Lottery.Entity
{
    [Serializable]
    /// <summary>
    /// 参与人员信息
    /// </summary>
    public class Lucker
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        public bool Win { get; set; }

        [XmlIgnore]
        public Image Photo
        {
            get;
            set;
        }
    }
}
