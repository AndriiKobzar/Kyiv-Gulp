using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kyiv_Live
{
    public class KLTag
    {
        private string id = null;
        private string nameUA = null;
        private string nameRU = null;
        private Color color = Colors.Yellow;
        public KLTag()
        {

        }

        public void setID(string Name)
        {
            this.id = Name;
        }
        public string getID()
        {
            return this.id;
        }
        public void setColor(string color)
        {
            bool t = App.COLOR.colors.TryGetValue("clr_" + color, out this.color);
        }

        public Color getColor()
        {
            return this.color;
        }
        public void setNameUA(string Name)
        {
            this.nameUA = Name;
        }
        public string getNameUA()
        {
            return this.nameUA;
        }
        public void setNameRU(string Name)
        {
            this.nameRU = Name;
        }
        public string getNameRU()
        {
            return this.nameRU;
        }

        public override bool Equals(object obj)
        {
            return (this.id == (obj as KLTag).id);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}
