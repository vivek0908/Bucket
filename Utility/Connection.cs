using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace WPF_Chat_ver1.Utility
{
    internal class ChatConnection
    {
        private static volatile ChatConnection instance;
        private static object syncRoot = new Object();

        private static Socket myCommunication;

        internal static Socket ChatCommunication 
        {
            get { return SetupSocket(); }
            set { myCommunication = value; }
        }

        internal static ChatConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ChatConnection();
                    }
                }

                return instance;
            }
            
        }

        public ChatConnection()
        {
            myCommunication = SetupSocket();
        }

        private static Socket SetupSocket()
        {
            // set up socket
            myCommunication = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            myCommunication.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            return myCommunication;
        }


    }
}
