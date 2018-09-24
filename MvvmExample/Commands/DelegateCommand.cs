﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmExample.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _action;
        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        #endregion
    }
}
