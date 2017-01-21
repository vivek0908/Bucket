using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.Command
{
    class StartCommand : ICommand
    {
        public void Execute(object frenIP)
        {
            ChatConnection.Instance.startCommunication(frenIP.ToString());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
