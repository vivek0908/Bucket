using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WPF_Chat_ver1.Annotations;
using WPF_Chat_ver1.Command;
using WPF_Chat_ver1.Communication;
using WPF_Chat_ver1.Model;

namespace WPF_Chat_ver1.ViewModel
{
    internal class ChatViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string myFrensIPText;

        private string myHostIP;

        private bool myButton_Send_State;

        private bool mytextBox_Message_State;

        private bool mytextbox_FrensIP_State=true;

        private bool mybutton_Start_State=true;

        private string myServerMessage_Content="Server Stopped";

        private Brush myServerMessage_Foreground=Brushes.DarkRed;

        private bool mybutton_Reset_State;

        private StartCommand myStartCommand;

        private SendCommand mySendCommand;

        private ResetCommand myResetCommand;

        private string myMsgs;

        private string mytextBox_Message_Text;

        public string MyIP
        {
            get { return myHostIP; }
            set { myHostIP = value; }
        }

        public StartCommand STARTCommand
        {
            get { return myStartCommand; }
            set
            {
                myStartCommand = value;
            }
        }

        public SendCommand SENDCommand
        {
            get { return mySendCommand; }
            set
            {
                mySendCommand = value;
            }
        }

        public ResetCommand ResetCommand
        {
            get { return myResetCommand; }
            set { myResetCommand = value; }
        }

        public bool Button_Send_State
        {
            get { return myButton_Send_State; }
            set
            {
                myButton_Send_State = value;
                OnPropertyChanged("Button_Send_State");
            }
        }

        public bool TextBox_Message_State
        {
            get { return mytextBox_Message_State; }
            set
            {
                mytextBox_Message_State = value;
                OnPropertyChanged("TextBox_Message_State");
            }
        }

        public string TextBox_Message_Text
        {
            get { return mytextBox_Message_Text; }
            set
            {
                mytextBox_Message_Text = value;
                OnPropertyChanged("TextBox_Message_Text");
            }
        }

        public bool Textbox_FrensIP_State
        {
            get { return mytextbox_FrensIP_State; }
            set
            {
                mytextbox_FrensIP_State = value;
                OnPropertyChanged("Textbox_FrensIP_State");
            }
        }

        public string FrensIPText
        {
            get { return myFrensIPText; }
            set
            {
                myFrensIPText = value;
                OnPropertyChanged(FrensIPText);
            }
        }

        public bool Button_Start_State
        {
            get { return mybutton_Start_State; }
            set
            {
                mybutton_Start_State = value;
                OnPropertyChanged("Button_Start_State");
            }
        }

        public string ServerMessage_Content
        {
            get { return myServerMessage_Content; }
            set
            {
                myServerMessage_Content = value; 
                OnPropertyChanged("ServerMessage_Content");
            }
        }

        public Brush ServerMessage_Foreground
        {
            get { return myServerMessage_Foreground; }
            set
            {
                myServerMessage_Foreground = value;
                OnPropertyChanged("ServerMessage_Foreground");
            }
        }

        public bool Button_Reset_State
        {
            get { return mybutton_Reset_State; }
            set
            {
                mybutton_Reset_State = value;
                OnPropertyChanged("Button_Reset_State");
            }
        }

        public string MyMessages
        {
            get { return myMsgs; }
            set
            {
                myMsgs = value;
                OnPropertyChanged("MyMessages");
            }
        }

        public ChatViewModel()
        {
            myHostIP = ChatConnection.Instance.GetLocalIP();
            mySendCommand = new SendCommand();
            myStartCommand=new StartCommand();
            myStartCommand.CommunicationStarted += (sender, args) =>
            {
                Button_Send_State = true;
                TextBox_Message_State = true;
                Textbox_FrensIP_State = false;
                Button_Start_State = false;
                ServerMessage_Foreground = Brushes.ForestGreen;
                Button_Reset_State = true;
                ServerMessage_Content = "Server Started";
                MyMessages = string.Empty;
                mytextBox_Message_Text = string.Empty;
            };

            ChatModel.INSTANCE.MessageIsUpdated += (sender, args) => { MyMessages = ChatModel.INSTANCE.UpdatedMessageText; };
            ChatModel.INSTANCE.MessageIsSend+=(sender,args)=> { TextBox_Message_Text = string.Empty; };
            myResetCommand=new ResetCommand();
            myResetCommand.RESETRequested += (sender, args) =>
            {
                Button_Send_State = false;
                TextBox_Message_State = false;
                Textbox_FrensIP_State = true;
                Button_Start_State = true;
                ServerMessage_Foreground = Brushes.DarkRed;
                Button_Reset_State = false;
                ServerMessage_Content = "Server Stopped";
                FrensIPText = string.Empty;
                MyMessages = string.Empty;
                TextBox_Message_Text = string.Empty;
                ChatModel.INSTANCE.UpdatedMessageText = string.Empty;
            };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
