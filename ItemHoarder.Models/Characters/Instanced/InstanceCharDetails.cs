using ItemHoarder.Models.Characters.Skeleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceCharDetails
    {
        public SkeletonGMDetails CharSkeleton { get; set; }
        public string Alignment { get; set; }
        //combination from class, race, background etc
        public List<string> AllLanguages { get; set; }
        public double HitPoints { get; set; }
        public double CurrentHitPoints { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        //combination from class, race, background, etc
        public List<string> WeaponProficiencies { get; set; } //no limit
        public List<string> ArmorProficiencies { get; set; } //no limit
        public List<string> ToolProficiencies { get; set; } //no limit
        public int? BaseAttackBonus { get; set; } //attack bonus is from pathfinder, different dependent on class/race
        public int? BaseAttackBonusTwo { get; set; }
        public int? BaseAttackBonusThree { get; set; }
        public int? BaseAttackBonusFour { get; set; }
        public List<string> PersonalityTraits { get; set; }//DND two
        public string Ideals { get; set; }//DND only one
        public string Bonds { get; set; }//DND only one
        public string Flaws { get; set; }//DND only one
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
        public double CarryWeight { get; set; }
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; } //10Gp -1pp
        public int ElectrumPieces { get; set; } //2ep - 1Gp
        public int SilverPieces { get; set; } //5sp - 1 ep
        public int CopperPieces { get; set; } //10cp - 1 sp
    }
}
