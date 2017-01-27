using System;
using System.Collections.ObjectModel;
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
        private static volatile ChatConnection instance;
        private static readonly object syncRoot = new Object();
        private static EndPoint myEndPoint, myEndPointRemote;
        private readonly ChatModel myChatModel;
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
            myChatModel = ChatModel.INSTANCE;
        }

        internal void startCommunication(string frenip)
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

        public void OperatorCallBack(IAsyncResult ar)
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

                    //var msgs = new ObservableCollection<string>();
                    //msgs.Add("Friend: " + fullmessage[0].Trim());
                    Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                    {
                        // add to listbox
                        myChatModel.UpdatedMessageText1 +="Friend: " + fullmessage[0].Trim()+"\n";
                        //myChatModel.UpdatedMessageText = myChatModel.UpdatedMessageText;

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
    }
}
