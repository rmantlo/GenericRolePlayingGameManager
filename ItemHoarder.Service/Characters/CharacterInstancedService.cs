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
                            ClassType = i.ClassType,
                            Damage = i.Damage,
                            DamageResiliance = i.DamageResiliance,
                            IsEquiptable = i.IsEquiptable,
                            IsEquipted = i.IsEquipted,
                            Strength = i.Strength,
                            Dexterity = i.Dexterity,
                            Constitution = i.Constitution,
                            Intelligence = i.Intelligence,
                            Wisdom = i.Wisdom,
                            Charisma = i.Charisma,
                            DateOfCreation = i.DateOfCreation,
                        };
                        itemList.Add(item);
                    }
                    List<FeatureDisplay> featList = new List<FeatureDisplay>();
                    foreach (var f in c.Features)
                    {
                        var feature = new FeatureDisplay
                        {
                            FeatureID = f.FeatureID,
                            FeatureName = f.Feature.FeatureName,
                            Description = f.Feature.Description,
                            RaceIdPrerequisite = f.Feature.RaceIdPrerequisite,
                            ClassIdPrerequisite = f.Feature.ClassIdPrerequisite,
                            StatPrerequisite = f.Feature.StatPrerequisite,
                            LvlPrerequisite = f.Feature.LvlPrerequisite,
                            Strength = f.Feature.Strength,
                            Dexterity = f.Feature.Dexterity,
                            Constitution = f.Feature.Constitution,
                            Intelligence = f.Feature.Intelligence,
                            Wisdom = f.Feature.Wisdom,
                            Charisma = f.Feature.Charisma
                        };
                        featList.Add(feature);
                    }
                    List<SkillDisplay> skillList = new List<SkillDisplay>();
                    foreach (var s in c.Skills)
                    {
                        var skill = new SkillDisplay
                        {
                            ID = s.ID,
                            Name = s.Skills.Name,
                            Description = s.Skills.Description,
                            ClassesApplied = s.Skills.ClassesApplied,
                            RacesApplied = s.Skills.RacesApplied,
                            BackgroundsApplied = s.Skills.BackgroundsApplied,
                            StatApplied = s.Skills.StatApplied,
                            Strength = s.Skills.Strength,
                            Dexterity = s.Skills.Dexterity,
                            Constitution = s.Skills.Constitution,
                            Intelligence = s.Skills.Intelligence,
                            Wisdom = s.Skills.Wisdom,
                            Charisma = s.Skills.Charisma
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
                        ProficiencyBonus = c.ProficiencyBonus,
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
                List<FeatureDisplay> featList = new List<FeatureDisplay>();
                foreach (var f in result.Features)
                {
                    var feature = new FeatureDisplay
                    {
                        FeatureID = f.FeatureID,
                        FeatureName = f.Feature.FeatureName,
                        Description = f.Feature.Description,
                        RaceIdPrerequisite = f.Feature.RaceIdPrerequisite,
                        ClassIdPrerequisite = f.Feature.ClassIdPrerequisite,
                        StatPrerequisite = f.Feature.StatPrerequisite,
                        LvlPrerequisite = f.Feature.LvlPrerequisite,
                        Strength = f.Feature.Strength,
                        Dexterity = f.Feature.Dexterity,
                        Constitution = f.Feature.Constitution,
                        Intelligence = f.Feature.Intelligence,
                        Wisdom = f.Feature.Wisdom,
                        Charisma = f.Feature.Charisma
                    };
                    featList.Add(feature);
                }
                List<SkillDisplay> skillList = new List<SkillDisplay>();
                foreach (var s in result.Skills)
                {
                    var skill = new SkillDisplay
                    {
                        ID = s.ID,
                        Name = s.Skills.Name,
                        Description = s.Skills.Description,
                        ClassesApplied = s.Skills.ClassesApplied,
                        RacesApplied = s.Skills.RacesApplied,
                        BackgroundsApplied = s.Skills.BackgroundsApplied,
                        StatApplied = s.Skills.StatApplied,
                        Strength = s.Skills.Strength,
                        Dexterity = s.Skills.Dexterity,
                        Constitution = s.Skills.Constitution,
                        Intelligence = s.Skills.Intelligence,
                        Wisdom = s.Skills.Wisdom,
                        Charisma = s.Skills.Charisma
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
                    ProficiencyBonus = result.ProficiencyBonus,
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

        //create is in room service

        //update character instance
        //Cant change class, race, or background
        //can change alignment, attacksAndSpells, characterNotes, money, strength etc, other languages
        //hit points, carry weight determined by system IF DND game, otherwise it is manual
        public bool UpdateInstancedCharacter(int id, InstanceUpdate update)
        {
            using (var ctx = new ApplicationDbContext())
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
            using (var ctx = new ApplicationDbContext())
            {
                var featList = ctx.CharacterInstances.Single(e => e.CharInstanceID == charId);
                var feat = new CharacterFeatList
                {
                    CharInstanceID = charId,
                    FeatureID = featId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                featList.Features.Add(feat);
                return ctx.SaveChanges() == 1;// might be 2??
            }
        }
        //update/add skill
        public bool AddCharacterSkill(int charId, int skillId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skillList = ctx.CharacterInstances.Single(e => e.CharInstanceID == charId);
                var skill = new CharProficiencySkills
                {
                    CharInstanceID = charId,
                    SkillID = skillId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                skillList.Skills.Add(skill);
                return ctx.SaveChanges() == 1; // might be 2??
            }
        }
        //remove feature
        public bool RemoveCharacterFeature(int charId, int featId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var featureList = ctx.CharacterFeatList.Single(e => e.CharInstanceID == charId && e.FeatureID == featId);
                ctx.CharacterFeatList.Remove(featureList);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove skill
        public bool RemoveCharacterSkill(int charId, int skillId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skill = ctx.CharProficiencySkills.Single(e => e.CharInstanceID == charId && e.SkillID == skillId);
                ctx.CharProficiencySkills.Remove(skill);
                return ctx.SaveChanges() == 1;
            }
        }
        //Delete my character instance
        public bool DeleteInstancedCharacter(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var character = ctx.CharacterInstances.Single(e => e.OwnerID == _userId && e.CharInstanceID == id);
                foreach (var i in character.InventoryItems)
                {
                    var invenItem = ctx.InventoryItems.Single(e => e.ItemID == i.ItemID);
                    ctx.InventoryItems.Remove(invenItem);
                }
                foreach (var i in character.Features)
                {
                    var features = ctx.CharacterFeatList.Single(e => e.ID == i.ID);
                    ctx.CharacterFeatList.Remove(features);
                }
                foreach (var i in character.Skills)
                {
                    var skills = ctx.CharProficiencySkills.Single(e => e.ID == i.ID);
                    ctx.CharProficiencySkills.Remove(skills);
                }
                ctx.CharacterInstances.Remove(character);

                return ctx.SaveChanges() == 1 + character.InventoryItems.Count() + character.Features.Count() + character.Skills.Count(); //might through wrong save number
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
