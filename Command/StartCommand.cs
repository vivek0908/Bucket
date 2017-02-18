using System;
using System.Windows.Input;
using WPF_Chat_ver1.Communication;

namespace WPF_Chat_ver1.Command
{
    public class StartCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;
        public void Execute(object frenip)
        {
            ChatConnection.Instance.StartCommunication(frenip.ToString());
            
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
