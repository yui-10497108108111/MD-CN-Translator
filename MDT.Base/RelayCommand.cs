using System;
using System.Windows.Input;

namespace MDT.Base
{
    public class RelayCommand : ICommand
    {

        /// <summary>
        /// 构造方法 <see cref="RelayCommand"/>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        /// <summary>
        /// 这个event raised 检查<see cref="CanExecute(object)"/> 变化;
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// 检查这个命令是否可以支持
        /// 备注:如果可以执行函数没有设置,返回true;
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
            => _CanExecute == null ? true : _CanExecute(parameter);
        private readonly Func<object, bool> _CanExecute = null;

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
            => _Execute(parameter);
        private readonly Action<object> _Execute;
    }
    public class RelayCommand<T> : ICommand
    {
        Action<T> mAction;


        public RelayCommand(Action<T> oneParamAction)
        {
            mAction = oneParamAction;
        }
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction?.Invoke((T)parameter);
        }
    }
}
