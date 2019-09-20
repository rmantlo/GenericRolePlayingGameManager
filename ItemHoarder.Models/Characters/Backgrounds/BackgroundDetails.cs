using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Backgrounds
{
    public class BackgroundDetails
    {
        public int BackgroundID { get; set; }
        public GameType GameTag { get; set; }
        public string BackgroundName { get; set; }
        public string BackgroundDescription { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public Dictionary<int, string> FeatureIDs { get; set; }
        public Dictionary<int, string> SkillIDs { get; set; }
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
