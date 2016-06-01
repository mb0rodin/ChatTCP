using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerLib.ServerToUser
{
    public class UserInfo  : IValue
    {
        private string _NickName;
        private string _FullName;
        private string _Info;
        private string _Password;
        private byte[] _Image;
        private int _Age;
        private ServerLib.TypeUser _Type;

        public byte[] Image
        {
            get
            {
                if (_Image == null)
                {
                    _Image = new byte[]{255};
                    return _Image;
                }
                return _Image;
            }
            set
            {
                _Image = value;
            }
        }
        public string NickName
        {
            get
            {
                if (_NickName == null)
                {
                    _NickName = "Unknown";
                    return _NickName;
                }
                return _NickName;
            }
            set
            {
                _NickName = value;
            }
        }
        public string FullName
        {
            get
            {
                if (_FullName == null)
                {
                    _FullName = "Unknown";
                    return _FullName;
                }
                return _FullName;
            }
            set
            {
                _NickName = value;
            }
        }
        public string Info
        {
            get
            {
                if (_Info == null)
                {
                    _Info = "Unknown";
                    return _Info;
                }
                return _Info;
            }
            set
            {
                _Info = value;
            }
        }
        public string Password
        {
            get
            {
                if (_Password == null)
                {
                    _Password = "pass";
                    return _Password;
                }
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        public int Age
        {
            get
            {
                if (_Age == null)
                {
                    _Age = 0;
                    return _Age;
                }
                return _Age;
            }
            set
            {
                _Age = value;
            }
        }
        public ServerLib.TypeUser Type
        {
            get
            {
                if (_Type == null)
                {
                    _Type = 0;
                    return _Type;
                }
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

    }
}
