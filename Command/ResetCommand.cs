﻿using System;
using System.Windows.Input;
using WPF_Chat_ver1.Annotations;
using WPF_Chat_ver1.Communication;

namespace WPF_Chat_ver1.Command
{
    public class ResetCommand : ICommand
    {
        /// <see cref="ICommand.Execute"/>
        public void Execute([NotNull] object parameter)
        {
            ChatConnection.Instance.SetupSocket();
            if (RESETRequested != null)
            {
                RESETRequested(this, EventArgs.Empty);
            }
        }

        /// <see cref="ICommand.CanExecute"/>
        public bool CanExecute([NotNull] object parameter)
        {
            return true;
        }

        public event EventHandler RESETRequested;

        public event EventHandler CanExecuteChanged;
    }
}
