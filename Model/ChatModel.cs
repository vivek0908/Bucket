using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF_Chat_ver1.Model
{
    internal class ChatModel
    {
        private static volatile ChatModel Instance;
       // private ObservableCollection<string> myMessageRecieved;
        // private ObservableCollection<string> myMessageSend;
         private IList<string> myNewMessageText=new List<string>();

        public static ChatModel INSTANCE
        {
            get { return Instance ?? (Instance = new ChatModel()); }
        }

        //public IList<string> UpdatedMessageText
        //{
        //    get { return myNewMessageText; }
        //    set
        //    {
        //        myNewMessageText = value;
        //        if (MessageIsUpdated != null)
        //        {
        //            MessageIsUpdated(this, EventArgs.Empty);
        //        }
        //    }
        //}

        public string UpdatedMessageText1
        {
            get { return myNewMessageText1; }
            set
            {
                myNewMessageText1 = value;
                if (MessageIsUpdated != null)
                {
                    MessageIsUpdated(this, EventArgs.Empty);
                }
            }
        }

        //public ObservableCollection<string> MESSAGERECIEVED
        //{
        //    get { return myMessageRecieved; }
        //    set
        //    {
        //        myMessageRecieved = value;
        //        if (MessageReceived != null)
        //        {
        //            MessageReceived(this,EventArgs.Empty);
        //        }
        //    }
        //}

        //public ObservableCollection<string> MESSAGESEND
        //{
        //    get { return myMessageSend; }
        //    set
        //    {
        //        myMessageSend = value;
        //        if (MessageSend != null)
        //        {
        //            MessageSend(this, EventArgs.Empty);
        //        }
        //    }
        //}

        public event EventHandler MessageIsUpdated;
        private string myNewMessageText1;
//        public event EventHandler MessageReceived;

      //  public event EventHandler MessageSend;

        //public string UpdatedMessageText1 { get; set; }
    }
}
