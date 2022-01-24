using MDT.Base;
using MDT.Core.Manager;
using MDT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MDT_OCR.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
{
        private string oldCardName = string.Empty;

        public MainWindowViewModel()
        {
            CardInfo info = CardMgr.Instance.GetCardInfo("12950");
            cardName = info.cn_name;
            cardType = info.types;
            cardDesc = info.desc;
            BattleBtnContent = "组卡";
            autoDetecText = "自动检测已关闭";
            NativeMethodEx.FindWindow(null, "masterduel");
            TipText = "空格=检测选中卡片，按住左ALT=拖动窗口，F1=切换自动检测";
            //Task.Run(() =>
            //{
            //    Thread.Sleep(30000);
            //    TipText = string.Empty;
            //});
            #region update cardinfo
            Task.Run(() =>
            {
                while (true)
                {
                    while (autoDetec)
                    {
                        try
                        {
                            UpdateCardinfo();
                            Thread.Sleep(500);

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Thread.Sleep(50);
                }
            });
            #endregion
            SwitchCommand = new RelayCommand((o) =>
            {
                InBattle = !InBattle;
                if (InBattle)
                {
                    BattleBtnContent = "决斗中";
                }
                else
                {
                    BattleBtnContent = "组卡";
                }
            });
        }
        public ICommand SwitchCommand { get; private set; }
        private string cardName;
        private string cardType;
        private string cardDesc;
        private string battleBtnContent;
        private string tipText;
        private string autoDetecText;

        public string AutoDetecText
        {
            get
            {
                return autoDetecText;
            }
            set
            {
                autoDetecText = value;
                RaisePropertyChanged(nameof(autoDetecText));
            }
        }

        public string TipText
        {
            get
            {
                return tipText;
            }
            set
            {
                tipText = value;
                RaisePropertyChanged(nameof(tipText));
            }
        }

        public void UpdateCardinfo()
        {
            string cardName = CardImageHandler.GetCardName(InBattle);
            if (cardName != oldCardName && cardName != string.Empty)
            {
                CardInfo cardinfo = CardMgr.Instance.GetCardInfoByEnName(cardName);
                if (cardinfo != null)
                {
                    if (cardinfo != null)
                    {
                        CardName = cardinfo.cn_name;
                        CardType = cardinfo.types;
                        CardDesc = cardinfo.desc;
                    }
                }
            }
            oldCardName = cardName;
        }

        public void SwitchAutoDetec()
        {
            autoDetec = !autoDetec;
            if (autoDetec)
            {
                AutoDetecText = "自动检测已开启";
            }
            else
            {
                AutoDetecText = "自动检测已关闭";
            }
        }
        public string BattleBtnContent
        {
            get
            {
                return battleBtnContent;
            }
            set
            {
                battleBtnContent = value;
                RaisePropertyChanged(nameof(battleBtnContent));
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

        private bool InBattle;
        private bool autoDetec;
    }
}
