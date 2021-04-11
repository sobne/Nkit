using System;

namespace LotteryChooser.Entity
{
    [Serializable]
    /// <summary>
    /// 中奖记录
    /// </summary>
    public class Winner
    {
        public Chooser Chooser { get; set; }
        public Gift Gift { get; set; }
        public Winner()
        {

        }
        public Winner(Chooser chooser,Gift gift)
        {
            this.Chooser = chooser;
            this.Gift = gift;
        }
    }
}
