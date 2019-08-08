using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.ItemStuff;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.ItemInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    //As player controlling their characters
    public class CharacterInstancedService
    {
        private readonly Guid _userId;
        public CharacterInstancedService(Guid userId)
        {
            _userId = userId;
        }
        //get all my character instance information
        public IEnumerable<InstanceDisplay> GetAllMyInstancedCharacters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var characters = ctx.CharacterInstances.Where(e => e.OwnerID == _userId).ToList();
                List<InstanceDisplay> charList = new List<InstanceDisplay>();
                foreach (var c in characters)
                {
                    //var inventory = ctx.Inventories.Single(e => e.CharInstanceID == c.CharInstanceID);
                    List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                    foreach (var i in c.InventoryItems)
                    {
                        var item = new InstanceItemDisplay
                        {
                            ItemID = i.ItemID,
                            Name = i.Name,
                            Description = i.Description,
                            Weight = i.Weight,
                            HitPoints = i.HitPoints,
                            ItemRarity = i.ItemRarity,
                            ItemClass = i.ItemClass,
                            Damage = i.Damage,
                            DamageResiliance = i.DamageResiliance,
                            IsEquiptable = i.IsEquiptable,
                            Strength = i.Strength,
                            Dexterity = i.Dexterity,
                            Constitution = i.Constitution,
                            Intelligence = i.Intelligence,
                            Wisdom = i.Wisdom,
                            Charisma = i.Charisma
                        };
                        itemList.Add(item);
                    }
                    var features = ctx.CharacterFeatList.Single(e => e.CharInstanceID == c.CharInstanceID);
                    List<FeatureDisplay> featList = new List<FeatureDisplay>();
                    foreach (var f in features.Features)
                    {
                        var feature = new FeatureDisplay
                        {
                            FeatureID = f.FeatureID,
                            FeatureName = f.FeatureName,
                            Description = f.Description,
                            RaceIdPrerequisite = f.RaceIdPrerequisite,
                            ClassIdPrerequisite = f.ClassIdPrerequisite,
                            StatPrerequisite = f.StatPrerequisite,
                            LvlPrerequisite = f.LvlPrerequisite,
                            Strength = f.Strength,
                            Dexterity = f.Dexterity,
                            Constitution = f.Constitution,
                            Intelligence = f.Intelligence,
                            Wisdom = f.Wisdom,
                            Charisma = f.Charisma
                        };
                        featList.Add(feature);
                    }
                    var skills = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == c.CharInstanceID);
                    List<SkillDisplay> skillList = new List<SkillDisplay>();
                    foreach (var s in skills.Skills)
                    {
                        var skill = new SkillDisplay
                        {
                            ID = s.ID,
                            Name = s.Name,
                            Description = s.Description,
                            ClassesApplied = s.ClassesApplied,
                            RacesApplied = s.RacesApplied,
                            BackgroundsApplied = s.BackgroundsApplied,
                            StatApplied = s.StatApplied,
                            Strength = s.Strength,
                            Dexterity = s.Dexterity,
                            Constitution = s.Constitution,
                            Intelligence = s.Intelligence,
                            Wisdom = s.Wisdom,
                            Charisma = s.Charisma
                        };
                        skillList.Add(skill);
                    }
                    var room = ctx.Rooms.SingleOrDefault(e => e.RoomID == c.RoomID);
                    string roomName = null;
                    if (room == null)
                    {
                        roomName = null;
                    }
                    else roomName = room.RoomName;
                    var charItem = new InstanceDisplay
                    {
                        CharInstanceID = c.CharInstanceID,
                        RoomID = c.RoomID,
                        RoomName = roomName,
                        CharSkeleton = new CharSkeleDisplay
                        {
                            ID = c.CharSkeletonID,
                            FirstName = c.CharSkeleton.FirstName,
                            LastName = c.CharSkeleton.LastName,
                            Gender = c.CharSkeleton.Gender,
                            VisualDescription = c.CharSkeleton.VisualDescription,
                            BackgroundDescription = c.CharSkeleton.BackgroundDescription,
                            CharacterNotes = c.CharSkeleton.CharacterNotes,
                            HeightInInches = c.CharSkeleton.HeightInInches,
                            WeightInPounds = c.CharSkeleton.WeightInPounds,
                            PersonalityTraits = c.CharSkeleton.PersonalityTraits,
                            Ideals = c.CharSkeleton.Ideals,
                            Bonds = c.CharSkeleton.Bonds,
                            Flaws = c.CharSkeleton.Flaws
                        },
                        Race = new RaceDisplay
                        {
                            RaceID = c.RaceID,
                            Name = c.Race.Name,
                            Speed = c.Race.Speed,
                            Size = c.Race.Size,
                            Languages = c.Race.Languages,
                            Proficiencies = c.Race.Proficiencies,
                            Trait = c.Race.Trait,
                            TraitDescription = c.Race.TraitDescription,
                            Strength = c.Race.Strength,
                            Dexterity = c.Race.Dexterity,
                            Constitution = c.Race.Constitution,
                            Intelligence = c.Race.Intelligence,
                            Wisdom = c.Race.Wisdom,
                            Charisma = c.Race.Charisma
                        },
                        Class = new ClassDisplay
                        {
                            ClassID = c.ClassID,
                            ClassName = c.Class.ClassName,
                            ClassDescription = c.Class.ClassDescription,
                            HitDie = c.Class.HitDie,
                            SavingThrows = c.Class.SavingThrows,
                            Proficiencies = c.Class.Proficiencies,
                            Strength = c.Class.Strength,
                            Dexterity = c.Class.Dexterity,
                            Constitution = c.Class.Constitution,
                            Intelligence = c.Class.Intelligence,
                            Wisdom = c.Class.Wisdom,
                            Charisma = c.Class.Charisma
                        },
                        Background = new BackgroundDisplay
                        {
                            BackgroundID = c.BackgroundID,
                            BackgroundName = c.Background.BackgroundName,
                            BackgroundDescription = c.Background.BackgroundDescription,
                            Proficiencies = c.Background.Proficiencies,
                            Strength = c.Background.Strength,
                            Dexterity = c.Background.Dexterity,
                            Constitution = c.Background.Constitution,
                            Intelligence = c.Background.Intelligence,
                            Wisdom = c.Background.Wisdom,
                            Charisma = c.Background.Charisma
                        },
                        Features = featList,
                        ProficiencySkills = skillList,
                        InventoryItems = itemList,
                        Alignment = c.Alignment,
                        OtherLanguages = c.OtherLanguages,
                        AttacksAndSpells = c.AttacksAndSpells,
                        HitPoints = c.HitPoints,
                        CurrentHitPoints = c.CurrentHitPoints,
                        ExperiencePoints = c.ExperiencePoints,
                        Level = c.Level,
                        Strength = c.Strength,
                        Dexterity = c.Dexterity,
                        Constitution = c.Constitution,
                        Intelligence = c.Intelligence,
                        Wisdom = c.Wisdom,
                        Charisma = c.Charisma,
                        StrMod = c.StrMod,
                        DexMod = c.DexMod,
                        ConMod = c.ConMod,
                        IntMod = c.IntMod,
                        WisMod = c.WisMod,
                        ChaMod = c.ChaMod,
                        CarryWeight = c.CarryWeight,
                        GoldPieces = c.GoldPieces,
                        SilverPieces = c.SilverPieces,
                        CopperPieces = c.CopperPieces,
                        CharacterNotes = c.CharacterNotes
                    };
                    charList.Add(charItem);
                }

                return charList;
            }
        }
        //get my character info by id
        public InstanceDisplay GetInstancedCharacterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //var inventory = ctx.Inventories.Single(e => e.CharInstanceID == id);
                var result = ctx.CharacterInstances.Single(e => e.OwnerID == _userId && e.CharInstanceID == id);
                List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                foreach (var i in result.InventoryItems)
                {
                    var item = new InstanceItemDisplay
                    {
                        ItemID = i.ItemID,
                        Name = i.Name,
                        Description = i.Description,
                        Weight = i.Weight,
                        HitPoints = i.HitPoints,
                        ItemRarity = i.ItemRarity,
                        ItemClass = i.ItemClass,
                        Damage = i.Damage,
                        DamageResiliance = i.DamageResiliance,
                        IsEquiptable = i.IsEquiptable,
                        IsEquipted = i.IsEquipted,
                        Strength = i.Strength,
                        Dexterity = i.Dexterity,
                        Constitution = i.Constitution,
                        Intelligence = i.Intelligence,
                        Wisdom = i.Wisdom,
                        Charisma = i.Charisma
                    };
                    itemList.Add(item);
                }
                var features = ctx.CharacterFeatList.Single(e => e.CharInstanceID == id);
                List<FeatureDisplay> featList = new List<FeatureDisplay>();
                foreach (var f in features.Features)
                {
                    var feature = new FeatureDisplay
                    {
                        FeatureID = f.FeatureID,
                        FeatureName = f.FeatureName,
                        Description = f.Description,
                        RaceIdPrerequisite = f.RaceIdPrerequisite,
                        ClassIdPrerequisite = f.ClassIdPrerequisite,
                        StatPrerequisite = f.StatPrerequisite,
                        LvlPrerequisite = f.LvlPrerequisite,
                        Strength = f.Strength,
                        Dexterity = f.Dexterity,
                        Constitution = f.Constitution,
                        Intelligence = f.Intelligence,
                        Wisdom = f.Wisdom,
                        Charisma = f.Charisma
                    };
                    featList.Add(feature);
                }
                var skills = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == id);
                List<SkillDisplay> skillList = new List<SkillDisplay>();
                foreach (var s in skills.Skills)
                {
                    var skill = new SkillDisplay
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Description = s.Description,
                        ClassesApplied = s.ClassesApplied,
                        RacesApplied = s.RacesApplied,
                        BackgroundsApplied = s.BackgroundsApplied,
                        StatApplied = s.StatApplied,
                        Strength = s.Strength,
                        Dexterity = s.Dexterity,
                        Constitution = s.Constitution,
                        Intelligence = s.Intelligence,
                        Wisdom = s.Wisdom,
                        Charisma = s.Charisma
                    };
                    skillList.Add(skill);
                }
                var room = ctx.Rooms.SingleOrDefault(e => e.RoomID == result.RoomID);
                string roomName = null;
                if (room == null)
                {
                    roomName = null;
                }
                else roomName = room.RoomName;
                var character = new InstanceDisplay
                {
                    CharInstanceID = result.CharInstanceID,
                    RoomID = result.RoomID,
                    RoomName = roomName,
                    CharSkeleton = new CharSkeleDisplay
                    {
                        ID = result.CharSkeletonID,
                        FirstName = result.CharSkeleton.FirstName,
                        LastName = result.CharSkeleton.LastName,
                        Gender = result.CharSkeleton.Gender,
                        VisualDescription = result.CharSkeleton.VisualDescription,
                        BackgroundDescription = result.CharSkeleton.BackgroundDescription,
                        CharacterNotes = result.CharSkeleton.CharacterNotes,
                        HeightInInches = result.CharSkeleton.HeightInInches,
                        WeightInPounds = result.CharSkeleton.WeightInPounds,
                        PersonalityTraits = result.CharSkeleton.PersonalityTraits,
                        Ideals = result.CharSkeleton.Ideals,
                        Bonds = result.CharSkeleton.Bonds,
                        Flaws = result.CharSkeleton.Flaws
                    },
                    Race = new RaceDisplay
                    {
                        RaceID = result.RaceID,
                        Name = result.Race.Name,
                        Speed = result.Race.Speed,
                        Size = result.Race.Size,
                        Languages = result.Race.Languages,
                        Proficiencies = result.Race.Proficiencies,
                        Trait = result.Race.Trait,
                        TraitDescription = result.Race.TraitDescription,
                        Strength = result.Race.Strength,
                        Dexterity = result.Race.Dexterity,
                        Constitution = result.Race.Constitution,
                        Intelligence = result.Race.Intelligence,
                        Wisdom = result.Race.Wisdom,
                        Charisma = result.Race.Charisma
                    },
                    Class = new ClassDisplay
                    {
                        ClassID = result.ClassID,
                        ClassName = result.Class.ClassName,
                        ClassDescription = result.Class.ClassDescription,
                        HitDie = result.Class.HitDie,
                        SavingThrows = result.Class.SavingThrows,
                        Proficiencies = result.Class.Proficiencies,
                        Strength = result.Class.Strength,
                        Dexterity = result.Class.Dexterity,
                        Constitution = result.Class.Constitution,
                        Intelligence = result.Class.Intelligence,
                        Wisdom = result.Class.Wisdom,
                        Charisma = result.Class.Charisma
                    },
                    Background = new BackgroundDisplay
                    {
                        BackgroundID = result.BackgroundID,
                        BackgroundName = result.Background.BackgroundName,
                        BackgroundDescription = result.Background.BackgroundDescription,
                        Proficiencies = result.Background.Proficiencies,
                        Strength = result.Background.Strength,
                        Dexterity = result.Background.Dexterity,
                        Constitution = result.Background.Constitution,
                        Intelligence = result.Background.Intelligence,
                        Wisdom = result.Background.Wisdom,
                        Charisma = result.Background.Charisma
                    },
                    Features = featList,
                    ProficiencySkills = skillList,
                    InventoryItems = itemList,
                    Alignment = result.Alignment,
                    OtherLanguages = result.OtherLanguages,
                    AttacksAndSpells = result.AttacksAndSpells,
                    HitPoints = result.HitPoints,
                    CurrentHitPoints = result.CurrentHitPoints,
                    ExperiencePoints = result.ExperiencePoints,
                    Level = result.Level,
                    Strength = result.Strength,
                    Dexterity = result.Dexterity,
                    Constitution = result.Constitution,
                    Intelligence = result.Intelligence,
                    Wisdom = result.Wisdom,
                    Charisma = result.Charisma,
                    StrMod = result.StrMod,
                    DexMod = result.DexMod,
                    ConMod = result.ConMod,
                    IntMod = result.IntMod,
                    WisMod = result.WisMod,
                    ChaMod = result.ChaMod,
                    CarryWeight = result.CarryWeight,
                    GoldPieces = result.GoldPieces,
                    SilverPieces = result.SilverPieces,
                    CopperPieces = result.CopperPieces,
                    CharacterNotes = result.CharacterNotes
                };
                return character;
            }
        }
        //create new character instance
        public bool CreateInstancedCharacter(InstanceCreate character)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newChar = new CharacterInstanced
                {
                    OwnerID = _userId,
                    RoomID = character.RoomID,
                    CharSkeletonID = character.CharSkeletonID,
                    ClassID = character.ClassID,
                    RaceID = character.RaceID,
                    BackgroundID = character.BackgroundID,
                    Alignment = character.Alignment,
                    OtherLanguages = character.OtherLanguages,
                    HitPoints = character.HitPoints,
                    CurrentHitPoints = character.HitPoints,
                    ExperiencePoints = 0,
                    Level = 1,
                    ProficiencyBonus = 0,
                    Strength = character.Strength,
                    Dexterity = character.Dexterity,
                    Constitution = character.Constitution,
                    Intelligence = character.Intelligence,
                    Wisdom = character.Wisdom,
                    Charisma = character.Charisma,
                    StrMod = StatModifier(character.Strength),
                    DexMod = StatModifier(character.Dexterity),
                    ConMod = StatModifier(character.Constitution),
                    IntMod = StatModifier(character.Intelligence),
                    WisMod = StatModifier(character.Wisdom),
                    ChaMod = StatModifier(character.Charisma),
                    CarryWeight = character.Strength*15,
                    PlatinumPieces = character.PlatinumPieces,
                    GoldPieces = character.GoldPieces,
                    ElectrumPieces = character.ElectrumPieces,
                    SilverPieces = character.SilverPieces,
                    CopperPieces = character.CopperPieces,
                    CharacterNotes = "",
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterInstances.Add(newChar);
                bool save = ctx.SaveChanges() == 1;
                //var inventory = new Inventory
                //{
                //    CharInstanceID = newChar.CharInstanceID,
                //    DateOfCreation = DateTimeOffset.UtcNow
                //};
                //ctx.Inventories.Add(inventory);
                var charFeats = new CharacterFeatList
                {
                    CharInstanceID = newChar.CharInstanceID,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterFeatList.Add(charFeats);
                var charSkills = new CharProficiencySkills
                {
                    CharInstanceID = newChar.CharInstanceID,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharProficiencySkills.Add(charSkills);
                return (ctx.SaveChanges() == 2 && save == true); //4 is number of editted table rows
            }
        }
        //update character instance
        //Cant change class, race, or background
        //can change alignment, attacksAndSpells, characterNotes, money, strength etc, other languages
        //hit points, carry weight determined by system IF DND game, otherwise it is manual
        public bool UpdateInstancedCharacter(int id, InstanceUpdate update)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var character = ctx.CharacterInstances.Single(e => e.OwnerID == _userId && e.CharInstanceID == id);
                character.Alignment = update.Alignment;
                character.AttacksAndSpells = update.AttacksAndSpells;
                character.HitPoints = update.HitPoints;
                character.CurrentHitPoints = update.CurrentHitPoints;
                character.ExperiencePoints = update.ExperiencePoints;
                character.Level = update.Level;
                character.OtherLanguages = update.OtherLanguages;
                character.Strength = update.Strength;
                character.Dexterity = update.Dexterity;
                character.Constitution = update.Constitution;
                character.Intelligence = update.Intelligence;
                character.Wisdom = update.Wisdom;
                character.Charisma = update.Charisma;
                character.CarryWeight = update.CarryWeight;
                character.PlatinumPieces = update.PlatinumPieces;
                character.GoldPieces = update.GoldPieces;
                character.ElectrumPieces = update.ElectrumPieces;
                character.SilverPieces = update.SilverPieces;
                character.CopperPieces = update.CopperPieces;
                character.CharacterNotes = update.CharacterNotes;
                return ctx.SaveChanges() == 1;
            }
        }
        //update/add feature
        public bool AddCharacterFeature(int charId, int featId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var featList = ctx.CharacterFeatList.Single(e => e.CharInstanceID == charId);
                var feature = ctx.CharacterFeatures.Single(e => e.FeatureID == featId);
                featList.Features.Add(feature);
                return ctx.SaveChanges() == 1;
            }
        }
        //update/add skill
        public bool AddCharacterSkill(int charId, int skillId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skillList = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == charId);
                var skill = ctx.ProficiencySkills.Single(e => e.ID == skillId);
                skillList.Skills.Add(skill);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove feature
        public bool RemoveCharacterFeature(int charId, int featId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var featureList = ctx.CharacterFeatList.Single(e => e.CharInstanceID == charId);
                var feature = ctx.CharacterFeatures.Single(e => e.FeatureID == featId);
                featureList.Features.Remove(feature);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove skill
        public bool RemoveCharacterSkill(int charId, int skillId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skillList = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == charId);
                var skill = ctx.ProficiencySkills.Single(e => e.ID == skillId);
                skillList.Skills.Remove(skill);
                return ctx.SaveChanges() == 1;
            }
        }
        //Delete my character instance
        public bool DeleteInstancedCharacter(int id)
        {
            using( var ctx = new ApplicationDbContext())
            {
                var character = ctx.CharacterInstances.Single(e => e.OwnerID == _userId && e.CharInstanceID == id);
                //delete char, inventory and feature/skill lists
                //var inventory = ctx.Inventories.Single(e => e.CharInstanceID == id);
                foreach( var i in character.InventoryItems)
                {
                    var invenItem = ctx.InventoryItems.Single(e => e.ItemID == i.ItemID);
                    ctx.InventoryItems.Remove(invenItem);
                }
                //ctx.Inventories.Remove(inventory);
                var features = ctx.CharacterFeatList.Single(e => e.CharInstanceID == id);
                ctx.CharacterFeatList.Remove(features);
                var skills = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == id);
                ctx.CharProficiencySkills.Remove(skills);
                ctx.CharacterInstances.Remove(character);

                return ctx.SaveChanges() == 4 + character.InventoryItems.Count(); //might through wrong save number
            }
        }
        private int StatModifier(double stat)
        {
            if (stat == 1) return -5;
            else if (stat == 2 || stat == 3) return -4;
            else if (stat == 4 || stat == 5) return -3;
            else if (stat == 6 || stat == 7) return -2;
            else if (stat == 8 || stat == 9) return -1;
            else if (stat == 10 || stat == 11) return 0;
            else if (stat == 12 || stat == 13) return 1;
            else if (stat == 14 || stat == 15) return 2;
            else if (stat == 16 || stat == 17) return 3;
            else if (stat == 18 || stat == 19) return 4;
            else if (stat == 20 || stat == 21) return 5;
            else if (stat == 22 || stat == 23) return 6;
            else if (stat == 24 || stat == 25) return 7;
            else if (stat == 26 || stat == 27) return 8;
            else if (stat == 28 || stat == 29) return 9;
            else if (stat == 30) return 10;
            else return 0;
        }
    }
}
