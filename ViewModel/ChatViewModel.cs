using System.Net;
using System.Net.Sockets;
using System.Windows.Media;
using WPF_Chat_ver1.Command;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.ViewModel
{
    internal class ChatViewModel
    {
        private string myHostIP;

        public string MyIP
        {
            get { return myHostIP; }
            set { myHostIP = value; }
        }

        private bool myButton_Send_State=true;

        public bool Button_Send_State
        {
            get { return myButton_Send_State; }
            set { myButton_Send_State = value; }
        }

        private bool mytextBox_Message_State=true;

        public bool TextBox_Message_State
        {
            get { return mytextBox_Message_State; }
            set { mytextBox_Message_State = value; }
        }

        private bool mytextbox_FrensIP_State=true;

        public bool Textbox_FrensIP_State
        {
            get { return mytextbox_FrensIP_State; }
            set { mytextbox_FrensIP_State = value; }
        }

        private bool mybutton_Start_State=true;

        public bool Button_Start_State
        {
            get { return mybutton_Start_State; }
            set { mybutton_Start_State = value; }
        }

        private string myServerMessage_Content="Server Stopped";

        public string ServerMessage_Content
        {
            get { return myServerMessage_Content; }
            set { myServerMessage_Content = value; }
        }

        private Brush myServerMessage_Foreground=Brushes.DarkRed;

        public Brush ServerMessage_Foreground
        {
            get { return myServerMessage_Foreground; }
            set { myServerMessage_Foreground = value; }
        }

        private bool mybutton_Reset_State;

        public bool Button_Reset_State
        {
            get { return mybutton_Reset_State; }
            set { mybutton_Reset_State = value; }
        }

        private StartCommand myStartCommand;

        public StartCommand STARTCommand
        {
            get { return myStartCommand; }
            set
            {
                myStartCommand = value;
                Button_Send_State = true;
                TextBox_Message_State = true;
                Textbox_FrensIP_State = false;
                Button_Start_State = false;
                ServerMessage_Content = "Server Started";
                ServerMessage_Foreground = Brushes.ForestGreen;
                Button_Reset_State = true;

                myMsgs = ChatModel.MyMessage;
            }
        }

        private SendCommand mySendCommand;

        public SendCommand SENDCommand
        {
            get { return mySendCommand; }
            set
            {
                mySendCommand = value;
                myMsgs = ChatModel.MyMessage;
                
            }
        }

        private ResetCommand myResetCommand;

        public ResetCommand ResetCommand
        {
            get { return myResetCommand; }
            set { myResetCommand = value; }
        }

        private string myMsgs;

        public string MyMessages
        {
            get { return myMsgs; }
            set { myMsgs = value; }
        }
        

        public ChatViewModel()
        {
            myHostIP = ChatConnection.Instance.GetLocalIP();
            mySendCommand = new SendCommand();
            myStartCommand=new StartCommand();
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

        

    }
}
