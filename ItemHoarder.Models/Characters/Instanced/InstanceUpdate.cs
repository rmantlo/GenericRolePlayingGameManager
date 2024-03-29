﻿using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceUpdate
    {
        public TypeOfAlignment Alignment { get; set; }
        public string OtherLanguages { get; set; }
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
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; } //10Gp -1pp
        public int ElectrumPieces { get; set; } //2ep - 1Gp
        public int SilverPieces { get; set; } //5sp - 1 ep
        public int CopperPieces { get; set; } //10cp - 1 sp
        public string CharacterNotes { get; set; }
    }
}
