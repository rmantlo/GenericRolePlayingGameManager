using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterSkill
    {
        [Key]
        public int SkillID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameTag { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public string SpecialInfo { get; set; }
        public string ClassesIDs { get; set; }
        public string RacesIDs { get; set; }
        public string BackgroundsIDs { get; set; }
        public string AbilityStatApplied { get; set; }
        public string SkillChecks { get; set; }//dnd more desc examples
        public bool? TrainedOnly { get; set; }//PF
        public bool? ArmorCheckPenalty { get; set; }//PF
        public string ActionType { get; set; }//?? maybe just include this in description?
        public string AttemptDetails { get; set; }//PF, some skills can try again, others only one time
        public string Restrictions { get; set; }//PF, restrictions for skill
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
