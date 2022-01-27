using MDT.Core;
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
using System.Windows.Shapes;

namespace MDT_OCR
{
    /// <summary>
    /// Interaction logic for MarkWindow.xaml
    /// </summary>
    public partial class MarkWindow : Window
    {
        public MarkWindow()
        {
            InitializeComponent();
            Guard.Instance.Command += EnableEdit_Command;
            this.Background.Opacity = 0.1;
            this.Topmost = true;
            #region event
            this.Loaded += (s, e) =>
            {
                NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
            };
            this.MouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            #endregion
        }

        private async Task EnableEdit_Command(SharpDX.DirectInput.KeyboardUpdate arg)
        {
            if (arg.IsPressed && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NativeMethodEx.RestoreMousePass(new WindowInteropHelper(this).Handle);
                    this.Background.Opacity = 0.9;
                    this.ResizeMode = ResizeMode.CanResizeWithGrip;
                }));
            }
            else if (arg.IsReleased && arg.Key == SharpDX.DirectInput.Key.LeftAlt)
            {
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NativeMethodEx.SetMousePass(new WindowInteropHelper(this).Handle);
                    this.Background.Opacity = 0.3;
                    this.ResizeMode = ResizeMode.NoResize;
                }));
            }
        }
    }
}
