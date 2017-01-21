using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.Command
{
    internal class SendCommand:ICommand
    {
        private ChatConnection myConnection;
        private string myTextMessage;

        public SendCommand()
        {
            
           // myTextMessage = textMessage;
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
           ChatModel.MyMessage= ("You : "+ msg);

            //Dispatcher.Invoke(new Action(() =>
            //{
                // add to listbox
                //listBox1.Items.Add("You: " + myTextMessage);

            //}), DispatcherPriority.SystemIdle, null);

        }
    }
}
