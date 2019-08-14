using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.BattleInstances
{
    public class BattleGMDisplay
    {
        public int BattleID { get; set; }
        public int RoomID { get; set; }
        public bool IsCurrent { get; set; }
        public List<MonsterDisplay> MonsterList { get; set; }
        public List<ItemDisplay> ItemDropList { get; set; }
        public List<ItemDisplay> RandomItemDropList { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
