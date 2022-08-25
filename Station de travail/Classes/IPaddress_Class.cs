using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station_de_travail
{
    class IPaddress_Class : INotifyPropertyChanged
    {
        private string _ipAdddress;
        private string _port;


        public string IPAddress
        {
            get => _ipAdddress ;
            set
            {
                if (_ipAdddress != value)
                {
                    _ipAdddress = value;
                    NotifyPropertyChanged("IPAddress");
                }
            }
        }

        public string Port
        {
            get => _port;
            set
            {
                if (_port != value)
                {
                    _port = value;
                    NotifyPropertyChanged("Port");
                }
            }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new
                PropertyChangedEventArgs(propName));
        }
        public IPaddress_Class(string ipad,string port)
        {
            _ipAdddress = ipad;
            _port = port;
        }
        
        
    }
}
