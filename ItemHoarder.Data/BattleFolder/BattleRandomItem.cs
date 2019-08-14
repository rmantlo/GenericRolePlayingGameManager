using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.BattleFolder
{
    public class BattleRandomItem
    {
        [Key]
        public int BattleItemID { get; set; }
        public Guid OwnerID { get; set; }
        public int BattleID { get; set; }
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
