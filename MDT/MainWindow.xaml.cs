using MDT.Core;
using MDT.Core.Manager;
using MDT.Models;
using SharpDX.DirectInput;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;


namespace MDT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow Instance;
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
