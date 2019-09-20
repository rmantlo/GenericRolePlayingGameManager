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
    public class CharacterRace
    {
        [Key]
        public int RaceID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string ApiRaceID { get; set; }
        public GameType GameTag { get; set; }
        public string RaceName { get; set; }
        public string VisualDescription { get; set; }
        public string WeaponProficiencies { get; set; } //in PF this is offensive racial traits
        public string ArmorProficiencies { get; set; }
        public string ToolProficiencies { get; set; }
        public string DefensiveRacialTrait { get; set; }//PF
        public string FeatRacialTrait { get; set; }//PF ex: elves get +2 racial bonus on perception checks
        public string MagicalRacialTrait { get; set; }//PF ex: elves get +2 racial bonus on caster lvl and spellcraft skill checks
        public string SensesRacialTrait { get; set; }//PF ex: elves see farther in dark than humans
        public double Speed { get; set; }
        public string Size { get; set; }
        public string Languages { get; set; }
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
