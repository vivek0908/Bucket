using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatBackend
{
    [ServiceContract]
    public interface IChatBackend
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessage(MessageContract data);

        void SendMessage(string msg);

        void StratCommunication();

        event EventHandler ServerStarted;
    }

    [DataContract]
    public class MessageContract
    {
        private string mySender = "Anonymous";
        private string myMessage = string.Empty;

        [DataMember]
        public string Sender
        {
            get { return mySender; }
            set { mySender = value; }
        }

        [DataMember]
        public string Message
        {
            get { return myMessage; }
            set { myMessage = value; }
        }

        public MessageContract(string sender, string message )
        {
            mySender = sender;
            myMessage = message;
        }
    }

    public delegate void DisplayMessageDelegate(MessageContract messageData);
}
