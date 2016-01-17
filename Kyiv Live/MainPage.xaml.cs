using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Kyiv_Live.ViewModels;
using Microsoft.Phone.Maps.Toolkit;
using Windows.Devices.Geolocation;
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace Kyiv_Live
{
    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinate userCoordinate = new GeoCoordinate();
        MyMessageBox box = new MyMessageBox();
     //   Geolocator geolocator = null;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            App.ViewModel.LoadTags();
            LayoutRoot.Children.Add(box);
            
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            setMapCenter();
            map.ZoomLevel = 16;
            map.Layers.Clear();
            MapLayer layer = App.ViewModel.layer;
            map.Layers.Add(layer);
         //   getUserLocation();
       //     setMapCenter();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (list.SelectedItem as ItemViewModel).ID;
            PhoneApplicationService.Current.State["currentPlaceIndex"] = id;
            NavigationService.Navigate(new Uri("/PlaceInfoPage.xaml", UriKind.Relative));
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPlacePage.xaml", UriKind.Relative));
        }

        private void SearchButtonClick(object sender, EventArgs e)
        {
            box.Show();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            App.ViewModel.chosenTags.Clear();
            App.ViewModel.ChosenTags.Clear();
            App.ViewModel.LoadData();
            App.ViewModel.LoadTags();
        }

        private void pivotMain_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            if (e.Item == lst)
                ApplicationBar.IsVisible = true;
            else
                ApplicationBar.IsVisible = false;
        }


        async void setMapCenter()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                userCoordinate = geoposition.Coordinate.ToGeoCoordinate();
                map.Center = userCoordinate;
                setUserLayer();
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    // the application does not have the right capability or the location master switch is off
                    MessageBox.Show("Location  is disabled in phone settings.");
                }
                //else
                {
                    MessageBox.Show("some problems");
                    // something else happened acquring the location
                }
            }
        }

      /*  void getUserLocation()
        {
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"])
            {
                // The user has opted out of Location.
                return;
            }
             
                     geolocator = new Geolocator();
                     geolocator.DesiredAccuracy = PositionAccuracy.High;
                     geolocator.MovementThreshold = 100; // The units are meters.
            
                     geolocator.StatusChanged += geolocator_StatusChanged;
                     geolocator.PositionChanged += geolocator_PositionChanged;
                    
                     setUserLayer();
         //       MessageBox.Show("location found");
            }
      

        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";

            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    // the application does not have the right capability or the location master switch is off
                    status = "location is disabled in phone settings";
                    break;
                case PositionStatus.Initializing:
                    // the geolocator started the tracking operation
                    status = "initializing";
                    break;
                case PositionStatus.NoData:
                    // the location service was not able to acquire the location
                    status = "no data";
                    break;
                case PositionStatus.Ready:
                    // the location service is generating geopositions as specified by the tracking parameters
                    status = "ready";
                    break;
                case PositionStatus.NotAvailable:
                    status = "not available";
                    // not used in WindowsPhone, Windows desktop uses this value to signal that there is no hardware capable to acquire location information
                    break;
                case PositionStatus.NotInitialized:
                    // the initial state of the geolocator, once the tracking operation is stopped by the user the geolocator moves back to this state

                    break;
            }

            Dispatcher.BeginInvoke(() =>
            {
             //  show something about changing status
            });
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                userCoordinate = args.Position.Coordinate.ToGeoCoordinate();
                setUserLayer();
            });
        }
        */
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (box.Visibility == Visibility.Visible)
            {
                box.Visibility = Visibility.Collapsed;
                e.Cancel = true;
            } else
                if(App.ViewModel.chosenTags.Count>0)
                {
                    App.ViewModel.chosenTags.Clear();
                    App.ViewModel.ChosenTags.Clear();
                    App.ViewModel.LoadData();
                    App.ViewModel.LoadTags();
                    e.Cancel = true;
                }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            box.Visibility = Visibility.Collapsed;

        }

        private void setUserLayer()
        {
            
            MapLayer userLayer = new MapLayer();
            MapOverlay overlay = new MapOverlay()
            {
                GeoCoordinate = userCoordinate,
                Content = new UserLocationMarker()
                {
                    GeoCoordinate = userCoordinate,
                    Margin = new Thickness(-18, -18, 0, 0)
                }
            };
            userLayer.Add(overlay);
            if (map.Layers.Count == 2)
            {
                map.Layers[1].Clear();
            }
            map.Layers.Add(userLayer);
        }

        private void AdView_ReceivedAd(object sender, GoogleAds.AdEventArgs e)
        {
            Debug.WriteLine("download completed");
            (sender as GoogleAds.AdView).Visibility = Visibility.Visible;
        }

        private void AdView_FailedToReceiveAd(object sender, GoogleAds.AdErrorEventArgs e)
        {
            Debug.WriteLine("download failed");
            (sender as GoogleAds.AdView).Visibility = Visibility.Collapsed;
        }

        private void ListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           // IsolatedStorageSettings.ApplicationSettings["language"] = langPicker.SelectedIndex;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void map_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            foreach(MapOverlay overlay in map.Layers[0])
            {
                KLPushpin p = (overlay.Content as KLPushpin);
                if (p.isVisible)
                {
                    if (!p.isSender)
                    {
                        p.Collapse();
                        
                    }
                    p.isSender = false;
                }
            }
        }
    }
}