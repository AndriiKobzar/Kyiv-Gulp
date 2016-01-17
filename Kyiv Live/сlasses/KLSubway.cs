using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kyiv_Live
{
    public class KLSubway
    {
        private string id = "";
        private string nameUA = "";
        
        private Color colour = Colors.Red;
        public KLSubway()
        {

        }

        public string getID()
        {
            return this.id;
        }

        public void setID(string ID)
        {
            this.id = ID;
        }

        public Color getColor()
        {
            return this.colour;
        }

        public void setColor(string color)
        {
            bool t = App.COLOR.colors.TryGetValue("clr_subway_" + color, out this.colour);
        }

        public void setName(string p)
        {
            this.nameUA = p;
        }

        public string getName()
        {
            return this.nameUA;
        }
    }
}
