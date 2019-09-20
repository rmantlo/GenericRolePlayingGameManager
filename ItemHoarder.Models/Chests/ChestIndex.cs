using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Chests
{
    public class ChestIndex
    {
        public int ChestID { get; set; }
        public int NumberOfItemsWillDrop { get; set; }
        public int NumberOfRandomItemsMayDrop { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
