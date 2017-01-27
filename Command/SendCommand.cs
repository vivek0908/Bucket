using System;
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
            string newMessage = textBoxMessage.ToString();
            SendMessage(newMessage);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private void SendMessage(string newMessage)
        {
            // converts from string to byte[]
            var testmsg = newMessage;
            var enc = new ASCIIEncoding();
            byte[] msg = enc.GetBytes(testmsg + '*');

            Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                // add to TextBlock
                myChatModel.UpdatedMessageText += "You : " + testmsg + "\n";

            }), DispatcherPriority.SystemIdle, null);
            ChatConnection.Instance.ChatCommunication.Send(msg);
        }

        
    }
}
