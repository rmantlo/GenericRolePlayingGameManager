using ItemHoarder.Models.ItemInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Chests
{
    public class ChestDetails
    {
        public int ChestID { get; set; }
        public List<GMItemDetails> ItemDropList { get; set; }
        public List<GMItemDetails> RandomItemDropList { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
