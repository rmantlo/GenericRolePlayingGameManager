using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Races
{
    public class RaceCreate
    {
        public string Name { get; set; }
        public double Speed { get; set; }
        public string Size { get; set; }
        public string Languages { get; set; }
        public string Trait { get; set; }
        public string TraitDescription { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
