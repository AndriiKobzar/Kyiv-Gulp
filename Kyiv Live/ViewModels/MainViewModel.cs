using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Kyiv_Live.Resources;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows;

namespace Kyiv_Live.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public KLData data;
        public KLTagBase tagBase = new KLTagBase();
        public MapLayer layer = new MapLayer();
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Tagslist = new ObservableCollection<TagModel>();
  //          this.PlaceItem = new PlaceViewModel();
            this.ChosenTags = new ObservableCollection<TagModel>();
        }
        
        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<TagModel> Tagslist { get; private set; }
   //     public PlaceViewModel PlaceItem { get; set; }
        public List<KLTag> chosenTags = new List<KLTag>();
        public ObservableCollection<TagModel> ChosenTags { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            int i = 0;
            data = new KLData();
            Items.Clear();
            foreach (KLPlace place in data.getPlaces())
            {
                if (chosenTags.Count == 0)
                {
                    loadPlace(place, i);
                } else
                if (containsChosenTags(place))
                {
                    loadPlace(place, i);    
                } 
                MapOverlay overlay = new MapOverlay()
                {
                    GeoCoordinate = place.getCoordinates(),
                    Content = new KLPushpin()
                    {
                        id = i,
                        
                        PlaceName = place.getName(),
                        Margin = new Thickness(0, -60, 0, 0)
                    }
                };
            layer.Add(overlay);       
                i++;
            }
            this.IsDataLoaded = true;
        }

        private void loadPlace(KLPlace place, int i)
        {
            List<TagModel> tagsModel = new List<TagModel>();
            foreach (KLTag tag in place.getTags())
            {
                tagsModel.Add(new TagModel()
                {
                    TagName = tag.getNameUA(),
                    Colour = new SolidColorBrush(tag.getColor())
                });
            }
            this.Items.Add(new ItemViewModel()
            {
                LineOne = place.getName(),
                Tag = tagsModel,
                SubwayColor = new SolidColorBrush(place.getSubway().getColor()),
                //    SubwayColor = new SolidColorBrush(Colors.Blue),
                ID = i
            });
        }

        public void LoadTags()
        {
            Tagslist.Clear();
            ChosenTags.Clear();
            int i = 0;
            if (chosenTags.Count==0)
            {
                foreach (KLTag tag in tagBase.getTags())
                {
                    Tagslist.Add(new TagModel() { TagName = tag.getNameUA(), id = i++ });
                }
            } else
            {
                foreach (KLTag tag in tagBase.getTags())
                {
                    if(!chosenTags.Contains(tag)) Tagslist.Add(new TagModel() { TagName = tag.getNameUA(), id = i++ });
                    else
                    {
                        ChosenTags.Add(new TagModel() { TagName = tag.getNameUA(), Colour=new SolidColorBrush(tag.getColor()), id = i++ });
                    }
                }
            }
        }

        private bool containsChosenTags(KLPlace place)
        {
            bool rezult = true;
            foreach(KLTag tag in chosenTags)
            {
                if (!place.getTags().Contains(tag))
                {
                    rezult = false;
                    break;
                }
            }
            return rezult;
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