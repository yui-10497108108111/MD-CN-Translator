using MDT.Base;
using MDT.Core.Manager;
using MDT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MDT.ViewModels
{
    public class MainWindowViewModel:NotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            CardInfo info = CardMgr.Instance.GetCardInfo("12950");
            cardName = info.cn_name;
            cardType = info.text.types;
            cardDesc = info.text.desc;
            BattleBtnContent = "组卡";
            #region update cardinfo
            Task.Run(() =>
            {
                Rewrite rewrite = new Rewrite("masterduel");
                while (true)
                {
                    try
                    {
                    int previousCardid = 0;
                    long cardIdAddr = 0;
                    if (InBattle)
                    {
                        cardIdAddr = rewrite.MultiPointer64(rewrite.GetDLL("GameAssembly.dll"), 0x01CB2B90, new int[] { 0xB8, 0, 0x44 });
                    }
                    else
                    {
                        cardIdAddr = rewrite.MultiPointer64(rewrite.GetDLL("GameAssembly.dll"), 0x01CCD278, new int[] { 0xB8, 0, 0xF8, 0x1D8, 0x20 });
                    }
                    int cardId = (int)rewrite.ReadInt64(cardIdAddr);
                    if (cardId != previousCardid)
                    {
                        if (cardId != 0)
                        {
                            CardInfo cardinfo = CardMgr.Instance.GetCardInfo(cardId.ToString());
                            if (cardinfo != null)
                            {
                                CardName = cardinfo.cn_name;
                                CardType = cardinfo.text.types;
                                CardDesc = cardinfo.text.desc;
                            }
                        }
                    }
                    previousCardid = cardId;
                    Thread.Sleep(20);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            });
            #endregion
            SwitchCommand = new RelayCommand((o) =>
            {
                InBattle = !InBattle;
                if (InBattle)
                {
                    BattleBtnContent  = "决斗中";
                }
                else
                {
                    BattleBtnContent  = "组卡";

                }
            });
        }
        public ICommand SwitchCommand { get; private set; }
        private string cardName;
        private string cardType;
        private string cardDesc;
        private string battleBtnContent;

        public string BattleBtnContent
        {
            get
            {
                return battleBtnContent;
            }
            set
            {
                battleBtnContent = value; RaisePropertyChanged(nameof(battleBtnContent));
            }
        }


        public string CardName
        {
            get
            {
                return cardName;
            }
            set
            {
                cardName = value;
                RaisePropertyChanged(nameof(CardName));
            }
        }
        public string CardType
        {
            get
            {
                return cardType;
            }
            set
            {
                cardType = value;
                RaisePropertyChanged(nameof(CardType));
            }
        }
        public string CardDesc
        {
            get
            {
                return cardDesc;
            }
            set
            {
                cardDesc = value;
                RaisePropertyChanged(nameof(CardDesc));
            }
        }

        public bool InBattle { get; private set; }
    }
}
