using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.BattleFolder
{
    public class BattleMonsters
    {
        [Key]
        public int BattleMonsterID { get; set; }
        public Guid OwnerID { get; set; }
        public int BattleID { get; set; }
        [ForeignKey("Monster")]
        public int MonsterID { get; set; }
        public virtual Monster Monster { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
