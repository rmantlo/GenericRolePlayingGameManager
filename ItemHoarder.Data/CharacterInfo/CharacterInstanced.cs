using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum TypeOfAlignment
    {
        LawfulGood,
        NeutralGood,
        ChaoticGood,
        LawfulNeutral,
        TrueNeutral,
        ChaoticNeutral,
        LawfulEvil,
        NeutralEvil,
        ChaoticEvil
    }
    public class CharacterInstanced
    {
        [Key]
        public int CharInstanceID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        [ForeignKey("CharSkeleton")]
        public int CharSkeletonID { get; set; }
        public virtual CharacterSkeleton CharSkeleton { get; set; }

        [ForeignKey("Race")]
        public int RaceID { get; set; }
        public virtual CharacterRace Race { get; set; }

        [ForeignKey("Background")]
        public int BackgroundID { get; set; }
        public virtual CharacterBackground Background { get; set; }
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual CharacterClass Class { get; set; }
        public virtual ICollection<CharProficiencySkills> Skills { get; set; }
        public virtual ICollection<CharacterFeatList> Features { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
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
