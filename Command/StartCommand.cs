using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.Command
{
    class StartCommand : ICommand
    {
        private EndPoint myEndPoint, myEndPointRemote;
        
        public StartCommand()
        {

            //loaded sssfly
            

            // am i in master

                
        }

        public void Execute(object parameter)
        {
            var frenip = (parameter.ToString());

            try
            {
                // bind socket                        
                myEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIP()), 8435);
                ChatConnection.ChatCommunication.Bind(myEndPoint);

                // connect to remote ip and port 
                myEndPointRemote = new IPEndPoint(IPAddress.Parse(frenip), 8435);
                ChatConnection.ChatCommunication.Connect(myEndPointRemote);

                // starts to listen to an specific port
                byte[] buffer = new byte[1464];
                ChatConnection.ChatCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack), buffer);

                //button_Send.IsEnabled = true;
                //textBox_Message.IsEnabled = true;
                //textbox_FrensIP.IsEnabled = false;
                //button_Start.IsEnabled = false;
                //lblServerMessage.Content = "Server Started";
                //lblServerMessage.Foreground = Brushes.ForestGreen;
                //button_Reset.IsEnabled = true;
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
        private string GetLocalIP()
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
                int size = ChatConnection.ChatCommunication.EndReceiveFrom(ar, ref myEndPointRemote);

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
                    ChatModel.MyMessage= ("Friend: " + fullmessage[0].ToString().Trim());
                        

                    //}), DispatcherPriority.SystemIdle, null);

                }

                // starts to listen again
                byte[] buffer = new byte[1464];
                ChatConnection.ChatCommunication.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref myEndPointRemote, new AsyncCallback(OperatorCallBack), buffer);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
