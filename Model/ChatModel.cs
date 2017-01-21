using System;
using System.Collections.ObjectModel;

namespace WPF_Chat_ver1.Model
{
    internal class ChatModel
    {
        private static volatile ChatModel Instance;
        private ObservableCollection<string> myMessageRecieved;
        private ObservableCollection<string> myMessageSend;

        public static ChatModel INSTANCE
        {
            get { return Instance ?? (Instance = new ChatModel()); }
        }

        public ObservableCollection<string> MESSAGERECIEVED
        {
            get { return myMessageRecieved; }
            set
            {
                myMessageRecieved = value;
                if (MessageReceived != null)
                {
                    MessageReceived(this,EventArgs.Empty);
                }
            }
        }

        public ObservableCollection<string> MESSAGESEND
        {
            get { return myMessageSend; }
            set
            {
                myMessageSend = value;
                if (MessageSend != null)
                {
                    MessageSend(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler MessageReceived;

        public event EventHandler MessageSend;
    }
}
