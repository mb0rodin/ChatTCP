using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFChat
{
    public class Client
    {
        public MainWindow PWindow { get; private set; }
      
        private ConnectPage _PConnectPage;
        private ChatControl _PChatControl;
        private Connecting _Pconnecting;
        public ConnectPage PConnectPage 
        {
            get
            {
                if (_PConnectPage != null)
                    return _PConnectPage;
                _PConnectPage = new ConnectPage(this);
                return _PConnectPage;
            }
            
        }
        public ChatControl PChatControl
        {
            get
            {
                if (_PChatControl != null)
                    return _PChatControl;
                _PChatControl = new ChatControl(this);
                return _PChatControl;
            }
            
        }
        public Connecting PConnecting
        {
            get
            {
                if (_Pconnecting != null)
                    return _Pconnecting;
                _Pconnecting = new Connecting(this);
                return _Pconnecting;
            }
        }
        public string ExInfo(object o)
        {
            const string nl = "\r\n";
            string s = "";
            if (o.ToString().Contains("Указан недопустимый адрес IP"))
            {
                s = "";
            }
            if (o.ToString().Contains("Сделана попытка выполнить операцию на сокете при отключенной сети"))
            {
                s += "Возможные причины: " + nl;
                s += "Вы не подключены к интернету;" + nl;
                s += "Вы ввели неверный IP;" + nl;
                s += "Сервер был отключен;" + nl;

            }
            return s;
        }
        
        public Client(MainWindow window)
        {
            PWindow = window;
        }
    }
}
