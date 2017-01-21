using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.Command
{
    internal class SendCommand:ICommand
    {
        private ChatModel myChatModel;
        public SendCommand()
        {
            myChatModel = ChatModel.INSTANCE;
        }

        public void Execute(object textBoxMessage)
        {
            SendMessage(textBoxMessage.ToString());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private void SendMessage(string message)
        {
            // converts from string to byte[]
            var testmsg = message;
            var enc = new ASCIIEncoding();
            byte[] msg = enc.GetBytes(testmsg);

            // sending the message
            ChatConnection.Instance.ChatCommunication.Send(msg);
            ObservableCollection<string> msgs=new ObservableCollection<string>();
            msgs.Add(("You : " + testmsg));
            myChatModel.MESSAGESEND = msgs;
        }
    }
}
