using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Kyiv_Live
{
    public partial class KLPushpin : UserControl 
    {
        public int id;
        private string _placeName = "";
        private bool isSmall = true;
        public bool isSender = false;
                        
        public string PlaceName
        {
            get
            {
                return _placeName;
            }
            set
            {
                this._placeName = value;
                this.navigateBtn.Content = this._placeName;
            }
        }

        public bool isVisible
        {
            get
            {
                if (navigateBtn.Visibility == Visibility.Visible) return true;
                else return false;
            }
        }
        
        public KLPushpin()
        {
            InitializeComponent();
            
        }

        private void navigateBtn_Click(object sender, RoutedEventArgs e)
        {
            PhoneApplicationService.Current.State["currentPlaceIndex"] = id;
            App.RootFrame.Navigate(new Uri("/PlaceInfoPage.xaml", UriKind.Relative));
        }

        protected override void OnTap(System.Windows.Input.GestureEventArgs e)
        {
            
            if(isSmall)
            {
                navigateBtn.Visibility = Visibility.Visible;
                this.Margin = new Thickness(0, -80, 0, 0);
                isSmall = false;
                isSender = true;
            }
            else
            {
                Collapse();

            }
            
        }

        public void Collapse()
        {
            navigateBtn.Visibility = Visibility.Collapsed;
            this.Margin = new Thickness(0, -60, 0, 0);
            isSmall = true;
            isSender = false;
        }
    }
}
