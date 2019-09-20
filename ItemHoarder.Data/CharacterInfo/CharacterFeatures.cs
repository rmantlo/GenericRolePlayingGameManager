using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterFeatures
    {
        [Key]
        public int FeatureID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameTag { get; set; }
        public string FeatureName { get; set; }
        public string Description { get; set; }
        public string RaceIdPrerequisites { get; set; }
        public string ClassIdPrerequisites { get; set; }
        public string FeatureIdPrerequisites { get; set; }
        public string StatPrerequisite { get; set; }
        public int? LvlPrerequisite { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
