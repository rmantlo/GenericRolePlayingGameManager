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
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public CharSkeleDisplay CharSkeleton { get; set; }
        public RaceDisplay Race { get; set; }
        public ClassDisplay Class { get; set; }
        public BackgroundDisplay Background { get; set; }
        public List<FeatureDisplay> Features { get; set; }
        public List<SkillDisplay> ProficiencySkills { get; set; }
        public List<InstanceItemDisplay> InventoryItems { get; set; }
        public TypeOfAlignment Alignment { get; set; }
        public string OtherLanguages { get; set; }
        public string AttacksAndSpells { get; set; }
        public double HitPoints { get; set; }
        public double CurrentHitPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public int ProficiencyBonus { get; set; } //dependent on lvl, reset on update method
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public int StrMod { get; set; }
        public int DexMod { get; set; }
        public int ConMod { get; set; }
        public int IntMod { get; set; }
        public int WisMod { get; set; }
        public int ChaMod { get; set; }
        public double CarryWeight { get; set; }
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; } //10Gp -1pp
        public int ElectrumPieces { get; set; } //2ep - 1Gp
        public int SilverPieces { get; set; } //5sp - 1 ep
        public int CopperPieces { get; set; } //10cp - 1 sp
        public string CharacterNotes { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
