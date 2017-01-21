﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using WPF_Chat_ver1.Model;

namespace WPF_Chat_ver1.Utility
{
    internal class ChatConnection
    {
        private static volatile ChatConnection instance;
        private static object syncRoot = new Object();
        private static EndPoint myEndPoint, myEndPointRemote;

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

        internal static void startCommunication(string frenip)
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
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack), buffer);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message + ",  Note : Are you providing a valid ip ?");
                // SetupSocket();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
                // SetupSocket();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
                // SetupSocket();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                // SetupSocket();
            }
        }

        // return the own ip
        internal static string GetLocalIP()
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

        public static void OperatorCallBack(IAsyncResult ar)
        {
            try
            {
                int size = ChatCommunication.EndReceiveFrom(ar, ref myEndPointRemote);

                // check if theres actually information
                if (size > 0)
                {
                    // used to help us on getting the data
                    byte[] aux = (byte[])ar.AsyncState;

                    // converts from data[] to string
                    var msg = Encoding.UTF8.GetString(aux);

                    string[] fullmessage = msg.Split('*');

                    // adds to listbox
                    //Dispatcher.Invoke(new Action(() =>
                    //{
                    // add to listbox
                    ChatModel.MyMessage = ("Friend: " + fullmessage[0].ToString().Trim());


                    //}), DispatcherPriority.SystemIdle, null);

                }

                // starts to listen again
                byte[] buffer = new byte[1464];
                ChatCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack), buffer);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
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
