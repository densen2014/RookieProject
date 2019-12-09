
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace TimedTask.ViewModel
{
    /// <summary>
    /// 命令类
    /// </summary>
    public class CommandBase : ICommand
    {
        private readonly Action<object> _executeCommand = null;
        private readonly Func<object, bool> _canExecuteCommand = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeMethod"></param>
        public CommandBase(Action<object> executeMethod)
            : this(executeMethod, null)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeMethod"></param>
        public CommandBase(Action executeMethod)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeCommand"></param>
        /// <param name="canExecuteCommand"></param>
        public CommandBase(Action<object> executeCommand, Func<object, bool> canExecuteCommand)
        {
            if (executeCommand == null)
                throw new ArgumentNullException("executeMethod");

            this._executeCommand = executeCommand;
            this._canExecuteCommand = canExecuteCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #region ICommand 成员
        /// <summary>
        /// 指示当前命令在目标元素上是否可用
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (this._canExecuteCommand != null)
            {
                return this._canExecuteCommand(parameter);
            }
            return true;

        }

        /// <summary>
        ///  执行
        /// </summary>
        public void Execute(object parameter)
        {
            if (this._executeCommand != null)
            {
                this._executeCommand(parameter);
            }
        }

        #endregion
    }
}
