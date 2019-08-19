using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.ItemStuff
{
    public class InventoryItem
    {
        [Key]
        public int ItemID { get; set; }
        [ForeignKey("OriginalItem")]
        public int OriginalItemID { get; set; }
        public virtual Item OriginalItem { get; set; }
        public Guid CreatorID { get; set; }
        //pick random num between array nums [fragile value, resilient value]
        public double ActualHitPoints { get; set; }
        public bool IsEquipted { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
