﻿using ItemHoarder.Data.RoomFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterRace
    {
        [Key]
        public int RaceID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string ApiRaceID { get; set; }
        public GameType GameTag { get; set; }
        public string Name { get; set; }
        public string Proficiencies { get; set; } //weapons, armors and language
        public double Speed { get; set; }
        public string Size { get; set; }
        public string Languages { get; set; }
        public string Trait { get; set; }
        public string TraitDescription { get; set; }
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
