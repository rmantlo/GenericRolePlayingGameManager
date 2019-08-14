using ItemHoarder.Data.CharacterInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class MonsterSkills
    {
        [Key]
        public int MonsterSkillID { get; set; }
        public int MonsterID { get; set; }
        [ForeignKey("Skills")]
        public int SkillID { get; set; }
        public virtual ProficiencySkill Skills { get; set; }
        //proficiency bonus + saving throw # with same stat as skill's applied stat
        public int ModStat { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
