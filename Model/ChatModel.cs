using System;

namespace WPF_Chat_ver1.Model
{
    internal class ChatModel
    {
        public event EventHandler MessageIsUpdated;
        public event EventHandler MessageIsSend;
        private static volatile ChatModel Instance;
        private string myNewMessageText;

        public static ChatModel INSTANCE
        {
            get { return Instance ?? (Instance = new ChatModel()); }
        }

        public string UpdatedMessageText
        {
            get { return myNewMessageText; }
            set
            {
                myNewMessageText = value;
                if (MessageIsUpdated != null)
                {
                    MessageIsUpdated(this, EventArgs.Empty);
                }

                if (MessageIsSend != null)
                {
                    MessageIsSend(this, EventArgs.Empty);
                }
            }
        }

    }
}
