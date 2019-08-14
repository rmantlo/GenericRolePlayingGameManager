using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Monsters
{
    public class MonsterSkillDisplay
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        public string StatApplied { get; set; }
        public int ModStatNumber { get; set; }
    }
}
