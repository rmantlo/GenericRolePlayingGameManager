﻿using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Instanced
{
    public class InstanceCreate
    {
        public int CharSkeletonID { get; set; }
        public int ClassID { get; set; }
        public int RaceID { get; set; }
        public int BackgroundID { get; set; }
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