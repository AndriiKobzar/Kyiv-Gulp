using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kyiv_Live.ViewModels
{
    public class TagModel
    {
        public SolidColorBrush Colour
        {
            get;
            set;
        }
        public string TagName
        {
            get;
            set;
        }

        public int id
        {
            get;
            set;
        }
    }
}
