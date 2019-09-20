using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.FamilarsFolder;
using ItemHoarder.Data.ItemStuff;
using ItemHoarder.Data.SpellsAndOther;
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
        ChaoticEvil,
        None
    }
    public class CharacterInstanced
    {
        [Key]
        public int CharInstanceID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public int? RoomID { get; set; }
        public ICollection<Photo> CharacterPhoto { get; set; }
        [ForeignKey("CharSkeleton")]
        public int CharSkeletonID { get; set; }
        public virtual CharacterSkeleton CharSkeleton { get; set; }
        [ForeignKey("Race")]
        public int RaceID { get; set; }
        public virtual CharacterRace Race { get; set; }
        [ForeignKey("Background")]
        public int BackgroundID { get; set; }
        public virtual CharacterBackground Background { get; set; }
        public virtual ICollection<CharacterClassList> Class { get; set; }
        [ForeignKey("SubClass")]
        public int SubClassID { get; set; }
        public virtual CharacterSubClass SubClass { get; set; }
        public virtual ICollection<CharProficiencySkills> Skills { get; set; }//determined by class and race
        public virtual ICollection<CharacterFeatList> Features { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
        public virtual ICollection<CharFamilars> Familars { get; set; }
        public string OtherLanguages { get; set; }
        public string WeaponProficiencies { get; set; } //no limit
        public string ArmorProficiencies { get; set; } //no limit
        public string ToolProficiencies { get; set; } //no limit
        public string PersonalityTraits { get; set; }//DND two
        public string Ideals { get; set; }//DND only one
        public string Bonds { get; set; }//DND only one
        public string Flaws { get; set; }//DND only one
        public TypeOfAlignment Alignment { get; set; }
        public string AllLanguages { get; set; }//list
        public int? BaseAttackBonus { get; set; } //attack bonus is from pathfinder, different dependent on class/race
        public int? BaseAttackBonusTwo { get; set; }
        public int? BaseAttackBonusThree { get; set; }
        public int? BaseAttackBonusFour { get; set; }
        public double HitPoints { get; set; }
        public double CurrentHitPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        public ICollection<CharacterSpells> Spells { get; set; }
        //list of condition IDs?
        public string Conditions { get; set; }//list of names
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public double CarryWeight { get; set; }
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; } //10Gp - 1pp
        public int ElectrumPieces { get; set; } //2ep - 1Gp
        public int SilverPieces { get; set; } //5sp - 1ep
        public int CopperPieces { get; set; } //10cp - 1sp
        public string CharacterNotes { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }

    }
}
