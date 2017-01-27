using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_Chat_ver1.Communication;
using WPF_Chat_ver1.Model;

namespace WPF_Chat_ver1.Command
{
    public class SendCommand:ICommand
    {
        private ChatModel myChatModel;

        public SendCommand()
        {
            myChatModel = ChatModel.INSTANCE;
        }

        public void Execute(object textBoxMessage)
        {
            //var allMessages = (Tuple<string, ObservableCollection<string>>)textBoxMessage;
            string newMessage = textBoxMessage.ToString();
            //string newMessage = allMessages.Item1;
            //var oldMessage = allMessages.Item2;
            // SendMessage(newMessage, oldMessage);
             SendMessage(newMessage);
        }

        private void SendMessage(string newMessage)
        {
            // converts from string to byte[]
            var testmsg = newMessage;
            var enc = new ASCIIEncoding();
            byte[] msg = enc.GetBytes(testmsg + '*');

            Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                // add to listbox
                myChatModel.UpdatedMessageText1 += "You : " + testmsg + "\n";
                //myChatModel.UpdatedMessageText = myChatModel.UpdatedMessageText;

            }), DispatcherPriority.SystemIdle, null);
            //var msgs = new ObservableCollection<string>();
            //msgs.Add(("You : " + testmsg));
            // sending the message
            ChatConnection.Instance.ChatCommunication.Send(msg);
        }

        //private void SendMessage(string newMessage, ObservableCollection<string> oldMessages)
        //{
        //    // converts from string to byte[]
        //    var testmsg = newMessage;
        //    var enc = new ASCIIEncoding();
        //    byte[] msg = enc.GetBytes(testmsg+'*');

            
        //    var msgs = new ObservableCollection<string>();
        //    msgs.Add(("You : " + testmsg));
        //    if (oldMessages != null)
        //    {
        //        foreach (var messages in oldMessages)
        //        {
        //            msgs.Add(messages);
        //        }
        //    }
        //    // sending the message
        //    ChatConnection.Instance.ChatCommunication.Send(msg);
        //    myChatModel.MESSAGEUPDATED = msgs;
        //}

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
