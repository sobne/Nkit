using System;
using System.Drawing;
using System.Xml.Serialization;

namespace LotteryChooser.Entity
{
    public class GiftArgs : EventArgs
    {
        public Gift Gift;

        public GiftArgs(Gift gift)
        {
            this.Gift = gift;
        }
    }
    [Serializable]
    /// <summary>
    /// 奖项信息
    /// </summary>
    public class Gift: GiftDetail
    {
        [XmlIgnore]
        public Image Img
        {
            get;
            set;
        }
    }

    [Serializable]
    /// <summary>
    /// 奖项信息
    /// </summary>
    public class GiftDetail
    {
        /// <summary>
        /// 标识 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 单次抽取数量
        /// </summary>
        public int NumberPerTime { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }
}
