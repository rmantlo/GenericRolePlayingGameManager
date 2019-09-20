using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum TypeOfMonster
    {
        Abberation,
        Beast,
        Celestial,
        Construct,
        Dragon,
        Elemental,
        Fey,
        Fiend,
        Giant,
        Humanoid,
        Monstrosity,
        Ooze,
        Plant,
        Undead,
        Other
    }
    public class Monster
    {
        [Key]
        public int MonsterID { get; set; }
        public Guid OwnerID { get; set; }
        public string MonsterName { get; set; }
        public ICollection<Photo> MonsterPhoto { get; set; }
        public string MonsterDescription { get; set; }
        public TypeOfMonster MonsterType { get; set; }
        public string Environment { get; set; }
        public TypeOfAlignment? Alignment { get; set; }
        public double ChallengeRating { get; set; }
        public int ProficiencyBonus { get; set; }
        public int ExperienceGain { get; set; }
        public int HitPoints { get; set; }
        public string Size { get; set; }
        public int? Speed { get; set; }
        public int? WaterSpeed { get; set; }
        public int? FlySpeed { get; set; }
        public int? BurrowSpeed { get; set; }
        public int? ClimbSpeed { get; set; }
        public int ArmorClass { get; set; }
        //different from char -- "Con +6|Int +8" numbers preset, not determined my statmods
        public string SavingThrows { get; set; }
        public string Languages { get; set; }
        public string Senses { get; set; }
        public ICollection<MonsterSkills> Skills { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public int StrMod { get; set; }
        public int DexMod { get; set; }
        public int ConMod { get; set; }
        public int IntMod { get; set; }
        public int WisMod { get; set; }
        public int ChaMod { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
