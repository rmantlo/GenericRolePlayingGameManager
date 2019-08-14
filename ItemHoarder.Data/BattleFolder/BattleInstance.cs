using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.BattleFolder
{
    public class BattleInstance
    {
        [Key]
        public int BattleID { get; set; }
        public int RoomID { get; set; }
        [DefaultValue(false)]
        public bool IsCurrent { get; set; }
        public ICollection<BattleMonsters> Monsters { get; set; }
        public ICollection<BattleItem> ItemDrops { get; set; }
        public ICollection<BattleRandomItem> ItemRandomDrops { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
