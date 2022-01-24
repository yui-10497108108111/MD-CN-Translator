using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MDT.Base
{
    /// <summary>
    /// 通知属性更改基类
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// 通知属性更改
        /// </summary>
        /// <param name="propertyName">字段名</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 通知属性更改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">字段</param>
        /// <param name="Value">值</param>
        /// <param name="propertyName">字段名</param>
        /// <returns></returns>
        public bool Set<T>(ref T field, T Value, [CallerMemberName] string propertyName = null)
        {
            //如果值没有变化
            if (EqualityComparer<T>.Default.Equals(field, Value))
                return false;
            //如果是一个新值
            field = Value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 通知所有
        /// </summary>
        protected void RaiseAllChange()
        {
            RaisePropertyChanged("");
        }

    }
}
