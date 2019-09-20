using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Features
{
    public class FeatureCreate
    {
        public GameType GameTag { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public List<int> RaceIdPrerequisite { get; set; }
        public List<int> ClassIdPrerequisite { get; set; }
        public List<int> FeatureIdPrerequisite { get; set; }
        public Dictionary<string, int> StatPrerequisite { get; set; }
        public int? LvlPrerequisite { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
