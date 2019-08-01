using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.ItemStuff
{
    public class Inventory
    {
        [Key]
        public int ID { get; set; }
        public int CharInstanceID { get; set; }
        public bool IsEquipted { get; set; }
        //[ForeignKey("Item")]
        //public int ItemID { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
