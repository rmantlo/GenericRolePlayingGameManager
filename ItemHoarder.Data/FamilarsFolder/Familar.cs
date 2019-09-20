using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.FamilarsFolder
{
    public class Familar
    {
        [Key]
        public int FamilarID { get; set; }
        public string FamilarName { get; set; }
        public ICollection<Photo> PetPhoto { get; set; }
        public string VisualDescription { get; set; }
        public string SpecialEffects { get; set; }
        public bool IsImproved { get; set; } //DnD for stronger familars
        public TypeOfAlignment Alignment { get; set; }//for improved familars
        public int? LevelRequirement { get; set; }//dnd for improved familars
        public string FamilarType { get; set; }
        public ICollection<FamilarFeature> FamilarFeatures { get; set; }
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
