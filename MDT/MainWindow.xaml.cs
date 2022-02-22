using MDT.Core;
using SharpDX.DirectInput;
using System;
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
            Guard.Instance.Command += DragWindow_Command;
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

        private async Task DragWindow_Command(KeyboardUpdate arg)
        {
            if (arg.IsPressed && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NativeMethodEx.RestoreMousePass(new WindowInteropHelper(this).Handle);
                    this.ResizeMode = ResizeMode.CanResizeWithGrip;
                }));
            }
            else if (arg.IsReleased && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
                    this.ResizeMode = ResizeMode.NoResize;
                }));
            }
        }

    }
}
