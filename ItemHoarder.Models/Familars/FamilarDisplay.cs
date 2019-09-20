using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Familars
{
    public class FamilarDisplay
    {
        public int FamilarID { get; set; }
        public string PetName { get; set; }
        public Photo PetPhoto { get; set; }
        public string FamilarName { get; set; }
        public string FamilarType { get; set; }
        public string VisualDescription { get; set; }
        public string SpecialEffects { get; set; }
        public List<Dictionary<string, string>> FamilarFeatures { get; set; }
        public bool IsImproved { get; set; } //DnD for stronger familars
        public string Alignment { get; set; }//for improved familars
        public int? LevelRequirement { get; set; }//dnd for improved familars
        public int HitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public string HitDie { get; set; }
        public int Initiative { get; set; }
        public int Speed { get; set; }
        public int ArmorClass { get; set; }
        public int BaseAttack { get; set; }
        public int Grapple { get; set; }
        public int FortitudeSave { get; set; }
        public int ReflexSave { get; set; }
        public int WillSave { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
