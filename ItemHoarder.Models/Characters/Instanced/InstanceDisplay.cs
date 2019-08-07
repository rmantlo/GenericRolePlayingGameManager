using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.ItemInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceDisplay
    {
        public int CharInstanceID { get; set; }
        public CharSkeleDisplay CharSkeleton { get; set; }
        public RaceDisplay Race { get; set; }
        public ClassDisplay Class { get; set; }
        public BackgroundDisplay Background { get; set; }
        public List<FeatureDisplay> Features { get; set; }
        public List<SkillDisplay> ProficiencySkills { get; set; }
        public List<InstanceItemDisplay> InventoryItems { get; set; }
        public TypeOfAlignment Alignment { get; set; }
        public string AttacksAndSpells { get; set; }
        public double HitPoints { get; set; }
        public double CurrentHitPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public double CarryWeight { get; set; }
        public int GoldPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }
        public string CharacterNotes { get; set; }
    }
}
