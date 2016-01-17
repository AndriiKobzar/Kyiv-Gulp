using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyiv_Live
{
    public class KLTagBase
    {
        private List<KLTag> tags = new List<KLTag>();
        private string path = "Assets/Data/tags.xml";

        public KLTagBase()
        {
            readTagsBase();
        }

        private void readTagsBase()
        {
            XDocument doc = XDocument.Load(path);
            foreach (XElement tagEl in doc.Root.Elements())
            {
                KLTag tag = new KLTag();
                foreach(XElement el in tagEl.Elements())
                {
                    if (el.Name == "id") tag.setID(el.Value); else
                    if (el.Name == "name_ua") tag.setNameUA(el.Value); else 
                    if (el.Name == "name_ru") tag.setNameRU(el.Value); else
                    if (el.Name == "color") tag.setColor(el.Value);
                }
                tags.Add(tag);
            }
        }

        public KLTag getTagByID(string ID)
        {
            KLTag tag = new KLTag();
            foreach(KLTag t in this.tags)
            {
                if (t.getID().Equals(ID,StringComparison.InvariantCultureIgnoreCase))
                {
                    tag = t;
                    break;
                }
            }
            return tag;
        }

        public List<KLTag> getTags()
        {
            return this.tags;
        }

    }
}
