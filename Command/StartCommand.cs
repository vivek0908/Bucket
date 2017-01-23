using System;
using System.Windows.Input;
using WPF_Chat_ver1.Communication;

namespace WPF_Chat_ver1.Command
{
    class StartCommand : ICommand
    {
        public void Execute(object frenIP)
        {
            ChatConnection.Instance.startCommunication(frenIP.ToString());
            if (CommunicationStarted != null)
            {
                CommunicationStarted(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CommunicationStarted;

        public event EventHandler CanExecuteChanged;
    }
}
