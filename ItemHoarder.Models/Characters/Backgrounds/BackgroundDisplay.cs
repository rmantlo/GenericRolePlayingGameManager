using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Backgrounds
{
    public class BackgroundDisplay
    {
        public int BackgroundID { get; set; }
        public string BackgroundName { get; set; }
        public string BackgroundDescription { get; set; }
        public string Proficiencies { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
