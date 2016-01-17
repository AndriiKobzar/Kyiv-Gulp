using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.IO;
using System.IO.IsolatedStorage;

namespace Kyiv_Live
{
    public class KLData
    {
        private const string PATH = "Assets/Data/data.xml";
        public List<KLPlace> places = new List<KLPlace>();
        private KLTagBase tagBase = new KLTagBase();
        private KLSubwayBase subBase = new KLSubwayBase();
        public KLData()
        {
            readFromAssets();
        }

        private void readFromAssets()
        {
            
           //     string DATA = "";
         //       loadStringAsync(DATA);
         //       XDocument doc = XDocument.Load(PATH);
           //     DATA.Start();
          //      DATA.Wait();
                if (IsolatedStorageSettings.ApplicationSettings.Contains("data.xml")) 
                { 
                XDocument doc = XDocument.Parse(IsolatedStorageSettings.ApplicationSettings["data.xml"].ToString());
                foreach (XElement place in doc.Root.Elements())
                {
                    KLPlace p = new KLPlace();
                    foreach (XElement e in place.Elements())
                    {
                        if (e.Name == "title") p.setName(e.Value); else
                        if (e.Name == "image") p.setImageName(e.Value); else
                        if (e.Name == "description") p.setDescription(e.Value); else
                        if (e.Name == "str_location") p.setStreet(e.Value); else
                        if (e.Name == "price") p.setPrice(e.Value); else
                        if (e.Name == "tag") p.addTag(tagBase.getTagByID(e.Value)); else
                        if (e.Name == "subway") p.setSubway(subBase.getSubwayByID(e.Value)); else
                        if (e.Name == "web") p.setWeb(e.Value); else
                        if (e.Name == "location") p.setCoordinates(e.Value); else
                        if (e.Name == "promoted") p.setPromoted(e.Value);
                    }
                    places.Add(p);
                }
                sortPlaces();
                }
           //     PhoneApplicationService.Current.State["data"] = places;
        }

        public void sortPlaces() 
        {
            places.Sort((place1, place2) => {
            /**
            *
            * @param place1
            * @param place2
            * @return 0 if equals
            * >0 if first upper, <0 if second
            */
            
            int comNames = (place1.getName().CompareTo(place2.getName()));
            int comPromoted = place1.getPromoted() - place2.getPromoted();

            // both are promoted or both are not
            // so sort by name
            if (comPromoted==0) 
            {
            return comNames;
            } else 
            { // one is promoted another no - so sort by promotion
            return comPromoted;
            }
            
        });
    }


        public List<KLPlace> getPlaces()
        {
            return this.places;
        }

    }
}