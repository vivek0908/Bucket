using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_Chat_ver1.Model
{
    internal static class ChatModel
    {
        private static string myMsg;

        public static string MyMessage
        {
            get { return myMsg; }
            set { myMsg = value; }
        }
        
        
    }
}
