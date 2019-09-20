using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Chests
{
    public class ChestCreate
    {
        public List<int> ListOfItemIDsWillDrop { get; set; }
        public List<int> ListOfItemIDsRandnomDrop { get; set; }
    }
}
