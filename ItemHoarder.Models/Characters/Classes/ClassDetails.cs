using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class ClassDetails
    {
        public int ClassID { get; set; }
        public GameType GameType { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string HitDie { get; set; }
        public List<string> SavingThrows { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public Dictionary<int, string> SubClasses { get; set; } //id and name
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
