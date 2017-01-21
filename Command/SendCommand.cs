using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.Command
{
    internal class SendCommand:ICommand
    {
        private string myTextMessage;
        private ChatModel myChatModel;
        public SendCommand()
        {
            myChatModel = ChatModel.INSTANCE;
        }

        public void Execute(object parameter)
        {
            myTextMessage = parameter.ToString();
            SendMessage();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private void SendMessage()
        {
            // converts from string to byte[]
            var testmsg = myTextMessage + "*";
            var enc = new ASCIIEncoding();
            byte[] msg = enc.GetBytes(testmsg);

            // sending the message
            ChatConnection.Instance.ChatCommunication.Send(msg);
            myChatModel.MESSAGESEND = ("You : " + testmsg);
        }
    }
}
