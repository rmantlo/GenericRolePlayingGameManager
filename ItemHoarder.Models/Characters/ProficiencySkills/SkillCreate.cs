using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.ProficiencySkills
{
    public class SkillCreate
    {
        public GameType GameTag { get; set; }
        public string SkillName { get; set; }
        public int? SkillRank { get; set; }//PF
        public string Description { get; set; }
        public string SpecialInfo { get; set; }
        public string ActionType { get; set; }
        public string AbilityStatApplied { get; set; }
        public string SkillChecks { get; set; }
        public List<int> ClassesIDs { get; set; }
        public List<int> RacesIDs { get; set; }
        public List<int> BackgroundsIDs { get; set; }
        public bool? TrainedOnly { get; set; }
        public bool? ArmorCheckPenalty { get; set; }
        public string AttemptDetails { get; set; }//PF, some skills can try again, others only one time
        public string Restrictions { get; set; }//PF, restrictions for skill
    }
}
