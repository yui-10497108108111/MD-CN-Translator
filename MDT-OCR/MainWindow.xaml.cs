using MDT_OCR.ViewModels;
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

namespace MDT_OCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            Instance = this;
            this.Background.Opacity = 0.75;
            #region event
            this.Loaded += (s, e) =>
            {
                NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
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
                                NativeMethodEx.RestoreMousePass(new WindowInteropHelper(this).Handle);
                            }));
                        }
                        else if (state.IsReleased && state.Key == SharpDX.DirectInput.Key.LeftAlt)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
                            }));
                        }
                        if (state.IsPressed && state.Key == SharpDX.DirectInput.Key.Space)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                (this.DataContext as MainWindowViewModel).UpdateCardinfo();

                            }));
                        }
                        if (state.IsPressed && state.Key == SharpDX.DirectInput.Key.F1)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                (this.DataContext as MainWindowViewModel).SwitchAutoDetec();
                            }));
                        }
                    }
                    Thread.Sleep(16);
                }
            });
            this.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.Background.Opacity = 0.9;
                    this.DragMove(); 
                }
                if (e.LeftButton == MouseButtonState.Released)
                {
                    this.Background.Opacity = 0.75;
                } 
            };

            #endregion
        }
    }
}
