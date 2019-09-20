using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.ProficiencySkills
{
    public class SkillIndex
    {
        public int SkillID { get; set; }
        public GameType GameTag { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public string AbilityStatApplied { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
