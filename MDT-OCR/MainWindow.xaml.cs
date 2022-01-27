using MDT.Core;
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
            Guard.Instance.Command += DragWindow_Command;
            Guard.Instance.Command += DetecCard_Command;
            Guard.Instance.Command += SwitchAutoDetec_command;
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

        private async Task SwitchAutoDetec_command(KeyboardUpdate arg)
        {
            if (arg.IsPressed && arg.Key == SharpDX.DirectInput.Key.F1)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    (this.DataContext as MainWindowViewModel).SwitchAutoDetec();
                }));
            }

        }

        private async Task DetecCard_Command(KeyboardUpdate arg)
        {
            if (arg.IsPressed && arg.Key == SharpDX.DirectInput.Key.Space)
            {
               await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    (this.DataContext as MainWindowViewModel).UpdateCardinfo();
                }));
            }
        }

        private async Task DragWindow_Command(KeyboardUpdate arg)
        {
            if (arg.IsPressed && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                   {
                       NativeMethodEx.RestoreMousePass(new WindowInteropHelper(this).Handle);
                   }));
            }
            else if (arg.IsReleased && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                  {
                      NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
                  }));
            }
        }
    }
}
