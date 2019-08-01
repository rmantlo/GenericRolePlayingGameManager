using ItemHoarder.Data.CharacterInfo;
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

        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual CharacterClass Class { get; set; }

        [ForeignKey("Background")]
        public int BackgroundID { get; set; }
        public virtual CharacterBackground Background { get; set; }

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
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }

    }
}
