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
        Other
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
        public int? ClassRequiredID { get; set; }
        public int? SubClassRequiredID { get; set; }
        public int? RaceRequiredID { get; set; }
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
