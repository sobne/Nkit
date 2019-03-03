using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lottery.Services
{
    public class LotteryRuntime
    {
        private static readonly object Locker = new object();
        private static LotteryRuntime _instance;
        public static LotteryRuntime Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if(_instance==null)
                        {
                            _instance=  new LotteryRuntime();
                        }
                    }
                }
                return _instance;
            }
        }
        private LotteryRuntime()
        {

        }

        private string _currentpath= string.Empty;
        private string _winnerXml = string.Empty;

        private IList<Gift> gifts;      //奖品
        private IList<Gift> gifts_left; //奖池
        private IList<Lucker> luckers;  //参与者
        private IList<Winner> winners;  //幸运者
        //private IList<Lucker> CurLuckers;
        private delegate void Handler();
        Handler handler;

        public AppSetting AppSetting { get; set; }
        public bool IsAdd { get; set; }
        public int CurrentGiftId { get; private set; }
        public int CurrentGiftCount { get; private set; }
        private int CurrentGiftNumber { get; set; }
        private int CurrentGiftNumberPerTime { get; set; }

        public void Init()
        {
            AppSetting = new AppSetting();
            _currentpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _winnerXml = _currentpath + "winners.xml";

            gifts = LoadGifts();
            CurrentGiftId = -1;
            //handler = new Handler(LoadGift);
            //IAsyncResult result = handler.BeginInvoke(new AsyncCallback(LoadGiftComplete), null);
            //while (result.IsCompleted)
            //{

            //}
            //handler = new Handler(LoadLucker);
            //result = handler.BeginInvoke(new AsyncCallback(LoadLuckerComplete), null);

        }
        public void LoadData()
        {
            winners = LoadWinners();
            if (winners != null && winners.Any())
            {
                foreach (var item in winners)
                {
                    if(item.Lucker!=null)
                        item.Lucker.Photo = GetPhoto(_currentpath + item.Lucker.Pic);
                }
            }
            luckers = LoadLuckers();
            gifts_left = LoadGiftLeft();
            RemoveGiftLeft(winners);
        }
        public void SetCurrentGift(int giftId)
        {
            if (giftId == -1) return;
            CurrentGiftId = giftId;
            var giftItem = gifts.FirstOrDefault(x => x.Id.Equals(CurrentGiftId));
            CurrentGiftNumber = giftItem.Number;
            CurrentGiftNumberPerTime = giftItem.NumberPerTime;
            var giftWinnerCount = GetGiftWinnerCount(giftId);
            var giftRemainCount = CurrentGiftNumber - giftWinnerCount;
            CurrentGiftCount = giftRemainCount > CurrentGiftNumberPerTime ? CurrentGiftNumberPerTime : giftRemainCount;
        }
        public bool HasGiftAndLuckerRemain()
        {
            if (CurrentGiftId ==-1 || luckers == null || !luckers.Any())
            {
                return false;
            }
            if (IsAdd)
            {
                return true;
            }
            return CurrentGiftCount > 0;
        }
        public bool HasGiftRemain()
        {
            return gifts_left.Count > 0;
            //return gifts.Count >= (winners==null?0:winners.Count);
            //List<int> giftIds = new List<int>();
            //if(winners != null && winners.Count > 0)
            //{
            //    foreach (var item in winners)
            //    {
            //        if (!giftIds.Contains(item.Gift.Id)) giftIds.Add(item.Gift.Id);
            //    }
            //}
            //return gifts.Count >= giftIds.Count;
        }

        public void AddLuckers(IList<Lucker> list)
        {
            if (list == null) return;
            //CurLuckers = list;
            IsAdd = false;

            var wins = new List<Winner>();
            foreach (var win in list)
            {
                if (win == null) continue;
                Winner wininfo = new Winner(win, gifts.FirstOrDefault(x => x.Id.Equals(CurrentGiftId)));
                wins.Add(wininfo);
                //CurLuckers.Add(win);
                luckers.Remove(win);
                //HasGiftRemain();
            }
            AddWinners(wins);
        }
        public void AddWinners(IList<Winner> list)
        {
            if (list == null) return;
            if (winners == null) winners = new List<Winner>();
            foreach (var win in list)
            {
                winners.Add(win);
            }
            if(SaveWinner(winners))
            {
                RemoveGiftLeft(list);
            }
        }

        public IList<Gift> GetGiftsLeft()
        {
            return this.gifts_left;
        }
        public IList<Gift> GetGifts()
        {
            return this.gifts;
        }
        public IList<Lucker> GetLuckers()
        {
            return this.luckers;
        }
        public IList<Winner> GetWinners()
        {
            return this.winners;
        }
        private int GetGiftWinnerCount(int giftId)
        {
            if (giftId == -1) return 0;
            if (winners != null && winners.Any())
            {
                int i = 0;
                foreach (Winner info in winners)
                {
                    if (info.Gift.Id == giftId)
                    {
                        i++;
                    }
                }
                return i;
            }
            return 0;
        }

        private void LoadGift()
        {
            gifts = LoadGifts();
        }
        private void LoadLucker()
        {
            luckers = LoadLuckers();
        }
        private void RemoveGiftLeft(IList<Winner> list)
        {
            if (list == null) return;
            foreach (var win in list)
            {
                var gift = gifts_left.FirstOrDefault(x => x.Id.Equals(win.Gift.Id));
                if (gift != null)
                    gifts_left.Remove(gift);
            }
        }
        private IList<Gift> LoadGiftLeft()
        {
            var list = new List<Gift>();
            foreach (var item in gifts)
            {
                for (int i = 0; i < item.Number; i++)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private IList<Winner> LoadWinners()
        {
            try
            {
                IList<Winner> list = new List<Winner>();
                using (StreamReader read = new StreamReader(_winnerXml, Encoding.UTF8))
                {
                    var xml = read.ReadToEnd();
                    using (StringReader sr = new StringReader(xml))
                    {
                        XmlSerializer xmldes = new XmlSerializer(list.GetType());
                        list = xmldes.Deserialize(sr) as IList<Winner>;
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private IList<Gift> LoadGifts()
        {
            try
            {
                XmlHelper xmlhepler = new XmlHelper(AppSetting.GiftXml);
                XmlNodeList nodes = xmlhepler.SelectNodes("/list/info");
                if (nodes != null)
                {
                    var list = new List<Gift>();
                    foreach (XmlNode node in nodes)
                    {
                        Gift item = new Gift();
                        XmlNodeList childnodes = node.ChildNodes;
                        item.Id = Convert.ToInt32(node.Attributes[0].Value);
                        item.Name = childnodes[0].InnerText;
                        item.Number = Convert.ToInt32(childnodes[1].InnerText);
                        item.NumberPerTime = Convert.ToInt32(childnodes[2].InnerText);
                        item.Pic = childnodes[3].InnerText;
                        item.Description = childnodes[4].InnerText;
                        item.Title = childnodes[5].InnerText;
                        item.Level = Convert.ToInt32(childnodes[6].InnerText);
                        item.Photo = GetPhoto(_currentpath + item.Pic);
                        list.Add(item);
                    }
                    return list;
                }
                xmlhepler = null;
            }
            catch (Exception)
            {

                throw;
            }
            
            return null;
        }
        private IList<Lucker> LoadLuckers()
        {
            try
            {
                XmlHelper xmlhepler = new XmlHelper(AppSetting.AvatarXml);
                XmlNodeList nodes = xmlhepler.SelectNodes("/list/info");
                if (nodes != null)
                {
                    var list = new List<Lucker>();
                    foreach (XmlNode node in nodes)
                    {
                        var Id = Convert.ToInt32(node.Attributes[0].Value);
                        if (isWinner(Id)) continue;

                        Lucker item = new Lucker();
                        XmlNodeList childnodes = node.ChildNodes;
                        item.Name = childnodes[0].InnerText;
                        item.Department = childnodes[1].InnerText;
                        item.Pic = childnodes[2].InnerText;
                        item.Photo = GetPhoto(_currentpath + item.Pic);
                        item.Remark = childnodes[3].InnerText;
                        item.Id = Convert.ToInt32(node.Attributes[0].Value);
                        list.Add(item);
                    }
                    return list;
                }
                xmlhepler = null;

            }
            catch (Exception)
            {

                throw;
            }
            
            return null;
        }

        void LoadGiftComplete(IAsyncResult result)
        {
            Handler handler = (Handler)((AsyncResult)result).AsyncDelegate;
            handler.EndInvoke(result);
        }

        void LoadLuckerComplete(IAsyncResult result)
        {
            Handler handler = (Handler)((AsyncResult)result).AsyncDelegate;
            handler.EndInvoke(result);
        }


        private bool SaveWinner(IList<Winner> list)
        {
            SetCurrentGift(CurrentGiftId);
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer xz = new XmlSerializer(winners.GetType());
                    xz.Serialize(sw, winners);
                    var xml = sw.ToString();
                    var xmlDoc = new XmlDocument();
                    //xmlDoc.LoadXml(xml.Replace("\r\n", ""));
                    xmlDoc.LoadXml(xml);
                    xmlDoc.Save(_winnerXml);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        private bool isWinner(int Id)
        {
            if (winners!=null && winners.Count > 0)
            {
                return winners.Any(x => x.Lucker!=null && x.Lucker.Id.Equals(Id));
            }
            return false;
        }
        private Image GetPhoto(string path)
        {
            Image photo;
            try
            {
                photo = Image.FromFile(path);
            }
            catch
            {
                photo = Image.FromFile(AppSetting.DefaultPhoto);
            }

            return photo;
        }
    }
}
