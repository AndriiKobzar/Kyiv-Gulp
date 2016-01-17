using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace Kyiv_Live
{
    public partial class AddPlacePage : PhoneApplicationPage
    {
        public  AddPlacePage()
        {
            InitializeComponent();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmailComposeTask ect = new EmailComposeTask();
            ect.Body = "Name:"+NameBox.Text+"\nDescription"+DescriptionBox.Text;
            ect.Subject = "Kyiv Live";  
            ect.Bcc = "nsldevelopers@gmail.com";
            ect.Show();
        }

    
    }
}