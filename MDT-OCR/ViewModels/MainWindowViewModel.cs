using MDT.Base;
using MDT.Core.Manager;
using MDT.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MDT_OCR.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
{
        private string oldCardName = string.Empty;
        private MarkWindow markWindow;
        public MainWindowViewModel()
        {
            if (markWindow==null)
            {
                markWindow = new MarkWindow();
                markWindow.Show();
            }
            CardInfo info = CardMgr.Instance.GetCardInfo("12950");
            cardName = info.cn_name;
            cardType = info.types;
            cardDesc = $"{info.desc }\n{info.pdesc}";
            autoDetecText = "自动检测已关闭";
            NativeMethodEx.FindWindow(null, "masterduel");
            Task.Run(() =>
            {
                TipText = "空格=检测选中卡片\n按住左ALT=拖动窗口和切换右上角组卡、决斗，右下角缩放\nF1=切换自动检测\n";
                Thread.Sleep(60000);
                TipText = string.Empty;
            });
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
        }
        private string cardName;
        private string cardType;
        private string cardDesc;
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
            if (markWindow == null)
            {

            }
            var point = markWindow.PointToScreen(new System.Windows.Point(0,0));
            string cardName = CardImageHandler.GetCardName((int)point.X,(int)point.Y,(int)markWindow.Width,(int)markWindow.Height);
            if (cardName != oldCardName && cardName != string.Empty)
            {
                CardInfo cardinfo = CardMgr.Instance.GetCardInfoByEnName(cardName);
                if (cardinfo != null)
                {
                    if (cardinfo != null)
                    {
                        CardName = cardinfo.cn_name;
                        CardType = cardinfo.types;
                        CardDesc = $"{cardinfo.desc }\n{cardinfo.pdesc}";
                    }
                }
            }
            oldCardName = cardName;
        }

        public void SwitchAutoDetec()
        {
            autoDetec = !autoDetec;
            AutoDetecText = autoDetec ? "自动检测已开启" : "自动检测已关闭";
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

        private bool autoDetec;
    }
}
