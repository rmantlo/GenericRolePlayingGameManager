using ItemHoarder.Data;
using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.ItemInventory
{
    public class ItemIndex
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public RarityOfItem ItemRarity { get; set; }
        public ItemClass ItemClass { get; set; }
        public bool? IsEquipted { get; set; }
        public Photo ItemPhoto { get; set; }
    }
}
