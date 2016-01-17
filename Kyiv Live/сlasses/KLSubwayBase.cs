using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kyiv_Live
{
    class KLSubwayBase
    {
        private const string path = "Assets/Data/subway.xml";
        private List<KLSubway> subwayBase = new List<KLSubway>();
        public KLSubwayBase()
        {
            readFromAssets();
        }

        private void readFromAssets()
        {
            XDocument doc = XDocument.Load(path);
            foreach(XElement station in doc.Root.Elements())
            {
                KLSubway stop = new KLSubway();
                foreach(XElement elem in station.Elements())
                {
                    if (elem.Name == "id") stop.setID(elem.Value); else
                    if (elem.Name == "title_ua") stop.setName(elem.Value); else
                    if (elem.Name == "color") stop.setColor(elem.Value);
                }
                this.subwayBase.Add(stop);
            }
        }

        public KLSubway getSubwayByID(string ID)
        {
            KLSubway subway = new KLSubway();
            foreach(KLSubway sub in this.subwayBase)
            {
                if (sub.getID().Equals(ID,StringComparison.InvariantCultureIgnoreCase))
                {
                    subway = sub;
                    break;
                }
            }
            return subway;
        }
    }
}
