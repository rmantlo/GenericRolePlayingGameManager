using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum AttackType
    {
        Spell,
        Cantrip
    }
    public enum AreaOfEffectType
    {
        Cone,
        Cube,
        Cylinder,
        Line,
        Sphere,
        Other,
        None
    }
    public enum SchoolsOfMagic
    {
        Abjuration,
        Conjuration,
        Divination,
        Enchantment,
        Evocation,
        Illusion,
        Necromancy,
        Transmutation,
        Other,
        None
    }
    public class Spells
    {
        [Key]
        public int AttackID { get; set; }
        public AttackType AttackType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AtHigherLevelsDescription { get; set; }
        public int? LevelRequired { get; set; }
        //multiple classes/subs/races can use, string of IDs?
        public string ClassRequiredID { get; set; }
        public string SubClassRequiredID { get; set; }
        public string RaceRequiredID { get; set; }
        public SchoolsOfMagic SchoolOfMagic { get; set; }
        public int SpellLevel { get; set; }
        public string DiceRollType { get; set; }
        public int Range { get; set; } //in feet (like speed)
        public string CastingTime { get; set; }
        public string Duration { get; set; }
        public AreaOfEffectType AreaOfEffect { get; set; }
        public int AreaOfEffectLength { get; set; } //in feet
        //target can make saving throw
        public string SavingThrow { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
