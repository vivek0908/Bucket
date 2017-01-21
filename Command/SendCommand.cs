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

        public SendCommand()
        {
        }

        public event EventHandler MessageUpdated;

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
            ChatModel.MyMessage= ("You : "+ msg);
            if (MessageUpdated != null)
            {
                MessageUpdated(this,EventArgs.Empty);
            }
            //Dispatcher.Invoke(new Action(() =>
            //{
                // add to listbox
                //listBox1.Items.Add("You: " + myTextMessage);

            //}), DispatcherPriority.SystemIdle, null);

        }
    }
}
