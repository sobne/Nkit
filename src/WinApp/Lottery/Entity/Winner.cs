using System;

namespace Lottery.Entity
{
    [Serializable]
    /// <summary>
    /// 中奖记录
    /// </summary>
    public class Winner
    {
        public Lucker Lucker { get; set; }
        public Gift Gift { get; set; }
        public Winner()
        {

        }
        public Winner(Lucker lucker,Gift gift)
        {
            this.Lucker = lucker;
            this.Gift = gift;
        }
    }
}
