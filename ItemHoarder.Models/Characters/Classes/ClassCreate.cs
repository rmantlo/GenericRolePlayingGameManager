using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class ClassCreate
    {
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string HitDie { get; set; }
        public string SavingThrows { get; set; }
        public string Proficiencies { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
