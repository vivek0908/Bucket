﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using WPF_Chat_ver1.Model;

namespace WPF_Chat_ver1.Communication
{
    internal class ChatConnection
    {
        public event EventHandler CommunicationStarted;

        private static volatile ChatConnection instance;
        private static readonly object syncRoot = new Object();
        private static EndPoint myEndPoint, myEndPointRemote;
        private ChatModel myChatModel;
        private Socket myCommunication;

        internal Socket ChatCommunication 
        {
            get { return myCommunication; }
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
                        instance = new ChatConnection();
                    }
                }
                return instance;
            }
            private set { instance = value; }
        }

        internal ChatConnection()
        {
            ChatCommunication = SetupSocket();
            myChatModel = ChatModel.INSTANCE;
        }

        internal void StartCommunication(string frenip)
        {
            try
            {
                // bind socket                        
                myEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 8435);
                ChatCommunication.Bind(myEndPoint);

                // connect to remote ip and port 
                myEndPointRemote = new IPEndPoint(IPAddress.Parse(frenip), 8435);
                ChatCommunication.Connect(myEndPointRemote);

                // starts to listen to an specific port
                byte[] buffer = new byte[1464];
                ChatCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack),buffer);

                if (CommunicationStarted != null)
                {
                    CommunicationStarted(this, EventArgs.Empty);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message + ",  Note : Are you providing a valid ip ?");
                 SetupSocket();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                 SetupSocket();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                 SetupSocket();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                 SetupSocket();
            }
        }

        // return the own ip
        internal string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        internal void OperatorCallBack(IAsyncResult ar)
        {
            try
            {
                int size = ChatCommunication.EndReceiveFrom(ar, ref myEndPointRemote);

                // check if theres actually information
                if (size > 0)
                {
                    // used to help us on getting the data
                    var aux = (byte[])ar.AsyncState;

                    // converts from data[] to string
                    var msg = Encoding.UTF8.GetString(aux);

                    var fullmessage = msg.Split('*');

                    Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                    {
                        // add to listbox
                        myChatModel.UpdatedMessageText +="Friend: " + fullmessage[0].Trim()+"\n";

                    }), DispatcherPriority.SystemIdle, null);
                }

                // starts to listen again
                var buffer = new byte[1464];
                ChatCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack), buffer);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        internal Socket SetupSocket()
        {
            // set up socket
            myCommunication = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            myCommunication.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            return myCommunication;
        }

        internal void Detach()
        {
            Instance = null;
        }
    }
}