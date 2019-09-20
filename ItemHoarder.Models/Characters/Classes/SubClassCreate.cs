using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class SubClassCreate
    {
        public GameType GameType { get; set; }
        public string SubClassName { get; set; }
        public string SubClassDesc { get; set; }
        public List<int> ParentClassIDs { get; set; }
        public List<int> FeatureIDs { get; set; }
        public List<int> AppliedSpells { get; set; }
        public List<int> SpellIDs { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
    }
}
