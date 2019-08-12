using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Features
{
    public class FeatureDisplay
    {
        public int FeatureID { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public int? RaceIdPrerequisite { get; set; }
        public int? ClassIdPrerequisite { get; set; }
        public string StatPrerequisite { get; set; }
        public int? LvlPrerequisite { get; set; }
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
