using System;
using System.Drawing;
using System.Xml.Serialization;

namespace LotteryChooser.Entity
{
    [Serializable]
    /// <summary>
    /// 参与者信息
    /// </summary>
    public class Chooser: Lucker
    {
        public bool Win { get; set; }
        [XmlIgnore]
        public Image Img
        {
            get;
            set;
        }
    }
    [Serializable]
    public class Lucker
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 参与者名称:可以是姓名/编号/电话号码
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参与者属性:如部门
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 照片/号码图片等
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
