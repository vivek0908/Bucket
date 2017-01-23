using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPF_Chat_ver1.Communication;
using WPF_Chat_ver1.Model;

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
            var allMessages = (Tuple<string, ObservableCollection<string>>)textBoxMessage;
            string newMessage = allMessages.Item1;
            var oldMessage = allMessages.Item2;
            SendMessage(newMessage, oldMessage);
        }

        private void SendMessage(string newMessage, ObservableCollection<string> oldMessages)
        {
            // converts from string to byte[]
            var testmsg = newMessage;
            var enc = new ASCIIEncoding();
            byte[] msg = enc.GetBytes(testmsg+'*');

            
            var msgs = new ObservableCollection<string>();
            msgs.Add(("You : " + testmsg));
            if (oldMessages != null)
            {
                foreach (var messages in oldMessages)
                {
                    msgs.Add(messages);
                }
            }
            // sending the message
            ChatConnection.Instance.ChatCommunication.Send(msg);
            myChatModel.MESSAGESEND = msgs;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
