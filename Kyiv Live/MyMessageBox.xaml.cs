using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Kyiv_Live.ViewModels;

namespace Kyiv_Live
{
    public partial class MyMessageBox : UserControl
    {
        List<KLTag> chosenTags = App.ViewModel.chosenTags;
        KLTagBase b = new KLTagBase();
        public MyMessageBox()
        {
            this.Visibility = Visibility.Collapsed;
            InitializeComponent();
        }
        
        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chosenTags.Add(b.getTags()[(list.SelectedItem as TagModel).id]);
            App.ViewModel.LoadTags();
            App.ViewModel.LoadData();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
