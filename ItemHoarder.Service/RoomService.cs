using ItemHoarder.Data;
using ItemHoarder.Data.BattleFolder;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.BattleInstances;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.Familars;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Models.Monsters;
using ItemHoarder.Models.Rooms;
using ItemHoarder.Models.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class RoomService
    {
        private readonly Guid _userID;
        public RoomService(Guid userId)
        {
            _userID = userId;
        }
        ////get all rooms (second param dictates to get rooms i own or rooms im in)
        //public object GetAllRooms(string ownershipStatus)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        if (ownershipStatus == "Owner")
        //        {
        //            var rooms = ctx.Rooms.Where(e => e.OwnerID == _userID).ToList();
        //            var ownerUsername = ctx.Users.Single(e => e.Id == _userID.ToString()).UserName;
        //            List<RoomIndex> roomList = new List<RoomIndex>();
        //            foreach (var i in rooms)
        //            {
        //                List<string> usernames = new List<string>();
        //                foreach (var n in i.RoomUsers)
        //                {
        //                    usernames.Add(n.PlayerUsername);
        //                }
        //                roomList.Add(new RoomIndex
        //                {
        //                    RoomID = i.RoomID,
        //                    RoomName = i.RoomName,
        //                    RoomCreatorUsername = ownerUsername,
        //                    GameType = i.GameType.ToString(),
        //                    PlayerUsernames = usernames,
        //                    RoomPhoto = i.RoomPhoto.ToList()[0],
        //                    PhotoID = (i.RoomPhoto.Count > 0) ? i.RoomPhoto.ToList()[0].PhotoID : 0,
        //                    DateOfCreation = i.DateOfCreation,
        //                    DateOfModification = i.DateOfModification
        //                });
        //            }
        //            return roomList;
        //        }
        //        else
        //        {
        //            var roomUsers = ctx.RoomUsers.Where(e => e.PlayerID == _userID).ToList();
        //            List<RoomIndex> roomList = new List<RoomIndex>();
        //            foreach (var r in roomUsers)
        //            {
        //                var room = ctx.Rooms.Single(e => e.RoomID == r.RoomID);
        //                var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString()).UserName;
        //                var character = ctx.CharacterInstances.SingleOrDefault(e => e.RoomID == room.RoomID && e.OwnerID == _userID);
        //                List<string> usernames = new List<string>();
        //                foreach (var u in room.RoomUsers)
        //                {
        //                    usernames.Add(u.PlayerUsername);
        //                }
        //                roomList.Add(new RoomIndex
        //                {
        //                    RoomID = room.RoomID,
        //                    RoomName = room.RoomName,
        //                    RoomCreatorUsername = gmUsername,
        //                    GameType = room.GameType.ToString(),
        //                    PlayerUsernames = usernames,
        //                    RoomPhoto = room.RoomPhoto.ToList()[0],
        //                    PhotoID = (room.RoomPhoto.Count > 0) ? room.RoomPhoto.ToList()[0].PhotoID : 0,
        //                    CharInstanceName = character.CharSkeleton.FirstName + character.CharSkeleton.LastName,
        //                    CharInstanceLevel = character.Level,
        //                    CharInstancePhoto = character.CharacterPhoto.ToList()[0],
        //                    DateOfCreation = room.DateOfCreation,
        //                    DateOfModification = room.DateOfModification
        //                });
        //            }
        //            return roomList;
        //        }
        //    }
        //}
        ////get player room by Id
        //public object GetRoomById(int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var room = ctx.Rooms.Single(e => e.RoomID == roomId);
        //        var chars = ctx.CharacterInstances.Where(e => e.RoomID == roomId).ToList();
        //        var currentBattle = ctx.BattleInstances.Single(e => e.RoomID == roomId && e.IsCurrent == true);
        //        var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString()).UserName;
        //        List<string> players = new List<string>();
        //        foreach (var u in room.RoomUsers)
        //        {
        //            players.Add(u.PlayerUsername);
        //        }

        //        List<ClassDisplay> classList = new List<ClassDisplay>();
        //        List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
        //        List<FeatureDisplay> featList = new List<FeatureDisplay>();
        //        List<SkillDisplay> skillList = new List<SkillDisplay>();
        //        List<FamilarDisplay> familarList = new List<FamilarDisplay>();
        //        List<SpellCantripDisplay> spellsList = new List<SpellCantripDisplay>();
        //        List<SpellCantripDisplay> cantripList = new List<SpellCantripDisplay>();
        //        if (room.OwnerID == _userID)
        //        {
        //            var someBattles = ctx.BattleInstances.Where(e => e.RoomID == roomId).OrderByDescending(b => (b.DateOfModification > b.DateOfCreation ? b.DateOfModification : b.DateOfCreation)).Take(3).ToList();
        //            List<InstanceGMDisplay> characters = new List<InstanceGMDisplay>();
        //            foreach (var c in chars)
        //            {
        //                foreach (var s in c.Class)
        //                {
        //                    classList.Add(new ClassDisplay
        //                    {
        //                        ClassID = s.ClassID,
        //                        GameType = s.Class.GameTag.ToString(),
        //                        ClassName = s.Class.ClassName,
        //                        ClassDescription = s.Class.ClassDescription,
        //                        HitDie = s.Class.HitDie,
        //                        SavingThrows = s.Class.SavingThrows.Split('|').ToList(),
        //                        WeaponProficiencies = s.Class.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = s.Class.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = s.Class.ToolProficiencies.Split('|').ToList(),
        //                        SubClasses = null,
        //                        Strength = s.Class.Strength,
        //                        Dexterity = s.Class.Dexterity,
        //                        Constitution = s.Class.Constitution,
        //                        Intelligence = s.Class.Intelligence,
        //                        Wisdom = s.Class.Wisdom,
        //                        Charisma = s.Class.Charisma,
        //                        DateOfCreation = s.Class.DateOfCreation,
        //                        DateOfModification = s.Class.DateOfModification
        //                    });
        //                }
        //                foreach (var i in c.InventoryItems)
        //                {
        //                    var item = new InstanceItemDisplay
        //                    {
        //                        ItemID = i.ItemID,
        //                        Name = i.OriginalItem.Name,
        //                        Description = i.OriginalItem.Description,
        //                        Weight = i.OriginalItem.Weight,
        //                        HitPoints = i.ActualHitPoints,
        //                        ItemRarity = i.OriginalItem.ItemRarity,
        //                        ItemClass = i.OriginalItem.ItemClass,
        //                        ClassType = i.OriginalItem.ClassType,
        //                        ArmorClass = i.OriginalItem.ArmorClass,
        //                        Damage = i.OriginalItem.Damage,
        //                        DamageResiliance = i.OriginalItem.DamageResiliance,
        //                        IsEquiptable = i.OriginalItem.IsEquiptable,
        //                        IsEquipted = i.IsEquipted,
        //                        Strength = i.OriginalItem.Strength,
        //                        Dexterity = i.OriginalItem.Dexterity,
        //                        Constitution = i.OriginalItem.Constitution,
        //                        Intelligence = i.OriginalItem.Intelligence,
        //                        Wisdom = i.OriginalItem.Wisdom,
        //                        Charisma = i.OriginalItem.Charisma,
        //                        DateOfCreation = i.DateOfCreation
        //                    };
        //                    itemList.Add(item);
        //                }
        //                foreach (var f in c.Features)
        //                {
        //                    var feature = new FeatureDisplay
        //                    {
        //                        FeatureID = f.FeatureID,
        //                        FeatureName = f.Feature.FeatureName,
        //                        Description = f.Feature.Description,
        //                        RaceIDs = f.Feature.RaceIDs.Split('|').ToList(),
        //                        ClassIDs = f.Feature.ClassIDs.Split('|').ToList(),
        //                        FeatureIDs = f.Feature.FeatureIDs.Split('|').ToList(),
        //                        StatPrerequisite = f.Feature.StatPrerequisite.Split('|').ToList(),
        //                        LvlPrerequisite = f.Feature.LvlPrerequisite,
        //                        Strength = f.Feature.Strength,
        //                        Dexterity = f.Feature.Dexterity,
        //                        Constitution = f.Feature.Constitution,
        //                        Intelligence = f.Feature.Intelligence,
        //                        Wisdom = f.Feature.Wisdom,
        //                        Charisma = f.Feature.Charisma
        //                    };
        //                    featList.Add(feature);
        //                }
        //                foreach (var s in c.Skills)
        //                {
        //                    var skill = new SkillDisplay
        //                    {
        //                        ID = s.ID,
        //                        GameTag = s.Skills.GameTag.ToString(),
        //                        Name = s.Skills.Name,
        //                        SkillRank = s.SkillRank,
        //                        Description = s.Skills.Description,
        //                        SpecialInfo = s.Skills.SpecialInfo,
        //                        ActionType = s.Skills.ActionType,
        //                        ClassesIDs = s.Skills.ClassesIDs.Split('|').ToList(),
        //                        RacesIDs = s.Skills.RacesIDs.Split('|').ToList(),
        //                        BackgroundsIDs = s.Skills.BackgroundsIDs.Split('|').ToList(),
        //                        AbilityStatApplied = s.Skills.AbilityStatApplied,
        //                        TrainedOnly = s.Skills.TrainedOnly,
        //                        ArmorCheckPenalty = s.Skills.ArmorCheckPenalty,
        //                        AttemptDetails = s.Skills.AttemptDetails,
        //                        Restrictions = s.Skills.Restrictions,
        //                        DateOfCreation = s.Skills.DateOfCreation,
        //                        DateOfModification = s.Skills.DateOfModification
        //                    };
        //                    skillList.Add(skill);
        //                }
        //                List<Dictionary<string, string>> familarFeats = new List<Dictionary<string, string>>();
        //                foreach (var f in c.Familars)
        //                {
        //                    foreach (var feat in f.FamilarFeatures)
        //                    {
        //                        familarFeats.Add(new Dictionary<string, string>
        //                        {
        //                            {feat.FeatName, feat.FeatDescription }
        //                        });
        //                    }
        //                    familarList.Add(new FamilarDisplay
        //                    {
        //                        FamilarID = f.FamilarID,
        //                        PetName = f.Name,
        //                        FamilarName = f.Familar.FamilarName,
        //                        FamilarType = f.Familar.FamilarType,
        //                        VisualDescription = f.Familar.VisualDescription,
        //                        SpecialEffects = f.Familar.SpecialEffects,
        //                        FamilarFeatures = familarFeats,
        //                        IsImproved = f.Familar.IsImproved,
        //                        Alignment = f.Familar.Alignment.ToString(),
        //                        LevelRequirement = f.Familar.LevelRequirement,
        //                        HitPoints = f.HitPoints,
        //                        CurrentHitPoints = f.CurrentHitPoints,
        //                        HitDie = f.HitDie,
        //                        Initiative = f.Familar.Initiative,
        //                        Speed = f.Familar.Speed,
        //                        ArmorClass = f.Familar.ArmorClass,
        //                        BaseAttack = f.Familar.BaseAttack,
        //                        Grapple = f.Familar.Grapple,
        //                        FortitudeSave = f.Familar.FortitudeSave,
        //                        ReflexSave = f.Familar.ReflexSave,
        //                        WillSave = f.Familar.WillSave,
        //                        Strength = f.Familar.Strength,
        //                        Dexterity = f.Familar.Dexterity,
        //                        Constitution = f.Familar.Constitution,
        //                        Intelligence = f.Familar.Intelligence,
        //                        Wisdom = f.Familar.Wisdom,
        //                        Charisma = f.Familar.Charisma,
        //                        DateOfCreation = f.DateOfCreation
        //                    });
        //                }
        //                foreach (var s in c.Spells)
        //                {
        //                    if (s.Spell.AttackType == 0)
        //                    {
        //                        spellsList.Add(new SpellCantripDisplay
        //                        {
        //                            ID = s.SpellID,
        //                            AttackType = s.Spell.AttackType.ToString(),
        //                            Name = s.Spell.Name,
        //                            Description = s.Spell.Description,
        //                            HigherLevelDescription = s.Spell.HigherLevelDesc,
        //                            LevelRequired = s.Spell.LevelRequired,
        //                            ClassIDs = null,
        //                            SubClassIDs = null,
        //                            RaceIDs = null,
        //                            SchoolOfMagic = s.Spell.SchoolOfMagic.ToString(),
        //                            SpellLevel = s.Spell.SpellLevel,
        //                            DiceRollType = s.Spell.DiceRollType,
        //                            Range = s.Spell.Range,
        //                            Components = s.Spell.Components.Split('|').ToList(),
        //                            Materials = s.Spell.Material.Split('|').ToList(),
        //                            Ritual = s.Spell.Ritual,
        //                            Concentration = s.Spell.Concentration,
        //                            CastingTime = s.Spell.CastingTime,
        //                            Duration = s.Spell.Duration,
        //                            AreaOfEffect = s.Spell.AreaOfEffect.ToString(),
        //                            AreaOfEffectLength = s.Spell.AreaOfEffectLength,
        //                            SavingThrow = s.Spell.SavingThrow,
        //                            DateOfCreation = s.Spell.DateOfCreation
        //                        });
        //                    }
        //                    else if (s.Spell.AttackType == (AttackType)1)
        //                    {
        //                        cantripList.Add(new SpellCantripDisplay
        //                        {
        //                            ID = s.SpellID,
        //                            AttackType = s.Spell.AttackType.ToString(),
        //                            Name = s.Spell.Name,
        //                            Description = s.Spell.Description,
        //                            HigherLevelDescription = s.Spell.HigherLevelDesc,
        //                            LevelRequired = s.Spell.LevelRequired,
        //                            ClassIDs = null,
        //                            SubClassIDs = null,
        //                            RaceIDs = null,
        //                            SchoolOfMagic = s.Spell.SchoolOfMagic.ToString(),
        //                            SpellLevel = s.Spell.SpellLevel,
        //                            DiceRollType = s.Spell.DiceRollType,
        //                            Range = s.Spell.Range,
        //                            Components = s.Spell.Components.Split('|').ToList(),
        //                            Materials = s.Spell.Material.Split('|').ToList(),
        //                            Ritual = s.Spell.Ritual,
        //                            Concentration = s.Spell.Concentration,
        //                            CastingTime = s.Spell.CastingTime,
        //                            Duration = s.Spell.Duration,
        //                            AreaOfEffect = s.Spell.AreaOfEffect.ToString(),
        //                            AreaOfEffectLength = s.Spell.AreaOfEffectLength,
        //                            SavingThrow = s.Spell.SavingThrow,
        //                            DateOfCreation = s.Spell.DateOfCreation
        //                        });
        //                    }
        //                }
        //                List<int> sCClassList = new List<int>();
        //                foreach (var sub in c.SubClass.Classes)
        //                {
        //                    sCClassList.Add(sub.ClassID);
        //                }
        //                List<int> sCFeatList = new List<int>();
        //                foreach (var sub in c.SubClass.Features)
        //                {
        //                    sCFeatList.Add(sub.FeatureID);
        //                }
        //                var sCAppliedSpells = c.SubClass.AppliedSpellIDs.Split('|').ToList();
        //                var sCSpells = c.SubClass.ListOfSpellIDs.Split('|').ToList();
        //                var instance = new InstanceGMDisplay
        //                {
        //                    CharInstanceID = c.CharInstanceID,
        //                    OwnerID = c.OwnerID,
        //                    RoomID = c.RoomID,
        //                    RoomName = room.RoomName,
        //                    CharacterPhoto = c.CharacterPhoto.ToList(),
        //                    CharSkeleton = new InstanceCharDetails
        //                    {
        //                        CharSkeleton = new SkeletonGMDisplay
        //                        {
        //                            ID = c.CharSkeleton.CharacterID,
        //                            FullName = (c.CharSkeleton.FirstName + c.CharSkeleton.LastName),
        //                            Gender = c.CharSkeleton.Gender.ToString(),
        //                            VisualDescription = c.CharSkeleton.VisualDescription,
        //                            BackgroundDescription = c.CharSkeleton.BackgroundDescription,
        //                            HeightInInches = c.CharSkeleton.HeightInInches,
        //                            WeightInPounds = c.CharSkeleton.WeightInPounds,
        //                            DateOfCreation = c.CharSkeleton.DateOfCreation,
        //                            DateOfModification = c.CharSkeleton.DateOfModification
        //                        },
        //                        Alignment = c.Alignment.ToString(),
        //                        AllLanguages = c.AllLanguages.Split('|').ToList(),
        //                        HitPoints = c.HitPoints,
        //                        CurrentHitPoints = c.CurrentHitPoints,
        //                        ExperiencePoints = c.ExperiencePoints,
        //                        Level = c.Level,
        //                        WeaponProficiencies = c.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.ToolProficiencies.Split('|').ToList(),
        //                        BaseAttackBonus = c.BaseAttackBonus,
        //                        BaseAttackBonusTwo = c.BaseAttackBonusTwo,
        //                        BaseAttackBonusThree = c.BaseAttackBonusThree,
        //                        BaseAttackBonusFour = c.BaseAttackBonusFour,
        //                        PersonalityTraits = c.PersonalityTraits.Split('|').ToList(),
        //                        Ideals = c.Ideals,
        //                        Bonds = c.Bonds,
        //                        Flaws = c.Flaws,
        //                        Strength = c.Strength,
        //                        Dexterity = c.Dexterity,
        //                        Constitution = c.Constitution,
        //                        Intelligence = c.Intelligence,
        //                        Wisdom = c.Wisdom,
        //                        Charisma = c.Charisma,
        //                        CarryWeight = c.CarryWeight,
        //                        PlatinumPieces = c.PlatinumPieces,
        //                        GoldPieces = c.GoldPieces,
        //                        ElectrumPieces = c.ElectrumPieces,
        //                        SilverPieces = c.SilverPieces,
        //                        CopperPieces = c.CopperPieces
        //                    },
        //                    Race = new RaceDisplay
        //                    {
        //                        RaceID = c.RaceID,
        //                        GameTag = c.Race.GameTag.ToString(),
        //                        Name = c.Race.Name,
        //                        Speed = c.Race.Speed,
        //                        Size = c.Race.Size,
        //                        Languages = null,
        //                        WeaponProficiencies = c.Race.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.Race.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.Race.ToolProficiencies.Split('|').ToList(),
        //                        DefensiveRacialTrait = c.Race.DefensiveRacialTrait.Split('|').ToList(),
        //                        FeatRacialTrait = c.Race.FeatRacialTrait.Split('|').ToList(),
        //                        MagicalRacialTrait = c.Race.MagicalRacialTrait.Split('|').ToList(),
        //                        SensesRacialTrait = c.Race.SensesRacialTrait.Split('|').ToList(),
        //                        Strength = c.Race.Strength,
        //                        Dexterity = c.Race.Dexterity,
        //                        Constitution = c.Race.Constitution,
        //                        Intelligence = c.Race.Intelligence,
        //                        Wisdom = c.Race.Wisdom,
        //                        Charisma = c.Race.Charisma,
        //                        DateOfCreation = c.Race.DateOfCreation,
        //                    },
        //                    Background = new BackgroundDisplay
        //                    {
        //                        BackgroundID = c.BackgroundID,
        //                        GameTag = c.Background.GameTag.ToString(),
        //                        BackgroundName = c.Background.BackgroundName,
        //                        BackgroundDescription = c.Background.BackgroundDescription,
        //                        WeaponProficiencies = c.Background.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.Background.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.Background.ToolProficiencies.Split('|').ToList(),
        //                        FeatureIDs = c.Background.FeatureIDs.Split('|').ToList(),
        //                        Strength = c.Background.Strength,
        //                        Dexterity = c.Background.Dexterity,
        //                        Constitution = c.Background.Constitution,
        //                        Intelligence = c.Background.Intelligence,
        //                        Wisdom = c.Background.Wisdom,
        //                        Charisma = c.Background.Charisma,
        //                        DateOfCreation = c.Background.DateOfCreation,
        //                        DateOfModification = c.Background.DateOfModification
        //                    },
        //                    Class = classList,
        //                    SubClass = new SubClassDisplay
        //                    {
        //                        SubClassID = c.SubClassID,
        //                        SubClassName = c.SubClass.SubClassName,
        //                        SubClassDesc = c.SubClass.Description,
        //                        ClassIDs = sCClassList,
        //                        FeatureIDs = sCFeatList,
        //                        AppliedSpells = sCAppliedSpells.Select(int.Parse).ToList(),
        //                        SpellIDs = sCSpells.Select(int.Parse).ToList(),
        //                        WeaponProficiencies = c.SubClass.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.SubClass.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.SubClass.ToolProficiencies.Split('|').ToList(),
        //                        DateOfCreation = c.SubClass.DateOfCreation
        //                    },
        //                    Features = featList,
        //                    Skills = skillList,
        //                    InventoryItems = itemList,
        //                    Familars = familarList,
        //                    Conditions = c.Conditions.Split('|').ToList(),
        //                    Spells = spellsList,
        //                    Cantrips = cantripList,
        //                    DateOfCreation = c.DateOfCreation,
        //                    DateOfModification = c.DateOfModification,
        //                };
        //                characters.Add(instance);
        //            }
        //            List<BattleGMDisplay> battleInstances = new List<BattleGMDisplay>();
        //            List<MonsterDisplay> bMonsters = new List<MonsterDisplay>();
        //            List<ItemDisplay> bItems = new List<ItemDisplay>();
        //            List<ItemDisplay> bRandomItems = new List<ItemDisplay>();
        //            foreach (var m in currentBattle.Monsters)
        //            {

        //            }
        //            foreach (var i in currentBattle.ItemDrops)
        //            {

        //            }
        //            foreach (var r in currentBattle.ItemRandomDrops)
        //            {

        //            }
        //            battleInstances.Add(new BattleGMDisplay
        //            {
        //                BattleID = currentBattle.BattleID,
        //                RoomID = currentBattle.RoomID,
        //                IsCurrent = currentBattle.IsCurrent,
        //                MonsterList = bMonsters,
        //                ItemDropList = bItems,
        //                RandomItemDropList = bRandomItems,
        //                DateOfCreation = currentBattle.DateOfCreation,
        //                DateOfModification = currentBattle.DateOfModification
        //            });
        //            foreach (var b in someBattles)
        //            {
        //                List<MonsterDisplay> baMonsters = new List<MonsterDisplay>();
        //                List<ItemDisplay> baItems = new List<ItemDisplay>();
        //                List<ItemDisplay> baRandomItems = new List<ItemDisplay>();
        //                foreach (var m in b.Monsters)
        //                {

        //                }
        //                foreach (var i in b.ItemDrops)
        //                {

        //                }
        //                foreach (var r in b.ItemRandomDrops)
        //                {

        //                }
        //                battleInstances.Add(new BattleGMDisplay
        //                {
        //                    BattleID = b.BattleID,
        //                    RoomID = b.RoomID,
        //                    IsCurrent = b.IsCurrent,
        //                    MonsterList = baMonsters,
        //                    ItemDropList = baItems,
        //                    RandomItemDropList = baRandomItems,
        //                    DateOfCreation = b.DateOfCreation,
        //                    DateOfModification = b.DateOfModification
        //                });
        //            }
                    
        //            var display = new RoomGMDisplay
        //            {
        //                RoomID = room.RoomID,
        //                RoomName = room.RoomName,
        //                GameType = room.GameType.ToString(),
        //                RoomNotes = new RoomNoteDisplay
        //                {
        //                    PlayerOneNotes = room.RoomNotes.PlayerOneNotes,
        //                    PlayerTwoNotes = room.RoomNotes.PlayerTwoNotes,
        //                    PlayerThreeNotes = room.RoomNotes.PlayerThreeNotes,
        //                    PlayerFourNotes = room.RoomNotes.PlayerFourNotes,
        //                    PlayerFiveNotes = room.RoomNotes.PlayerFiveNotes,
        //                    PlayerSixNotes = room.RoomNotes.PlayerSixNotes,
        //                    PlayerSevenNotes = room.RoomNotes.PlayerSevenNotes,
        //                    GeneralNotes = room.RoomNotes.GeneralNotes,
        //                    DateOfModification = room.RoomNotes.DateOfModification
        //                },
        //                PlayerUsernames = players,
        //                Characters = characters,
        //                BattleInstances = battleInstances,
        //                Chests,
        //                DateOfCreation = room.DateOfCreation,
        //                DateOfModification = room.DateOfModification
        //            };
        //            return display;
        //        }
        //        else
        //        {
        //            var myChar = new InstanceDisplay();
        //            List<InstanceOthersDisplay> otherChars = new List<InstanceOthersDisplay>();
        //            foreach (var c in chars)
        //            {
        //                if (c.OwnerID == _userID)
        //                {
        //                    foreach (var i in c.InventoryItems)
        //                    {
        //                        var item = new InstanceItemDisplay
        //                        {
        //                            ItemID = i.ItemID,
        //                            Name = i.OriginalItem.Name,
        //                            Description = i.OriginalItem.Description,
        //                            Weight = i.OriginalItem.Weight,
        //                            HitPoints = i.ActualHitPoints,
        //                            ItemRarity = i.OriginalItem.ItemRarity,
        //                            ItemClass = i.OriginalItem.ItemClass,
        //                            ClassType = i.OriginalItem.ClassType,
        //                            Damage = i.OriginalItem.Damage,
        //                            DamageResiliance = i.OriginalItem.DamageResiliance,
        //                            IsEquiptable = i.OriginalItem.IsEquiptable,
        //                            IsEquipted = i.IsEquipted,
        //                            Strength = i.OriginalItem.Strength,
        //                            Dexterity = i.OriginalItem.Dexterity,
        //                            Constitution = i.OriginalItem.Constitution,
        //                            Intelligence = i.OriginalItem.Intelligence,
        //                            Wisdom = i.OriginalItem.Wisdom,
        //                            Charisma = i.OriginalItem.Charisma,
        //                            DateOfCreation = i.DateOfCreation
        //                        };
        //                        itemList.Add(item);
        //                    }
        //                    foreach (var f in c.Features)
        //                    {
        //                        var feature = new FeatureDisplay
        //                        {
        //                            FeatureID = f.FeatureID,
        //                            FeatureName = f.Feature.FeatureName,
        //                            Description = f.Feature.Description,
        //                            RaceIdPrerequisite = f.Feature.RaceIdPrerequisite.Split('|').ToList(),
        //                            ClassIdPrerequisite = f.Feature.ClassIdPrerequisite.Split('|').ToList(),
        //                            StatPrerequisite = f.Feature.StatPrerequisite.Split('|').ToList(),
        //                            LvlPrerequisite = f.Feature.LvlPrerequisite,
        //                            Strength = f.Feature.Strength,
        //                            Dexterity = f.Feature.Dexterity,
        //                            Constitution = f.Feature.Constitution,
        //                            Intelligence = f.Feature.Intelligence,
        //                            Wisdom = f.Feature.Wisdom,
        //                            Charisma = f.Feature.Charisma
        //                        };
        //                        featList.Add(feature);
        //                    }
        //                    foreach (var s in c.Skills)
        //                    {
        //                        var skill = new SkillDisplay
        //                        {
        //                            ID = s.ID,
        //                            Name = s.Skills.Name,
        //                            Description = s.Skills.Description,
        //                            ClassesApplied = s.Skills.ClassesApplied.Split('|').ToList(),
        //                            RacesApplied = s.Skills.RacesApplied.Split('|').ToList(),
        //                            BackgroundsApplied = s.Skills.BackgroundsApplied.Split('|').ToList(),
        //                            AbilityStatApplied = s.Skills.AbilityStatApplied
        //                        };
        //                        skillList.Add(skill);
        //                    }
        //                    foreach (var cla in c.Class)
        //                    {
        //                        var newClass = new ClassDisplay
        //                        {
        //                            ClassID = cla.ClassID,
        //                            ClassDescription = cla.Class.ClassDescription,
        //                            HitDie = cla.Class.HitDie,
        //                            SavingThrows = cla.Class.SavingThrows.Split('|').ToList(),
        //                            WeaponProficiencies = cla.Class.WeaponProficiencies.Split('|').ToList(),
        //                            ArmorProficiencies = cla.Class.ArmorProficiencies.Split('|').ToList(),
        //                            ToolProficiencies = cla.Class.ToolProficiencies.Split('|').ToList(),
        //                            Strength = cla.Class.Strength,
        //                            Dexterity = cla.Class.Dexterity,
        //                            Constitution = cla.Class.Constitution,
        //                            Intelligence = cla.Class.Intelligence,
        //                            Wisdom = cla.Class.Wisdom,
        //                            Charisma = cla.Class.Charisma,
        //                            DateOfCreation = cla.Class.DateOfCreation,
        //                            DateOfModification = cla.Class.DateOfModification
        //                        };
        //                        classList.Add(newClass);
        //                    }
        //                    myChar.CharInstanceID = c.CharInstanceID;
        //                    myChar.OwnerID = c.OwnerID;
        //                    myChar.RoomID = c.RoomID;
        //                    myChar.RoomName = room.RoomName;
        //                    myChar.CharSkeleton = new InstanceCharDisplay
        //                    {
        //                        ID = c.CharSkeletonID,
        //                        FirstName = c.CharSkeleton.FirstName,
        //                        LastName = c.CharSkeleton.LastName,
        //                        Gender = c.CharSkeleton.Gender,
        //                        VisualDescription = c.CharSkeleton.VisualDescription,
        //                        BackgroundDescription = c.CharSkeleton.BackgroundDescription,
        //                        CharacterNotes = c.CharSkeleton.CharacterNotes,
        //                        HeightInInches = c.CharSkeleton.HeightInInches,
        //                        WeightInPounds = c.CharSkeleton.WeightInPounds,
        //                        PersonalityTraits = c.CharSkeleton.PersonalityTraits,
        //                        Ideals = c.CharSkeleton.Ideals,
        //                        Bonds = c.CharSkeleton.Bonds,
        //                        Flaws = c.CharSkeleton.Flaws,
        //                        DateOfCreation = c.CharSkeleton.DateOfCreation,
        //                        DateOfModification = c.CharSkeleton.DateOfModification
        //                    };
        //                    myChar.Race = new RaceDisplay
        //                    {
        //                        RaceID = c.RaceID,
        //                        DateOfCreation = c.Race.DateOfCreation,
        //                        Name = c.Race.Name,
        //                        Speed = c.Race.Speed,
        //                        Size = c.Race.Size,
        //                        Trait = c.Race.Trait,
        //                        TraitDescription = c.Race.TraitDescription,
        //                        Languages = c.Race.Languages.Split('|').ToList(),
        //                        WeaponProficiencies = c.Race.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.Race.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.Race.ToolProficiencies.Split('|').ToList(),
        //                        Strength = c.Race.Strength,
        //                        Dexterity = c.Race.Dexterity,
        //                        Constitution = c.Race.Constitution,
        //                        Intelligence = c.Race.Intelligence,
        //                        Wisdom = c.Race.Wisdom,
        //                        Charisma = c.Race.Charisma
        //                    };
        //                    myChar.Class = classList;
        //                    myChar.Background = new BackgroundDisplay
        //                    {
        //                        BackgroundID = c.BackgroundID,
        //                        BackgroundName = c.Background.BackgroundName,
        //                        BackgroundDescription = c.Background.BackgroundDescription,
        //                        WeaponProficiencies = c.Background.WeaponProficiencies.Split('|').ToList(),
        //                        ArmorProficiencies = c.Background.ArmorProficiencies.Split('|').ToList(),
        //                        ToolProficiencies = c.Background.ToolProficiencies.Split('|').ToList(),
        //                        Strength = c.Background.Strength,
        //                        Dexterity = c.Background.Dexterity,
        //                        Constitution = c.Background.Constitution,
        //                        Intelligence = c.Background.Intelligence,
        //                        Wisdom = c.Background.Wisdom,
        //                        Charisma = c.Background.Charisma,
        //                        DateOfCreation = c.Background.DateOfCreation,
        //                        DateOfModification = c.Background.DateOfModification
        //                    };
        //                    myChar.Features = featList;
        //                    myChar.ProficiencySkills = skillList;
        //                    myChar.InventoryItems = itemList;
        //                    myChar.Alignment = c.Alignment;
        //                    myChar.OtherLanguages = c.OtherLanguages;
        //                    myChar.AttacksAndSpells = c.AttacksAndSpells;
        //                    myChar.HitPoints = c.HitPoints;
        //                    myChar.CurrentHitPoints = c.CurrentHitPoints;
        //                    myChar.ExperiencePoints = c.ExperiencePoints;
        //                    myChar.Level = c.Level;
        //                    myChar.Strength = c.Strength;
        //                    myChar.Dexterity = c.Dexterity;
        //                    myChar.Constitution = c.Constitution;
        //                    myChar.Intelligence = c.Intelligence;
        //                    myChar.Wisdom = c.Wisdom;
        //                    myChar.Charisma = c.Charisma;
        //                    myChar.CarryWeight = c.CarryWeight;
        //                    myChar.GoldPieces = c.GoldPieces;
        //                    myChar.SilverPieces = c.SilverPieces;
        //                    myChar.CopperPieces = c.CopperPieces;
        //                    myChar.CharacterNotes = c.CharacterNotes;
        //                    myChar.DateOfCreation = c.DateOfCreation;
        //                    myChar.DateOfModification = c.DateOfModification;
        //                }
        //                else
        //                {
        //                    foreach (var cla in c.Class)
        //                    {
        //                        var newClass = new ClassDisplay
        //                        {
        //                            ClassID = cla.ClassID,
        //                            ClassDescription = cla.Class.ClassDescription,
        //                            HitDie = cla.Class.HitDie,
        //                            SavingThrows = cla.Class.SavingThrows.Split('|').ToList(),
        //                            WeaponProficiencies = cla.Class.WeaponProficiencies.Split('|').ToList(),
        //                            ArmorProficiencies = cla.Class.ArmorProficiencies.Split('|').ToList(),
        //                            ToolProficiencies = cla.Class.ToolProficiencies.Split('|').ToList(),
        //                            Strength = cla.Class.Strength,
        //                            Dexterity = cla.Class.Dexterity,
        //                            Constitution = cla.Class.Constitution,
        //                            Intelligence = cla.Class.Intelligence,
        //                            Wisdom = cla.Class.Wisdom,
        //                            Charisma = cla.Class.Charisma,
        //                            DateOfCreation = cla.Class.DateOfCreation,
        //                            DateOfModification = cla.Class.DateOfModification
        //                        };
        //                        classList.Add(newClass);
        //                    }
        //                    var otherC = new InstanceOthersDisplay
        //                    {
        //                        CharInstanceID = c.CharInstanceID,
        //                        OwnerID = c.OwnerID,
        //                        RoomID = c.RoomID,
        //                        RoomName = room.RoomName,
        //                        CharacterName = c.CharSkeleton.FirstName + c.CharSkeleton.LastName,
        //                        Gender = c.CharSkeleton.Gender,
        //                        VisualDescription = c.CharSkeleton.VisualDescription,
        //                        Race = new RaceDisplay
        //                        {
        //                            RaceID = c.RaceID,
        //                            DateOfCreation = c.Race.DateOfCreation,
        //                            Name = c.Race.Name,
        //                            Speed = c.Race.Speed,
        //                            Size = c.Race.Size,
        //                            Trait = c.Race.Trait,
        //                            TraitDescription = c.Race.TraitDescription,
        //                            Languages = c.Race.Languages.Split('|').ToList(),
        //                            WeaponProficiencies = c.Race.WeaponProficiencies.Split('|').ToList(),
        //                            ArmorProficiencies = c.Race.ArmorProficiencies.Split('|').ToList(),
        //                            ToolProficiencies = c.Race.ToolProficiencies.Split('|').ToList(),
        //                            Strength = c.Race.Strength,
        //                            Dexterity = c.Race.Dexterity,
        //                            Constitution = c.Race.Constitution,
        //                            Intelligence = c.Race.Intelligence,
        //                            Wisdom = c.Race.Wisdom,
        //                            Charisma = c.Race.Charisma
        //                        },
        //                        Class = classList,
        //                        Background = new BackgroundDisplay
        //                        {
        //                            BackgroundID = c.BackgroundID,
        //                            BackgroundName = c.Background.BackgroundName,
        //                            BackgroundDescription = c.Background.BackgroundDescription,
        //                            WeaponProficiencies = c.Background.WeaponProficiencies.Split('|').ToList(),
        //                            ArmorProficiencies = c.Background.ArmorProficiencies.Split('|').ToList(),
        //                            ToolProficiencies = c.Background.ToolProficiencies.Split('|').ToList(),
        //                            Strength = c.Background.Strength,
        //                            Dexterity = c.Background.Dexterity,
        //                            Constitution = c.Background.Constitution,
        //                            Intelligence = c.Background.Intelligence,
        //                            Wisdom = c.Background.Wisdom,
        //                            Charisma = c.Background.Charisma,
        //                            DateOfCreation = c.Background.DateOfCreation,
        //                            DateOfModification = c.Background.DateOfModification
        //                        },
        //                        DateOfCreation = c.DateOfCreation
        //                    };
        //                    otherChars.Add(otherC);
        //                }
        //            }
        //            var display = new RoomPlayerDisplay
        //            {
        //                RoomID = room.RoomID,
        //                RoomCreatorUsername = gmUsername,
        //                RoomName = room.RoomName,
        //                GameType = room.GameType.ToString(),
        //                PlayerUsernames = players,
        //                RoomClasses = classes,
        //                RoomBackgrounds = backgrounds,
        //                RoomFeatures = features,
        //                RoomRaces = races,
        //                RoomSkills = skills,
        //                MyCharacter = myChar,
        //                OtherCharacters = otherChars,
        //                DateOfCreation = room.DateOfCreation
        //            };
        //            return display;
        //        }
        //    }
        //}







        ////Create new Room (and notes) as GM
        //public bool CreateRoom(RoomGMCreate newRoomCreate)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var newNotes = new RoomNotes
        //        {
        //            OwnerID = _userID,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        ctx.RoomNotes.Add(newNotes);
        //        bool save = ctx.SaveChanges() == 1;
        //        var newRoom = new Room
        //        {
        //            OwnerID = _userID,
        //            RoomName = newRoomCreate.RoomName,
        //            GameType = newRoomCreate.GameType,
        //            RoomNotesID = newNotes.NotesID,
        //            DateOfCreation = DateTimeOffset.UtcNow,
        //        };
        //        ctx.Rooms.Add(newRoom);
        //        return (ctx.SaveChanges() == 1 && save == true);
        //    }
        //}
        ////update as GM Room settings
        //public bool UpdateRoom(int roomId, RoomGMUpdateSettings gmRoomUpdates)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var room = ctx.Rooms.Where(e => e.OwnerID == _userID).SingleOrDefault(e => e.RoomID == roomId);
        //        if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "" && gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
        //        {
        //            room.RoomName = gmRoomUpdates.RoomName;
        //            room.GameType = gmRoomUpdates.GameType;
        //            room.DateOfModification = DateTimeOffset.UtcNow;
        //            return ctx.SaveChanges() == 1;
        //        }
        //        else if (gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
        //        {
        //            room.GameType = gmRoomUpdates.GameType;
        //            room.DateOfModification = DateTimeOffset.UtcNow;
        //            return ctx.SaveChanges() == 1;
        //        }
        //        else if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "")
        //        {
        //            room.RoomName = gmRoomUpdates.RoomName;
        //            room.DateOfModification = DateTimeOffset.UtcNow;
        //            return ctx.SaveChanges() == 1;
        //        }
        //        else return false;
        //    }
        //}
        ////update as GM room notes
        //public bool UpdateRoomNotes(int roomId, RoomGMUpdateNotes gmNote)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        var rNotes = ctx.RoomNotes.Single(e => e.NotesID == room.RoomNotesID);
        //        if (gmNote.PlayerOneNotes != null && gmNote.PlayerOneNotes != "") { rNotes.PlayerOneNotes = gmNote.PlayerOneNotes; };
        //        if (gmNote.PlayerTwoNotes != null && gmNote.PlayerTwoNotes != "") { rNotes.PlayerTwoNotes = gmNote.PlayerTwoNotes; };
        //        if (gmNote.PlayerThreeNotes != null && gmNote.PlayerThreeNotes != "") { rNotes.PlayerThreeNotes = gmNote.PlayerThreeNotes; };
        //        if (gmNote.PlayerFourNotes != null && gmNote.PlayerFourNotes != "") { rNotes.PlayerFourNotes = gmNote.PlayerFourNotes; };
        //        if (gmNote.PlayerFiveNotes != null && gmNote.PlayerFiveNotes != "") { rNotes.PlayerFiveNotes = gmNote.PlayerFiveNotes; };
        //        if (gmNote.PlayerSixNotes != null && gmNote.PlayerSixNotes != "") { rNotes.PlayerSixNotes = gmNote.PlayerSixNotes; };
        //        if (gmNote.PlayerSevenNotes != null && gmNote.PlayerSevenNotes != "") { rNotes.PlayerSevenNotes = gmNote.PlayerSevenNotes; };

        //        rNotes.DateOfModification = DateTimeOffset.UtcNow;
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////delete room I own
        //public bool DeleteGMRoom(int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        int count = 0;
        //        var room = ctx.Rooms.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        var roomNotes = ctx.RoomNotes.SingleOrDefault(e => e.OwnerID == _userID && e.NotesID == room.RoomNotesID);

        //        var roomUsers = ctx.RoomUsers.Where(e => e.RoomID == roomId);
        //        foreach (var r in roomUsers) { ctx.RoomUsers.Remove(r); }
        //        var roomClasses = ctx.RoomClasses.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        foreach (var c in roomClasses) { ctx.RoomClasses.Remove(c); }
        //        var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        foreach (var r in roomRaces) { ctx.RoomRaces.Remove(r); }
        //        var proSkills = ctx.RoomProficiencies.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        foreach (var p in proSkills) { ctx.RoomProficiencies.Remove(p); }
        //        var roomBackgrounds = ctx.RoomBackgrounds.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        foreach (var b in roomBackgrounds) { ctx.RoomBackgrounds.Remove(b); }
        //        var roomFeatures = ctx.RoomFeatures.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        foreach (var f in roomFeatures) { ctx.RoomFeatures.Remove(f); }
        //        ctx.RoomNotes.Remove(roomNotes);
        //        ctx.Rooms.Remove(room);

        //        count = 2 + roomUsers.Count() + roomClasses.Count() + roomRaces.Count() + proSkills.Count() + roomBackgrounds.Count() + roomFeatures.Count();
        //        return ctx.SaveChanges() == count;
        //    }
        //}
        ////add player to room
        //public string AddPlayerToRoom(int roomId, string playerUsername)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var username = ctx.Users.SingleOrDefault(e => e.UserName == playerUsername);
        //        if (username == null) return "Username not found";
        //        var room = ctx.Rooms.Single(e => e.RoomID == roomId);
        //        var isPlayerAlreadyInRoom = room.RoomUsers.SingleOrDefault(e => e.PlayerUsername == playerUsername && e.PlayerID == Guid.Parse(username.Id));
        //        if (isPlayerAlreadyInRoom == null)
        //        {
        //            if (room.RoomUsers.Count() < 7)
        //            {
        //                var newRoomUser = new RoomUsers
        //                {
        //                    RoomID = roomId,
        //                    PlayerID = Guid.Parse(username.Id),
        //                    PlayerUsername = username.UserName,
        //                    DateOfCreation = DateTimeOffset.UtcNow
        //                };
        //                room.RoomUsers.Add(newRoomUser);
        //                if (ctx.SaveChanges() == 2) return "Player added to room"; // or is this 1??
        //                else return "Player not added to room";
        //            }
        //            else return "Already 7 players in room";
        //        }
        //        else return "Player already in room";
        //    }
        //}
        ////Remove player from my room
        //public bool RemovePlayerFromGMRoom(int roomId, string playerUsername)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomUser = ctx.RoomUsers.Single(e => e.RoomID == roomId && e.PlayerUsername == playerUsername);
        //        var charInsta = ctx.CharacterInstances.SingleOrDefault(e => e.OwnerID == roomUser.PlayerID && e.RoomID == roomId);
        //        charInsta.RoomID = null;
        //        ctx.RoomUsers.Remove(roomUser);
        //        return ctx.SaveChanges() == 2;
        //    }
        //}
        ////delete myself from a room as player
        //public bool RemoveSelfAsPlayerFromRoom(int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomUsers = ctx.RoomUsers.SingleOrDefault(e => e.RoomID == roomId);
        //        var charInsta = ctx.CharacterInstances.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        charInsta.RoomID = null;
        //        ctx.RoomUsers.Remove(roomUsers);
        //        return ctx.SaveChanges() == 2;
        //    }
        //}
        ////create character instance
        //public string CreateInstancedCharacter(InstanceCreate character)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var oldCharacter = ctx.CharacterInstances.SingleOrDefault(e => e.RoomID == character.RoomID);
        //        if (oldCharacter == null)
        //        {
        //            var newChar = new CharacterInstanced
        //            {
        //                OwnerID = _userID,
        //                RoomID = character.RoomID,
        //                CharSkeletonID = character.CharSkeletonID,
        //                ClassID = character.ClassID,
        //                RaceID = character.RaceID,
        //                BackgroundID = character.BackgroundID,
        //                Alignment = character.Alignment,
        //                OtherLanguages = character.OtherLanguages,
        //                HitPoints = character.HitPoints,
        //                CurrentHitPoints = character.HitPoints,
        //                ExperiencePoints = 0,
        //                Level = 1,
        //                ProficiencyBonus = 0,
        //                Strength = character.Strength,
        //                Dexterity = character.Dexterity,
        //                Constitution = character.Constitution,
        //                Intelligence = character.Intelligence,
        //                Wisdom = character.Wisdom,
        //                Charisma = character.Charisma,
        //                StrMod = StatModifier(character.Strength),
        //                DexMod = StatModifier(character.Dexterity),
        //                ConMod = StatModifier(character.Constitution),
        //                IntMod = StatModifier(character.Intelligence),
        //                WisMod = StatModifier(character.Wisdom),
        //                ChaMod = StatModifier(character.Charisma),
        //                CarryWeight = character.Strength * 15,
        //                PlatinumPieces = character.PlatinumPieces,
        //                GoldPieces = character.GoldPieces,
        //                ElectrumPieces = character.ElectrumPieces,
        //                SilverPieces = character.SilverPieces,
        //                CopperPieces = character.CopperPieces,
        //                CharacterNotes = "",
        //                DateOfCreation = DateTimeOffset.UtcNow
        //            };
        //            ctx.CharacterInstances.Add(newChar);
        //            if (ctx.SaveChanges() == 1) return "Character created"; //1 is number of editted table rows
        //            else return "Character not created, something went wrong";
        //        }
        //        else return "Character instance already in room";
        //    }
        //}


        ////characteristic controls for room
        ////add class to a room
        //public bool AddClassToRoom(int classId, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomClass = new RoomClasses
        //        {
        //            OwnerID = _userID,
        //            ClassID = classId,
        //            RoomID = roomId,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        room.RoomClasses.Add(roomClass);
        //        return ctx.SaveChanges() == 1; //or is this 2??
        //    }
        //}
        ////remove class from room
        //public bool RemoveClassToRoom(int classId, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var result = ctx.RoomClasses.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.ClassID == classId);
        //        ctx.RoomClasses.Remove(result);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////add class to a room
        //public bool AddBackgroundsToRoom(int backgroundId, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomBack = new RoomBackgrounds
        //        {
        //            OwnerID = _userID,
        //            RoomID = roomId,
        //            BackgroundID = backgroundId,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        room.RoomBackgrounds.Add(roomBack);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////remove class from room
        //public bool RemoveBackgroundsToRoom(int backgroundId, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomBack = ctx.RoomBackgrounds.Single(e => e.OwnerID == _userID && e.BackgroundID == backgroundId && e.RoomID == roomId);
        //        ctx.RoomBackgrounds.Remove(roomBack);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////add feature to my room
        //public bool AddFeatureToRoom(int id, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomFeat = new RoomFeatures
        //        {
        //            OwnerID = _userID,
        //            RoomID = roomId,
        //            FeatureID = id,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        room.RoomFeatures.Add(roomFeat);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////remove feature from my room
        //public bool RemoveFeatureFromRoom(int id, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomFeat = ctx.RoomFeatures.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.FeatureID == id);
        //        ctx.RoomFeatures.Remove(roomFeat);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////add race to a room
        //public bool AddRaceToRoom(int raceId, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var result = new RoomRaces
        //        {
        //            OwnerID = _userID,
        //            RaceID = raceId,
        //            RoomID = roomId,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        room.RoomRaces.Add(result);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////remove race from room
        //public bool RemoveRaceFromRoom(int id, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var result = ctx.RoomRaces.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.RaceID == id);
        //        ctx.RoomRaces.Remove(result);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////add feature to my room
        //public bool AddSkillToRoom(int id, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var roomSkill = new RoomSkills
        //        {
        //            OwnerID = _userID,
        //            RoomID = roomId,
        //            ProficiencySkillID = id,
        //            DateOfCreation = DateTimeOffset.UtcNow
        //        };
        //        var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
        //        room.RoomProficiencies.Add(roomSkill);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}
        ////remove feature from my room
        //public bool RemoveSkillFromRoom(int id, int roomId)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var room = ctx.RoomProficiencies.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.ProficiencySkillID == id);
        //        ctx.RoomProficiencies.Remove(room);
        //        return ctx.SaveChanges() == 1;
        //    }
        //}

        //private int StatModifier(double stat)
        //{
        //    if (stat == 1) return -5;
        //    else if (stat == 2 || stat == 3) return -4;
        //    else if (stat == 4 || stat == 5) return -3;
        //    else if (stat == 6 || stat == 7) return -2;
        //    else if (stat == 8 || stat == 9) return -1;
        //    else if (stat == 10 || stat == 11) return 0;
        //    else if (stat == 12 || stat == 13) return 1;
        //    else if (stat == 14 || stat == 15) return 2;
        //    else if (stat == 16 || stat == 17) return 3;
        //    else if (stat == 18 || stat == 19) return 4;
        //    else if (stat == 20 || stat == 21) return 5;
        //    else if (stat == 22 || stat == 23) return 6;
        //    else if (stat == 24 || stat == 25) return 7;
        //    else if (stat == 26 || stat == 27) return 8;
        //    else if (stat == 28 || stat == 29) return 9;
        //    else if (stat == 30) return 10;
        //    else return 0;
        //}
    }
}
