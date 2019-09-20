using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Races
{
    public class RaceDetails
    {
        public int RaceID { get; set; }
        public GameType GameTag { get; set; }
        public string RaceName { get; set; }
        public string VisualDescription { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public List<string> DefensiveRacialTrait { get; set; }//PF
        public List<string> FeatRacialTrait { get; set; }//PF ex: elves get +2 racial bonus on perception checks
        public List<string> MagicalRacialTrait { get; set; }//PF ex: elves get +2 racial bonus on caster lvl and spellcraft skill checks
        public List<string> SensesRacialTrait { get; set; }//PF ex: elves see farther in dark than humans
        public double Speed { get; set; }
        public string Size { get; set; }
        public List<string> Languages { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
