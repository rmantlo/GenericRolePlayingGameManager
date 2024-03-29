﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharProficiencySkills
    {
        [Key]
        public int ID { get; set; }
        public int CharInstanceID { get; set; }
        [ForeignKey("Skills")]
        public int SkillID { get; set; }
        public virtual CharacterSkill Skills { get; set; }
        public int? SkillRank { get; set; }//PF
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
