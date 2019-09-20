using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class CharacterBackground
    {
        [Key]
        public int BackgroundID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public GameType GameTag { get; set; }
        public string BackgroundName { get; set; }
        public string BackgroundDescription { get; set; }//put languages, and equiptment in here
        public string WeaponProficiencies { get; set; }
        public string ArmorProficiencies { get; set; }
        public string ToolProficiencies { get; set; }
        public string SkillIDs { get; set; } //list of skillIDs that apply to this background
        public string FeatureIDs { get; set; }//list of feat IDs that apply to this background
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
