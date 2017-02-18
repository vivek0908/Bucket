using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatBackend
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatBackend : IChatBackend
    {
        DisplayMessageDelegate myMessageDelegate = null;
        ServiceHost host = null;
        ChannelFactory<IChatBackend> channelFactory = null;
        IChatBackend myChannel;

        public event EventHandler ServerStarted;

        public ChatBackend(DisplayMessageDelegate msgDelegate)
        {
            myMessageDelegate = msgDelegate;
        }

        public void StratCommunication()
        {
            StartServer();
        }

        public void DisplayMessage(MessageContract data)
        {
            if(myMessageDelegate != null)
            {
                myMessageDelegate(data);
            }
        }

        public void SendMessage(string msg)
        {
            myChannel.DisplayMessage(new MessageContract("", msg));
        }

        private void StartServer()
        {
            host = new ServiceHost(this);
            host.Open();

            //Client channel
            channelFactory = new ChannelFactory<IChatBackend>("ChatEndpoint");
            myChannel = channelFactory.CreateChannel();
        }
    }
}
