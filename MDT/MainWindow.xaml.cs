using MDT.Manager;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Guard guard;
        public MainWindow Instance;
        public bool InBattle = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            Instance = this;
            this.Background.Opacity = 0.75;
            #region event
            this.Loaded += (s, e) =>
            {
                NativeMethod.SetMousePass(new WindowInteropHelper(this).Handle);
            };
            Task.Run(() =>
            {
                // Initialize DirectInput
                DirectInput directInput = new DirectInput();

                // Instantiate the joystick
                SharpDX.DirectInput.Keyboard keyboard = new SharpDX.DirectInput.Keyboard(directInput);

                // Acquire the joystick
                keyboard.Properties.BufferSize = 128;
                keyboard.Acquire();

                // Poll events from joystick
                while (true)
                {
                    keyboard.Poll();
                    KeyboardUpdate[] datas = keyboard.GetBufferedData();
                    foreach (KeyboardUpdate state in datas)
                    {
                        if (state.IsPressed && state.Key == SharpDX.DirectInput.Key.LeftAlt)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                NativeMethod.RestoreMousePass(new WindowInteropHelper(this).Handle);
                            }));
                        }
                        else if (state.IsReleased && state.Key == SharpDX.DirectInput.Key.LeftAlt)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                NativeMethod.SetMousePass(new WindowInteropHelper(this).Handle);
                            }));
                        }
                    }
                    Thread.Sleep(16);
                }
            });
            b_btn.Click += (s, e) =>
            {
                InBattle = !InBattle;
                if (InBattle)
                {
                    b_btn.Content = "决斗中";
                }
                else
                {
                    b_btn.Content = "组卡";

                }
            };
            this.MouseDown += (s, e) =>
            {
                if(e.LeftButton == MouseButtonState.Pressed)
                {

                }
                this.DragMove();
            };
            Task.Run(() =>
            {
                Rewrite rewrite = new Rewrite("masterduel");
                
                while (true)
                {
                    int previousCardid = 0;
                    long cardIdAddr = 0;
                    if (InBattle)
                    {
                        cardIdAddr = rewrite.MultiPointer64(rewrite.GetDLL("GameAssembly.dll"), 0x01CB2B90, new int[] { 0xB8, 0, 0x44});
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
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                CardInfo cardinfo = CardMgr.Instance.GetCardInfo(cardId.ToString());
                                if (cardinfo != null)
                                {
                                    card_name.Text = cardinfo.cn_name;
                                    types.Text = cardinfo.text.types;
                                    desc.Text = cardinfo.text.desc;
                                }
                            }));
                        }
                    }
                    previousCardid = cardId;
                    Thread.Sleep(16);
                }
            });
            #endregion
        }

    }
}
