using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WPF_Chat_ver1.Annotations;
using WPF_Chat_ver1.Command;
using WPF_Chat_ver1.Model;
using WPF_Chat_ver1.Utility;

namespace WPF_Chat_ver1.ViewModel
{
    internal class ChatViewModel:INotifyPropertyChanged
    {
        private string myHostIP;

        public string MyIP
        {
            get { return myHostIP; }
            set { myHostIP = value; }
        }

        private bool myButton_Send_State;

        public bool Button_Send_State
        {
            get { return myButton_Send_State; }
            set
            {
                myButton_Send_State = value;
                OnPropertyChanged("Button_Send_State");
            }
        }

        private bool mytextBox_Message_State;

        public bool TextBox_Message_State
        {
            get { return mytextBox_Message_State; }
            set
            {
                mytextBox_Message_State = value;
                OnPropertyChanged("TextBox_Message_State");
                
            }
        }

        private bool mytextbox_FrensIP_State=true;

        public bool Textbox_FrensIP_State
        {
            get { return mytextbox_FrensIP_State; }
            set
            {
                mytextbox_FrensIP_State = value;
                OnPropertyChanged("Textbox_FrensIP_State");
                
            }
        }

        private bool mybutton_Start_State=true;

        public bool Button_Start_State
        {
            get { return mybutton_Start_State; }
            set
            {
                mybutton_Start_State = value;
                OnPropertyChanged("Button_Start_State");
                
            }
        }

        private string myServerMessage_Content="Server Stopped";

        public string ServerMessage_Content
        {
            get { return myServerMessage_Content; }
            set
            {
                myServerMessage_Content = value; 
                OnPropertyChanged("ServerMessage_Content");
            }
        }

        private Brush myServerMessage_Foreground=Brushes.DarkRed;

        public Brush ServerMessage_Foreground
        {
            get { return myServerMessage_Foreground; }
            set
            {
                myServerMessage_Foreground = value;
                OnPropertyChanged("ServerMessage_Foreground");
                
            }
        }

        private bool mybutton_Reset_State;

        public bool Button_Reset_State
        {
            get { return mybutton_Reset_State; }
            set
            {
                mybutton_Reset_State = value;
                OnPropertyChanged("Button_Reset_State");
                
            }
        }

        private StartCommand myStartCommand;

        public StartCommand STARTCommand
        {
            get { return myStartCommand; }
            set
            {
                myStartCommand = value;
                

            }
        }

        private SendCommand mySendCommand;

        public SendCommand SENDCommand
        {
            get { return mySendCommand; }
            set
            {
                mySendCommand = value;
                
            }
        }

        private ResetCommand myResetCommand;

        public ResetCommand ResetCommand
        {
            get { return myResetCommand; }
            set { myResetCommand = value; }
        }

        private ObservableCollection<string> myMsgs;

        public ObservableCollection<string> MyMessages
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
            };

            ChatModel.INSTANCE.MessageReceived += (sender, args) => { MyMessages = ChatModel.INSTANCE.MESSAGERECIEVED; };
            ChatModel.INSTANCE.MessageSend += (sender, args) => { MyMessages = ChatModel.INSTANCE.MESSAGESEND; };

            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
