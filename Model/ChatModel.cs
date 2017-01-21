using System;
using System.Collections.ObjectModel;

namespace WPF_Chat_ver1.Model
{
    internal class ChatModel
    {
        private static volatile ChatModel Instance;

        public static ChatModel INSTANCE
        {

            get
            {
                if (Instance == null)
                {
                      Instance = new ChatModel();
                }

                return Instance;
            }
        }

        public event EventHandler MessageReceived;
        private ObservableCollection<string> myMessageRecieved;
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

        public event EventHandler MessageSend;
        private ObservableCollection<string> myMessageSend;
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
    }
}
