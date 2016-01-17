using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kyiv_Live
{
    public class KLPlace
    {
        private string name = "";
        private string description = "";
        private List<KLTag> tags = new List<KLTag>();
        private BitmapImage image = null;
        private string street = "";
        private string web = "";
        private double price = 0;
        private KLSubway subway = new KLSubway();
        private string imagepath = "";
        private int promoted = -1;
        private GeoCoordinate coordinate = new GeoCoordinate();

        public KLPlace()
        {
            
        }

        public void setName(string p)
        {
            this.name = p;
        }
        public string getName()
        {
            return this.name;
        }

        public void setDescription(string Value)
        {
            this.description = Value;
        }

        public string getDescription()
        {
           return this.description;
        }

        public void setImage(string path)
        {
            if (path!=string.Empty)
            this.image = new BitmapImage(new Uri(@"Assets/images/" + path, UriKind.RelativeOrAbsolute));
        }

        public BitmapImage getImage()
        {
            return this.image;
        }

        public void setStreet(string p)
        {
            this.street = p;
        }

        public string getStreet()
        {
            return this.street;
        }

        public void setPrice(string value)
        {
            if (value != string.Empty)
            {
                this.price = double.Parse(value);
            }
        }

        public double getPrice()
        {
            return this.price;
        }

        public void addTag(KLTag tag)
        {
            this.tags.Add(tag);
        }

        public List<KLTag> getTags()
        {
            return this.tags;
        }

        public KLSubway getSubway()
        {
            return this.subway;
        }

        public void setSubway(KLSubway subway)
        {
            this.subway = subway;
        }

        public void setWeb(string web)
        {
            this.web = web;
        }

        public string getWeb()
        {
            return this.web;
        }

        public GeoCoordinate getCoordinates()
        {
            return this.coordinate;
        }

        
        public void setCoordinates(string data)
        {
            try
            {
                double latitude = double.Parse(data.Substring(0, data.IndexOf(',')).Replace('.',','));
                double longtitude = double.Parse(data.Substring(data.IndexOf(' ') + 1).Replace('.', ','));
                this.coordinate = new GeoCoordinate(latitude, longtitude);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void setImageName(string p)
        {
            imagepath = "http://pixelstore.com.ua/live/kyiv/" + p;
        }

        public string getImageName()
        {
            return this.imagepath;
        }

        public int getPromoted()
        {
            return 0;
        }

        public void setPromoted(string p)
        {
            this.promoted = 1;
        }
    }
}
