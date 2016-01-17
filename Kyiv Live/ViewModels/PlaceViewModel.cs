using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kyiv_Live.ViewModels
{
    public class PlaceViewModel:INotifyPropertyChanged
    {
        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _web;
        public string Web
        {
            get
            {
                return this._web;
            }
            set
            {
                this._web = value;
                NotifyPropertyChanged("Web");
            }
        }

        private string _adress;
        public string Adress
        {
            get
            {
                return this._adress;
            }
            set
            {
                this._adress = value;
                NotifyPropertyChanged("Adress");
            }
        }

        private SolidColorBrush _subwayColor;
        public SolidColorBrush SubwayColor
        {
            get
            {
                return this._subwayColor;
            }
            set
            {
                this._subwayColor = value;
                NotifyPropertyChanged("Subway");
            }
        }

        private string _subwayName;
        public string SubwayName
        {
            get
            {
                return this._subwayName;
            }
            set
            {
                this._subwayName = value;
                NotifyPropertyChanged("SubwayName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
