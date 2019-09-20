using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Classes
{
    public class SubClassDetails
    {
        public int SubClassID { get; set; }
        public GameType GameType { get; set; }
        public string SubClassName { get; set; }
        public string SubClassDesc { get; set; }
        public Dictionary<int, string> ParentClasses { get; set; }
        public Dictionary<int, string> FeatureIDs { get; set; }
        public Dictionary<int, string> AppliedSpells { get; set; }
        public Dictionary<int, string> SpellIDs { get; set; }
        public List<string> WeaponProficiencies { get; set; }
        public List<string> ArmorProficiencies { get; set; }
        public List<string> ToolProficiencies { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
