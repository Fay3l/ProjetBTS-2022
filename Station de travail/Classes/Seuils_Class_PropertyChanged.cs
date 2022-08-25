using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station_de_travail
{
    class Seuils_Class_PropertyChanged : INotifyPropertyChanged
    {
        private string _maxvalue;
        private string _minvalue;
        private string _maxvalueco2;
        private string _minvalueco2;
        private string _maxvaluelux;
        private string _minvaluelux;
        private string _maxvaluetc;
        private string _minvaluetc;
        private string _maxvaluehum;
        private string _minvaluehum;
        private string _maxvaluelp;
        private string _minvaluelp;
        private string _maxvaluebat;
        private string _minvaluebat;
        private string _maxvaluepres;
        private string _minvaluepres;

        public string MinValue
        {
            get => _minvalue;
            set
            {
                if(_minvalue != value)
                {
                    _minvalue = value;
                    NotifyPropertyChanged(MinValue);
                }
            }
        }

        public string MaxValue
        {
            get => _maxvalue;
            set
            {
                if(_maxvalue != value)
                {
                    _maxvalue = value;
                    NotifyPropertyChanged(MaxValue);
                }
            }
        }

        public string MinValueCO2
        {
            get => _minvalueco2;
            set
            {
                if (_minvalueco2 != value)
                {
                    _minvalueco2 = value;
                    NotifyPropertyChanged(MinValueCO2);
                }
            }
        }

        public string MaxValueCO2
        {
            get => _maxvalueco2;
            set
            {
                if (_maxvalueco2 != value)
                {
                    _maxvalueco2 = value;
                    NotifyPropertyChanged(MaxValueCO2);
                }
            }
        }

        public string MinValueLUX
        {
            get => _minvaluelux;
            set
            {
                if (_minvaluelux  != value)
                {
                    _minvaluelux = value;
                    NotifyPropertyChanged(MinValueLUX);
                }
            }
        }

        public string MaxValueLUX 
        {
            get => _maxvaluelux;
            set
            {
                if (_maxvaluelux != value)
                {
                    _maxvaluelux = value;
                    NotifyPropertyChanged(MaxValueLUX);
                }
            }
        }
       
        public string MinValueTC
        {
            get => _minvaluetc;
            set
            {
                if (_minvaluetc != value)
                {
                    _minvaluetc = value;
                    NotifyPropertyChanged(MinValueTC);
                }
            }
        }

        public string MaxValueTC 
        {
            get => _maxvaluetc;
            set
            {
                if (_maxvaluetc != value)
                {
                    _maxvaluetc = value;
                    NotifyPropertyChanged(MaxValueTC);
                }
            }
        }

        public string MinValuePRES
        {
            get => _minvaluepres;
            set
            {
                if (_minvaluepres != value)
                {
                    _minvaluepres = value;
                    NotifyPropertyChanged(MinValuePRES);
                }
            }
        }

        public string MaxValuePRES
        {
            get => _maxvaluepres;
            set
            {
                if (_maxvaluepres != value)
                {
                    _maxvaluepres = value;
                    NotifyPropertyChanged(MaxValuePRES);
                }
            }
        }

        public string MinValueBAT
        {
            get => _minvaluebat;
            set
            {
                if (_minvaluebat != value)
                {
                    _minvaluebat = value;
                    NotifyPropertyChanged(MinValueBAT);
                }
            }
        }

        public string MaxValueBAT 
        {
            get => _maxvaluebat;
            set
            {
                if (_maxvaluebat != value)
                {
                    _maxvaluebat = value;
                    NotifyPropertyChanged(MaxValueBAT);
                }
            }
        }

        public string MinValueHUM
        {
            get => _minvaluehum;
            set
            {
                if (_minvaluehum != value)
                {
                    _minvaluehum = value;
                    NotifyPropertyChanged(MinValueHUM);
                }
            }
        }

        public string MaxValueHUM
        {
            get => _maxvaluehum;
            set
            {
                if (_maxvaluehum != value)
                {
                    _maxvaluehum = value;
                    NotifyPropertyChanged(MaxValueHUM);
                }
            }
        }

        public string MinValueLP
        {
            get => _minvaluelp;
            set
            {
                if (_minvaluelp != value)
                {
                    _minvaluelp = value;
                    NotifyPropertyChanged(MinValueLP);
                }
            }
        }

        public string MaxValueLP
        {
            get => _maxvaluelp;
            set
            {
                if (_maxvaluelp != value)
                {
                    _maxvaluelp = value;
                    NotifyPropertyChanged(_maxvaluelp);
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
        public Seuils_Class_PropertyChanged(string max,string min , string maxco2, string minco2, string maxlp, string minlp, string maxhum, string minhum ,string maxtc, string mintc ,string maxbat, string minbat, string maxpres, string minpres, string maxlux, string minlux)
        {
            _maxvalue = max;
            _minvalue = min;
            _maxvalueco2 = maxco2;
            _minvalueco2 = minco2;
            _minvaluelp = minlp;
            _maxvaluelp = maxlp;
            _maxvaluehum = maxhum;
            _minvaluehum = minhum;
            _maxvaluetc = maxtc;
            _minvaluetc = mintc;
            _maxvaluebat = maxbat;
            _minvaluebat = minbat;
            _maxvaluepres = maxpres;
            _minvaluepres = minpres;
            _minvaluelux = minlux;
            _maxvaluelux = maxlux;
        }
    }
}
