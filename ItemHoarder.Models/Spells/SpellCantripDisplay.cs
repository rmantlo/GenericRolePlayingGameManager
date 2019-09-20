using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Spells
{
    public class SpellCantripDisplay
    {
        public int ID { get; set; }
        public string AttackType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HigherLevelDescription { get; set; }
        public int? LevelRequired { get; set; }
        public List<int> ClassIDs { get; set; }
        public List<int> SubClassIDs { get; set; }
        public List<int> RaceIDs { get; set; }
        public string SchoolOfMagic { get; set; }
        public int SpellLevel { get; set; }
        public string DiceRollType { get; set; }
        public int Range { get; set; }
        public List<string> Components { get; set; }
        public List<string> Materials { get; set; }
        public bool Ritual { get; set; }
        public bool Concentration { get; set; }
        public string CastingTime { get; set; }
        public string Duration { get; set; }
        public string AreaOfEffect { get; set; }
        public int AreaOfEffectLength { get; set; }
        public string SavingThrow { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
