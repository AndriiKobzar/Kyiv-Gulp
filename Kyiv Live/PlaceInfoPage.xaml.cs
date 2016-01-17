using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media.Imaging;
using System.Device.Location;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;

namespace Kyiv_Live
{
    public partial class PlaceInfoPage : PhoneApplicationPage
    {
        bool isLoaded = false;
        
        public String[] REMOVABLE_URL_PARTS = {"https://", "http://"};
        public const int SHORT_URL_END = 20;
        KLPlace current; 
        List<GeoCoordinate> MyCoordinates = new List<GeoCoordinate>();
        RouteQuery MyQuery = null;
        GeocodeQuery Mygeocodequery = null;

        public PlaceInfoPage()
        {
            InitializeComponent();
            int id = (int)PhoneApplicationService.Current.State["currentPlaceIndex"];
            current = App.ViewModel.data.getPlaces()[id]; 
            this.GetCoordinates();
            
        }

        public String getShortWebUrl(string webUrl) 
        {
            if (webUrl.Length>1) 
            {
            // remove protocols
                String res = webUrl;
                foreach(String rem in REMOVABLE_URL_PARTS) 
                {
                    res = res.Replace(rem, "");
                }

                // makes length shorter
                if (res.Length > SHORT_URL_END) 
                {
                    res = res.Substring(0, SHORT_URL_END);
                    res += "..";
                } 
                else 
                {
                    res = res.Substring(0, res.Length);
                }
                
                // removes '/' as the last character
                if (res.LastIndexOf("/") == res.Length - 1) 
                {
                    res = res.Substring(0, res.Length - 1);
                }

                return res;
                } else 
                    {
                return "";
                    }
        }
        private async void GetCoordinates()
        {
            // Get the phone's current location.
            Geolocator MyGeolocator = new Geolocator();
            MyGeolocator.DesiredAccuracyInMeters = 5;
            Geoposition MyGeoPosition = null;
            try
            {
                
                MyGeoPosition = await MyGeolocator.GetGeopositionAsync(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
                MyCoordinates.Add(new GeoCoordinate(MyGeoPosition.Coordinate.Latitude, MyGeoPosition.Coordinate.Longitude));
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
            }
            catch (Exception ex)
            {
                // Something else happened while acquiring the location.
                MessageBox.Show(ex.Message);
            }
            Mygeocodequery = new GeocodeQuery();
            Mygeocodequery.SearchTerm = current.getStreet();
            Mygeocodequery.GeoCoordinate = current.getCoordinates();
            Mygeocodequery.QueryCompleted += Mygeocodequery_QueryCompleted;
            Mygeocodequery.QueryAsync();
        }
        void Mygeocodequery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                try
                {
                    MyQuery = new RouteQuery();
                    MyCoordinates.Add(current.getCoordinates());
                    MyQuery.Waypoints = MyCoordinates;
                    MyQuery.QueryCompleted += MyQuery_QueryCompleted;
                    MyQuery.QueryAsync();
                    Mygeocodequery.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не вдалося побудувати маршрут");
                }
            }
        }
        void MyQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                try
                {
                    Route MyRoute = e.Result;
                    MapRoute MyMapRoute = new MapRoute(MyRoute);
                    localMap.AddRoute(MyMapRoute);
                    MyQuery.Dispose();
                }
                catch
                {
                    MessageBox.Show("Не вдалося побудувати маршрут");
                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pageName.Text = current.getName();
           // List<PlaceViewModel> Item = new List<PlaceViewModel>();
         //   Item.Add(new PlaceViewModel()
        //    {
            string price = "";
            for (int i = 0; i < current.getPrice();i++)
            {
                price += "₴";
            }
                Image.Source = new BitmapImage(new Uri(current.getImageName(), UriKind.Absolute));
                Price.Text = price;
                Description.Text = current.getDescription();
                Web.Content = getShortWebUrl(current.getWeb());
                Adress.Text = current.getStreet();
                SubwayColor.Background = new SolidColorBrush(current.getSubway().getColor());
                SubwayName.Text = current.getSubway().getName();
         //   });
        //    list.DataContext = Item; 
                localMap.Center = current.getCoordinates();
                localMap.ZoomLevel = 15;
                Pushpin pin = new Pushpin()
                        {
                            Content = current.getName(),
                            Margin = new Thickness(0,-60,0,0)
                        };
                MapOverlay overlay = new MapOverlay()
                {
                    GeoCoordinate = current.getCoordinates(),
                    Content = pin    
                };
                MapLayer layer = new MapLayer();
                layer.Add(overlay);
                localMap.Layers.Add(layer);
        }
        
        private void Web_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
            wbt.Uri = new Uri(current.getWeb());
            wbt.Show();
        }

    }
}