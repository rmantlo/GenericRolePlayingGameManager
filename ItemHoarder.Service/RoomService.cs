using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Backgrounds;
using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Models.Characters.Features;
using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Models.Characters.ProficiencySkills;
using ItemHoarder.Models.Characters.Races;
using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Models.Rooms;
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
        //get all rooms I own
        public IEnumerable<RoomGMDisplay> GetOwnedRooms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID).ToList();
                List<RoomGMDisplay> displayRooms = new List<RoomGMDisplay>();
                foreach (var i in room)
                {
                    var chars = ctx.CharacterInstances.Where(e => e.RoomID == i.RoomID).ToList();
                    List<string> usernames = new List<string>();
                    foreach (var n in i.RoomUsers)
                    {
                        usernames.Add(n.PlayerUsername);
                    }
                    List<ClassDisplay> classes = new List<ClassDisplay>();
                    List<RaceDisplay> races = new List<RaceDisplay>();
                    List<BackgroundDisplay> backgrounds = new List<BackgroundDisplay>();
                    List<FeatureDisplay> features = new List<FeatureDisplay>();
                    List<SkillDisplay> skills = new List<SkillDisplay>();
                    List<InstanceGMDisplay> characters = new List<InstanceGMDisplay>();

                    List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                    List<FeatureDisplay> featList = new List<FeatureDisplay>();
                    List<SkillDisplay> skillList = new List<SkillDisplay>();

                    foreach (var c in i.RoomClasses)
                    {
                        var newClass = new ClassDisplay
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
                            Charisma = c.Class.Charisma,
                            DateOfCreation = c.Class.DateOfCreation,
                            DateOfModification = c.Class.DateOfModification
                        };
                        classes.Add(newClass);
                    }
                    foreach (var race in i.RoomRaces)
                    {
                        var newRace = new RaceDisplay
                        {
                            RaceID = race.Race.RaceID,
                            Name = race.Race.Name,
                            Speed = race.Race.Speed,
                            Size = race.Race.Size,
                            Languages = race.Race.Languages,
                            Proficiencies = race.Race.Proficiencies,
                            Trait = race.Race.Trait,
                            TraitDescription = race.Race.TraitDescription,
                            Strength = race.Race.Strength,
                            Dexterity = race.Race.Dexterity,
                            Constitution = race.Race.Constitution,
                            Intelligence = race.Race.Intelligence,
                            Wisdom = race.Race.Wisdom,
                            Charisma = race.Race.Charisma,
                            DateOfCreation = race.Race.DateOfCreation,
                            DateOfModification = race.Race.DateOfModification
                        };
                        races.Add(newRace);
                    }
                    foreach (var b in i.RoomBackgrounds)
                    {
                        var back = new BackgroundDisplay
                        {
                            BackgroundID = b.Background.BackgroundID,
                            BackgroundName = b.Background.BackgroundName,
                            BackgroundDescription = b.Background.BackgroundDescription,
                            Proficiencies = b.Background.Proficiencies,
                            Strength = b.Background.Strength,
                            Dexterity = b.Background.Dexterity,
                            Constitution = b.Background.Constitution,
                            Intelligence = b.Background.Intelligence,
                            Wisdom = b.Background.Wisdom,
                            Charisma = b.Background.Charisma,
                            DateOfCreation = b.Background.DateOfCreation,
                            DateOfModification = b.Background.DateOfModification
                        };
                        backgrounds.Add(back);
                    }
                    foreach (var f in i.RoomFeatures)
                    {
                        var feat = new FeatureDisplay
                        {
                            FeatureID = f.Feature.FeatureID,
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
                            Charisma = f.Feature.Charisma,
                            DateOfCreation = f.Feature.DateOfCreation,
                            DateOfModification = f.Feature.DateOfModification
                        };
                        features.Add(feat);
                    }
                    foreach (var s in i.RoomProficiencies)
                    {
                        var skill = new SkillDisplay
                        {
                            ID = s.Skill.ID,
                            Name = s.Skill.Name,
                            Description = s.Skill.Description,
                            ClassesApplied = s.Skill.ClassesApplied,
                            RacesApplied = s.Skill.RacesApplied,
                            BackgroundsApplied = s.Skill.BackgroundsApplied,
                            StatApplied = s.Skill.StatApplied,
                            Strength = s.Skill.Strength,
                            Dexterity = s.Skill.Dexterity,
                            Constitution = s.Skill.Constitution,
                            Intelligence = s.Skill.Intelligence,
                            Wisdom = s.Skill.Wisdom,
                            Charisma = s.Skill.Charisma,
                            DateOfCreation = s.Skill.DateOfCreation,
                            DateOfModification = s.Skill.DateOfModification
                        };
                        skills.Add(skill);
                    }
                    foreach (var c in chars)
                    {
                        foreach (var item in c.InventoryItems)
                        {
                            var it = new InstanceItemDisplay
                            {
                                ItemID = item.ItemID,
                                Name = item.Name,
                                Description = item.Description,
                                Weight = item.Weight,
                                HitPoints = item.HitPoints,
                                ItemRarity = item.ItemRarity,
                                ItemClass = item.ItemClass,
                                ClassType = item.ClassType,
                                Damage = item.Damage,
                                DamageResiliance = item.DamageResiliance,
                                IsEquiptable = item.IsEquiptable,
                                IsEquipted = item.IsEquipted,
                                Strength = item.Strength,
                                Dexterity = item.Dexterity,
                                Constitution = item.Constitution,
                                Intelligence = item.Intelligence,
                                Wisdom = item.Wisdom,
                                Charisma = item.Charisma,
                                DateOfCreation = item.DateOfCreation,
                            };
                            itemList.Add(it);
                        }
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
                                Charisma = f.Feature.Charisma,
                            };
                            featList.Add(feature);
                        }
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
                        var instance = new InstanceGMDisplay
                        {
                            CharInstanceID = c.CharInstanceID,
                            OwnerID = c.OwnerID,
                            RoomID = c.RoomID,
                            RoomName = i.RoomName,
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
                                Flaws = c.CharSkeleton.Flaws,
                                DateOfCreation = c.CharSkeleton.DateOfCreation,
                                DateOfModification = c.CharSkeleton.DateOfModification
                            },
                            Race = new RaceDisplay
                            {
                                RaceID = c.RaceID,
                                DateOfCreation = c.Race.DateOfCreation,
                                Name = c.Race.Name,
                                Speed = c.Race.Speed,
                                Size = c.Race.Size,
                                Trait = c.Race.Trait,
                                TraitDescription = c.Race.TraitDescription,
                                Languages = c.Race.Languages,
                                Proficiencies = c.Race.Proficiencies,
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
                                ClassDescription = c.Class.ClassDescription,
                                HitDie = c.Class.HitDie,
                                SavingThrows = c.Class.SavingThrows,
                                Proficiencies = c.Class.Proficiencies,
                                Strength = c.Class.Strength,
                                Dexterity = c.Class.Dexterity,
                                Constitution = c.Class.Constitution,
                                Intelligence = c.Class.Intelligence,
                                Wisdom = c.Class.Wisdom,
                                Charisma = c.Class.Charisma,
                                DateOfCreation = c.Class.DateOfCreation,
                                DateOfModification = c.Class.DateOfModification
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
                                Charisma = c.Background.Charisma,
                                DateOfCreation = c.Background.DateOfCreation,
                                DateOfModification = c.Background.DateOfModification
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
                            DateOfCreation = c.DateOfCreation,
                            DateOfModification = c.DateOfModification,
                        };
                        characters.Add(instance);
                    }
                    var display = new RoomGMDisplay
                    {
                        RoomID = i.RoomID,
                        RoomName = i.RoomName,
                        GameType = i.GameType,
                        PlayerUsernames = usernames,
                        RoomNotes = new RoomNoteDisplay
                        {
                            PlayerOneNotes = i.RoomNotes.PlayerOneNotes,
                            PlayerTwoNotes = i.RoomNotes.PlayerTwoNotes,
                            PlayerThreeNotes = i.RoomNotes.PlayerThreeNotes,
                            PlayerFourNotes = i.RoomNotes.PlayerFourNotes,
                            PlayerFiveNotes = i.RoomNotes.PlayerFiveNotes,
                            PlayerSixNotes = i.RoomNotes.PlayerSixNotes,
                            PlayerSevenNotes = i.RoomNotes.PlayerSevenNotes,
                            GeneralNotes = i.RoomNotes.GeneralNotes,
                            DateOfModification = i.RoomNotes.DateOfModification
                        },
                        RoomClasses = classes,
                        RoomBackgrounds = backgrounds,
                        RoomRaces = races,
                        RoomFeatures = features,
                        RoomSkills = skills,
                        Characters = characters,
                        DateOfCreation = i.DateOfCreation,
                        DateOfModification = i.DateOfModification
                    };
                    displayRooms.Add(display);
                }
                return displayRooms;
            }
        }
        //get rooms Im a player in
        public IEnumerable<RoomPlayerDisplay> GetPlayerRooms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<RoomPlayerDisplay> displayRooms = new List<RoomPlayerDisplay>();
                var roomUsers = ctx.RoomUsers.Where(e => e.PlayerID == _userID).ToList();
                foreach (var r in roomUsers)
                {
                    var room = ctx.Rooms.Single(e => e.RoomID == r.RoomID);
                    var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString());
                    var chars = ctx.CharacterInstances.Where(e => e.RoomID == room.RoomID).ToList();
                    List<string> players = new List<string>();
                    foreach (var u in room.RoomUsers)
                    {
                        players.Add(u.PlayerUsername);
                    }
                    List<ClassDisplay> classes = new List<ClassDisplay>();
                    List<RaceDisplay> races = new List<RaceDisplay>();
                    List<BackgroundDisplay> backgrounds = new List<BackgroundDisplay>();
                    List<FeatureDisplay> features = new List<FeatureDisplay>();
                    List<SkillDisplay> skills = new List<SkillDisplay>();
                    var myChar = new InstanceDisplay();
                    List<InstanceOthersDisplay> otherChars = new List<InstanceOthersDisplay>();
                    foreach (var c in room.RoomClasses)
                    {
                        var newClass = new ClassDisplay
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
                            Charisma = c.Class.Charisma,
                            DateOfCreation = c.Class.DateOfCreation,
                            DateOfModification = c.Class.DateOfModification
                        };
                        classes.Add(newClass);
                    }
                    foreach (var race in room.RoomRaces)
                    {
                        var newRace = new RaceDisplay
                        {
                            RaceID = race.Race.RaceID,
                            Name = race.Race.Name,
                            Speed = race.Race.Speed,
                            Size = race.Race.Size,
                            Languages = race.Race.Languages,
                            Proficiencies = race.Race.Proficiencies,
                            Trait = race.Race.Trait,
                            TraitDescription = race.Race.TraitDescription,
                            Strength = race.Race.Strength,
                            Dexterity = race.Race.Dexterity,
                            Constitution = race.Race.Constitution,
                            Intelligence = race.Race.Intelligence,
                            Wisdom = race.Race.Wisdom,
                            Charisma = race.Race.Charisma,
                            DateOfCreation = race.Race.DateOfCreation,
                            DateOfModification = race.Race.DateOfModification
                        };
                        races.Add(newRace);
                    }
                    foreach (var b in room.RoomBackgrounds)
                    {
                        var back = new BackgroundDisplay
                        {
                            BackgroundID = b.Background.BackgroundID,
                            BackgroundName = b.Background.BackgroundName,
                            BackgroundDescription = b.Background.BackgroundDescription,
                            Proficiencies = b.Background.Proficiencies,
                            Strength = b.Background.Strength,
                            Dexterity = b.Background.Dexterity,
                            Constitution = b.Background.Constitution,
                            Intelligence = b.Background.Intelligence,
                            Wisdom = b.Background.Wisdom,
                            Charisma = b.Background.Charisma,
                            DateOfCreation = b.Background.DateOfCreation,
                            DateOfModification = b.Background.DateOfModification
                        };
                        backgrounds.Add(back);
                    }
                    foreach (var f in room.RoomFeatures)
                    {
                        var feat = new FeatureDisplay
                        {
                            FeatureID = f.Feature.FeatureID,
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
                            Charisma = f.Feature.Charisma,
                            DateOfCreation = f.Feature.DateOfCreation,
                            DateOfModification = f.Feature.DateOfModification
                        };
                        features.Add(feat);
                    }
                    foreach (var s in room.RoomProficiencies)
                    {
                        var skill = new SkillDisplay
                        {
                            ID = s.Skill.ID,
                            Name = s.Skill.Name,
                            Description = s.Skill.Description,
                            ClassesApplied = s.Skill.ClassesApplied,
                            RacesApplied = s.Skill.RacesApplied,
                            BackgroundsApplied = s.Skill.BackgroundsApplied,
                            StatApplied = s.Skill.StatApplied,
                            Strength = s.Skill.Strength,
                            Dexterity = s.Skill.Dexterity,
                            Constitution = s.Skill.Constitution,
                            Intelligence = s.Skill.Intelligence,
                            Wisdom = s.Skill.Wisdom,
                            Charisma = s.Skill.Charisma,
                            DateOfCreation = s.Skill.DateOfCreation,
                            DateOfModification = s.Skill.DateOfModification
                        };
                        skills.Add(skill);
                    }
                    List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                    List<FeatureDisplay> featList = new List<FeatureDisplay>();
                    List<SkillDisplay> skillList = new List<SkillDisplay>();
                    foreach (var c in chars)
                    {
                        if (c.OwnerID == _userID)
                        {
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
                                    Charisma = f.Feature.Charisma,
                                };
                                featList.Add(feature);
                            }
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
                            myChar.CharInstanceID = c.CharInstanceID;
                            myChar.OwnerID = c.OwnerID;
                            myChar.RoomID = c.RoomID;
                            myChar.RoomName = room.RoomName;
                            myChar.CharSkeleton = new CharSkeleDisplay
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
                                Flaws = c.CharSkeleton.Flaws,
                                DateOfCreation = c.CharSkeleton.DateOfCreation,
                                DateOfModification = c.CharSkeleton.DateOfModification
                            };
                            myChar.Race = new RaceDisplay
                            {
                                RaceID = c.RaceID,
                                DateOfCreation = c.Race.DateOfCreation,
                                Name = c.Race.Name,
                                Speed = c.Race.Speed,
                                Size = c.Race.Size,
                                Trait = c.Race.Trait,
                                TraitDescription = c.Race.TraitDescription,
                                Languages = c.Race.Languages,
                                Proficiencies = c.Race.Proficiencies,
                                Strength = c.Race.Strength,
                                Dexterity = c.Race.Dexterity,
                                Constitution = c.Race.Constitution,
                                Intelligence = c.Race.Intelligence,
                                Wisdom = c.Race.Wisdom,
                                Charisma = c.Race.Charisma
                            };
                            myChar.Class = new ClassDisplay
                            {
                                ClassID = c.ClassID,
                                ClassDescription = c.Class.ClassDescription,
                                HitDie = c.Class.HitDie,
                                SavingThrows = c.Class.SavingThrows,
                                Proficiencies = c.Class.Proficiencies,
                                Strength = c.Class.Strength,
                                Dexterity = c.Class.Dexterity,
                                Constitution = c.Class.Constitution,
                                Intelligence = c.Class.Intelligence,
                                Wisdom = c.Class.Wisdom,
                                Charisma = c.Class.Charisma,
                                DateOfCreation = c.Class.DateOfCreation,
                                DateOfModification = c.Class.DateOfModification
                            };
                            myChar.Background = new BackgroundDisplay
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
                                Charisma = c.Background.Charisma,
                                DateOfCreation = c.Background.DateOfCreation,
                                DateOfModification = c.Background.DateOfModification
                            };
                            myChar.Features = featList;
                            myChar.ProficiencySkills = skillList;
                            myChar.InventoryItems = itemList;
                            myChar.Alignment = c.Alignment;
                            myChar.OtherLanguages = c.OtherLanguages;
                            myChar.AttacksAndSpells = c.AttacksAndSpells;
                            myChar.HitPoints = c.HitPoints;
                            myChar.CurrentHitPoints = c.CurrentHitPoints;
                            myChar.ExperiencePoints = c.ExperiencePoints;
                            myChar.Level = c.Level;
                            myChar.ProficiencyBonus = c.ProficiencyBonus;
                            myChar.Strength = c.Strength;
                            myChar.Dexterity = c.Dexterity;
                            myChar.Constitution = c.Constitution;
                            myChar.Intelligence = c.Intelligence;
                            myChar.Wisdom = c.Wisdom;
                            myChar.Charisma = c.Charisma;
                            myChar.StrMod = c.StrMod;
                            myChar.DexMod = c.DexMod;
                            myChar.ConMod = c.ConMod;
                            myChar.IntMod = c.IntMod;
                            myChar.WisMod = c.WisMod;
                            myChar.ChaMod = c.ChaMod;
                            myChar.CarryWeight = c.CarryWeight;
                            myChar.GoldPieces = c.GoldPieces;
                            myChar.SilverPieces = c.SilverPieces;
                            myChar.CopperPieces = c.CopperPieces;
                            myChar.CharacterNotes = c.CharacterNotes;
                            myChar.DateOfCreation = c.DateOfCreation;
                            myChar.DateOfModification = c.DateOfModification;
                        }
                        else
                        {
                            var otherC = new InstanceOthersDisplay
                            {
                                CharInstanceID = c.CharInstanceID,
                                OwnerID = c.OwnerID,
                                RoomID = c.RoomID,
                                RoomName = room.RoomName,
                                CharacterName = c.CharSkeleton.FirstName + c.CharSkeleton.LastName,
                                Gender = c.CharSkeleton.Gender,
                                VisualDescription = c.CharSkeleton.VisualDescription,
                                Race = new RaceDisplay
                                {
                                    RaceID = c.RaceID,
                                    DateOfCreation = c.Race.DateOfCreation,
                                    Name = c.Race.Name,
                                    Speed = c.Race.Speed,
                                    Size = c.Race.Size,
                                    Trait = c.Race.Trait,
                                    TraitDescription = c.Race.TraitDescription,
                                    Languages = c.Race.Languages,
                                    Proficiencies = c.Race.Proficiencies,
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
                                    ClassDescription = c.Class.ClassDescription,
                                    HitDie = c.Class.HitDie,
                                    SavingThrows = c.Class.SavingThrows,
                                    Proficiencies = c.Class.Proficiencies,
                                    Strength = c.Class.Strength,
                                    Dexterity = c.Class.Dexterity,
                                    Constitution = c.Class.Constitution,
                                    Intelligence = c.Class.Intelligence,
                                    Wisdom = c.Class.Wisdom,
                                    Charisma = c.Class.Charisma,
                                    DateOfCreation = c.Class.DateOfCreation,
                                    DateOfModification = c.Class.DateOfModification
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
                                    Charisma = c.Background.Charisma,
                                    DateOfCreation = c.Background.DateOfCreation,
                                    DateOfModification = c.Background.DateOfModification
                                },
                                DateOfCreation = c.DateOfCreation
                            };
                            otherChars.Add(otherC);
                        }
                    }
                    var display = new RoomPlayerDisplay
                    {
                        RoomID = room.RoomID,
                        RoomCreatorUsername = gmUsername.UserName,
                        RoomName = room.RoomName,
                        GameType = room.GameType,
                        PlayerUsernames = players,
                        RoomClasses = classes,
                        RoomRaces = races,
                        RoomBackgrounds = backgrounds,
                        RoomFeatures = features,
                        RoomSkills = skills,
                        MyCharacter = myChar,
                        OtherCharacters = otherChars,
                        DateOfCreation = room.DateOfCreation
                    };
                    displayRooms.Add(display);
                }
                return displayRooms;
            }
        }
        //get player room by Id
        public object GetRoomById(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Single(e => e.RoomID == roomId);
                var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString());
                var chars = ctx.CharacterInstances.Where(e => e.RoomID == roomId).ToList();
                List<string> players = new List<string>();
                foreach (var u in room.RoomUsers)
                {
                    players.Add(u.PlayerUsername);
                }
                List<ClassDisplay> classes = new List<ClassDisplay>();
                List<RaceDisplay> races = new List<RaceDisplay>();
                List<BackgroundDisplay> backgrounds = new List<BackgroundDisplay>();
                List<FeatureDisplay> features = new List<FeatureDisplay>();
                List<SkillDisplay> skills = new List<SkillDisplay>();
                var myChar = new InstanceDisplay();
                List<InstanceOthersDisplay> otherChars = new List<InstanceOthersDisplay>();
                foreach (var c in room.RoomClasses)
                {
                    var newClass = new ClassDisplay
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
                        Charisma = c.Class.Charisma,
                        DateOfCreation = c.Class.DateOfCreation,
                        DateOfModification = c.Class.DateOfModification
                    };
                    classes.Add(newClass);
                }
                foreach (var race in room.RoomRaces)
                {
                    var newRace = new RaceDisplay
                    {
                        RaceID = race.Race.RaceID,
                        Name = race.Race.Name,
                        Speed = race.Race.Speed,
                        Size = race.Race.Size,
                        Languages = race.Race.Languages,
                        Proficiencies = race.Race.Proficiencies,
                        Trait = race.Race.Trait,
                        TraitDescription = race.Race.TraitDescription,
                        Strength = race.Race.Strength,
                        Dexterity = race.Race.Dexterity,
                        Constitution = race.Race.Constitution,
                        Intelligence = race.Race.Intelligence,
                        Wisdom = race.Race.Wisdom,
                        Charisma = race.Race.Charisma,
                        DateOfCreation = race.Race.DateOfCreation,
                        DateOfModification = race.Race.DateOfModification
                    };
                    races.Add(newRace);
                }
                foreach (var b in room.RoomBackgrounds)
                {
                    var back = new BackgroundDisplay
                    {
                        BackgroundID = b.Background.BackgroundID,
                        BackgroundName = b.Background.BackgroundName,
                        BackgroundDescription = b.Background.BackgroundDescription,
                        Proficiencies = b.Background.Proficiencies,
                        Strength = b.Background.Strength,
                        Dexterity = b.Background.Dexterity,
                        Constitution = b.Background.Constitution,
                        Intelligence = b.Background.Intelligence,
                        Wisdom = b.Background.Wisdom,
                        Charisma = b.Background.Charisma,
                        DateOfCreation = b.Background.DateOfCreation,
                        DateOfModification = b.Background.DateOfModification
                    };
                    backgrounds.Add(back);
                }
                foreach (var f in room.RoomFeatures)
                {
                    var feat = new FeatureDisplay
                    {
                        FeatureID = f.Feature.FeatureID,
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
                        Charisma = f.Feature.Charisma,
                        DateOfCreation = f.Feature.DateOfCreation,
                        DateOfModification = f.Feature.DateOfModification
                    };
                    features.Add(feat);
                }
                foreach (var s in room.RoomProficiencies)
                {
                    var skill = new SkillDisplay
                    {
                        ID = s.Skill.ID,
                        Name = s.Skill.Name,
                        Description = s.Skill.Description,
                        ClassesApplied = s.Skill.ClassesApplied,
                        RacesApplied = s.Skill.RacesApplied,
                        BackgroundsApplied = s.Skill.BackgroundsApplied,
                        StatApplied = s.Skill.StatApplied,
                        Strength = s.Skill.Strength,
                        Dexterity = s.Skill.Dexterity,
                        Constitution = s.Skill.Constitution,
                        Intelligence = s.Skill.Intelligence,
                        Wisdom = s.Skill.Wisdom,
                        Charisma = s.Skill.Charisma,
                        DateOfCreation = s.Skill.DateOfCreation,
                        DateOfModification = s.Skill.DateOfModification
                    };
                    skills.Add(skill);
                }
                List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                List<FeatureDisplay> featList = new List<FeatureDisplay>();
                List<SkillDisplay> skillList = new List<SkillDisplay>();
                if (room.OwnerID == _userID)
                {
                    List<InstanceGMDisplay> characters = new List<InstanceGMDisplay>();
                    foreach (var c in chars)
                    {
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
                                Charisma = f.Feature.Charisma,
                            };
                            featList.Add(feature);
                        }
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
                        var instance = new InstanceGMDisplay
                        {
                            CharInstanceID = c.CharInstanceID,
                            OwnerID = c.OwnerID,
                            RoomID = c.RoomID,
                            RoomName = room.RoomName,
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
                                Flaws = c.CharSkeleton.Flaws,
                                DateOfCreation = c.CharSkeleton.DateOfCreation,
                                DateOfModification = c.CharSkeleton.DateOfModification
                            },
                            Race = new RaceDisplay
                            {
                                RaceID = c.RaceID,
                                DateOfCreation = c.Race.DateOfCreation,
                                Name = c.Race.Name,
                                Speed = c.Race.Speed,
                                Size = c.Race.Size,
                                Trait = c.Race.Trait,
                                TraitDescription = c.Race.TraitDescription,
                                Languages = c.Race.Languages,
                                Proficiencies = c.Race.Proficiencies,
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
                                ClassDescription = c.Class.ClassDescription,
                                HitDie = c.Class.HitDie,
                                SavingThrows = c.Class.SavingThrows,
                                Proficiencies = c.Class.Proficiencies,
                                Strength = c.Class.Strength,
                                Dexterity = c.Class.Dexterity,
                                Constitution = c.Class.Constitution,
                                Intelligence = c.Class.Intelligence,
                                Wisdom = c.Class.Wisdom,
                                Charisma = c.Class.Charisma,
                                DateOfCreation = c.Class.DateOfCreation,
                                DateOfModification = c.Class.DateOfModification
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
                                Charisma = c.Background.Charisma,
                                DateOfCreation = c.Background.DateOfCreation,
                                DateOfModification = c.Background.DateOfModification
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
                            DateOfCreation = c.DateOfCreation,
                            DateOfModification = c.DateOfModification,
                        };
                        characters.Add(instance);
                    }
                    var display = new RoomGMDisplay
                    {
                        RoomID = room.RoomID,
                        RoomName = room.RoomName,
                        GameType = room.GameType,
                        RoomNotes = new RoomNoteDisplay
                        {
                            PlayerOneNotes = room.RoomNotes.PlayerOneNotes,
                            PlayerTwoNotes = room.RoomNotes.PlayerTwoNotes,
                            PlayerThreeNotes = room.RoomNotes.PlayerThreeNotes,
                            PlayerFourNotes = room.RoomNotes.PlayerFourNotes,
                            PlayerFiveNotes = room.RoomNotes.PlayerFiveNotes,
                            PlayerSixNotes = room.RoomNotes.PlayerSixNotes,
                            PlayerSevenNotes = room.RoomNotes.PlayerSevenNotes,
                            GeneralNotes = room.RoomNotes.GeneralNotes,
                            DateOfModification = room.RoomNotes.DateOfModification
                        },
                        PlayerUsernames = players,
                        RoomClasses = classes,
                        RoomBackgrounds = backgrounds,
                        RoomFeatures = features,
                        RoomRaces = races,
                        RoomSkills = skills,
                        Characters = characters,
                        DateOfCreation = room.DateOfCreation
                    };
                    return display;
                }
                else
                {
                    foreach (var c in chars)
                    {
                        if (c.OwnerID == _userID)
                        {
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
                                    Charisma = f.Feature.Charisma,
                                };
                                featList.Add(feature);
                            }
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
                            myChar.CharInstanceID = c.CharInstanceID;
                            myChar.OwnerID = c.OwnerID;
                            myChar.RoomID = c.RoomID;
                            myChar.RoomName = room.RoomName;
                            myChar.CharSkeleton = new CharSkeleDisplay
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
                                Flaws = c.CharSkeleton.Flaws,
                                DateOfCreation = c.CharSkeleton.DateOfCreation,
                                DateOfModification = c.CharSkeleton.DateOfModification
                            };
                            myChar.Race = new RaceDisplay
                            {
                                RaceID = c.RaceID,
                                DateOfCreation = c.Race.DateOfCreation,
                                Name = c.Race.Name,
                                Speed = c.Race.Speed,
                                Size = c.Race.Size,
                                Trait = c.Race.Trait,
                                TraitDescription = c.Race.TraitDescription,
                                Languages = c.Race.Languages,
                                Proficiencies = c.Race.Proficiencies,
                                Strength = c.Race.Strength,
                                Dexterity = c.Race.Dexterity,
                                Constitution = c.Race.Constitution,
                                Intelligence = c.Race.Intelligence,
                                Wisdom = c.Race.Wisdom,
                                Charisma = c.Race.Charisma
                            };
                            myChar.Class = new ClassDisplay
                            {
                                ClassID = c.ClassID,
                                ClassDescription = c.Class.ClassDescription,
                                HitDie = c.Class.HitDie,
                                SavingThrows = c.Class.SavingThrows,
                                Proficiencies = c.Class.Proficiencies,
                                Strength = c.Class.Strength,
                                Dexterity = c.Class.Dexterity,
                                Constitution = c.Class.Constitution,
                                Intelligence = c.Class.Intelligence,
                                Wisdom = c.Class.Wisdom,
                                Charisma = c.Class.Charisma,
                                DateOfCreation = c.Class.DateOfCreation,
                                DateOfModification = c.Class.DateOfModification
                            };
                            myChar.Background = new BackgroundDisplay
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
                                Charisma = c.Background.Charisma,
                                DateOfCreation = c.Background.DateOfCreation,
                                DateOfModification = c.Background.DateOfModification
                            };
                            myChar.Features = featList;
                            myChar.ProficiencySkills = skillList;
                            myChar.InventoryItems = itemList;
                            myChar.Alignment = c.Alignment;
                            myChar.OtherLanguages = c.OtherLanguages;
                            myChar.AttacksAndSpells = c.AttacksAndSpells;
                            myChar.HitPoints = c.HitPoints;
                            myChar.CurrentHitPoints = c.CurrentHitPoints;
                            myChar.ExperiencePoints = c.ExperiencePoints;
                            myChar.Level = c.Level;
                            myChar.ProficiencyBonus = c.ProficiencyBonus;
                            myChar.Strength = c.Strength;
                            myChar.Dexterity = c.Dexterity;
                            myChar.Constitution = c.Constitution;
                            myChar.Intelligence = c.Intelligence;
                            myChar.Wisdom = c.Wisdom;
                            myChar.Charisma = c.Charisma;
                            myChar.StrMod = c.StrMod;
                            myChar.DexMod = c.DexMod;
                            myChar.ConMod = c.ConMod;
                            myChar.IntMod = c.IntMod;
                            myChar.WisMod = c.WisMod;
                            myChar.ChaMod = c.ChaMod;
                            myChar.CarryWeight = c.CarryWeight;
                            myChar.GoldPieces = c.GoldPieces;
                            myChar.SilverPieces = c.SilverPieces;
                            myChar.CopperPieces = c.CopperPieces;
                            myChar.CharacterNotes = c.CharacterNotes;
                            myChar.DateOfCreation = c.DateOfCreation;
                            myChar.DateOfModification = c.DateOfModification;
                        }
                        else
                        {
                            var otherC = new InstanceOthersDisplay
                            {
                                CharInstanceID = c.CharInstanceID,
                                OwnerID = c.OwnerID,
                                RoomID = c.RoomID,
                                RoomName = room.RoomName,
                                CharacterName = c.CharSkeleton.FirstName + c.CharSkeleton.LastName,
                                Gender = c.CharSkeleton.Gender,
                                VisualDescription = c.CharSkeleton.VisualDescription,
                                Race = new RaceDisplay
                                {
                                    RaceID = c.RaceID,
                                    DateOfCreation = c.Race.DateOfCreation,
                                    Name = c.Race.Name,
                                    Speed = c.Race.Speed,
                                    Size = c.Race.Size,
                                    Trait = c.Race.Trait,
                                    TraitDescription = c.Race.TraitDescription,
                                    Languages = c.Race.Languages,
                                    Proficiencies = c.Race.Proficiencies,
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
                                    ClassDescription = c.Class.ClassDescription,
                                    HitDie = c.Class.HitDie,
                                    SavingThrows = c.Class.SavingThrows,
                                    Proficiencies = c.Class.Proficiencies,
                                    Strength = c.Class.Strength,
                                    Dexterity = c.Class.Dexterity,
                                    Constitution = c.Class.Constitution,
                                    Intelligence = c.Class.Intelligence,
                                    Wisdom = c.Class.Wisdom,
                                    Charisma = c.Class.Charisma,
                                    DateOfCreation = c.Class.DateOfCreation,
                                    DateOfModification = c.Class.DateOfModification
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
                                    Charisma = c.Background.Charisma,
                                    DateOfCreation = c.Background.DateOfCreation,
                                    DateOfModification = c.Background.DateOfModification
                                },
                                DateOfCreation = c.DateOfCreation
                            };
                            otherChars.Add(otherC);
                        }
                    }
                    var display = new RoomPlayerDisplay
                    {
                        RoomID = room.RoomID,
                        RoomCreatorUsername = gmUsername.UserName,
                        RoomName = room.RoomName,
                        GameType = room.GameType,
                        PlayerUsernames = players,
                        RoomClasses = classes,
                        RoomBackgrounds = backgrounds,
                        RoomFeatures = features,
                        RoomRaces = races,
                        RoomSkills = skills,
                        MyCharacter = myChar,
                        OtherCharacters = otherChars,
                        DateOfCreation = room.DateOfCreation
                    };
                    return display;
                }
            }
        }

        //Create new Room (and notes) as GM
        public bool CreateRoom(RoomGMCreate newRoomCreate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newNotes = new RoomNotes
                {
                    OwnerID = _userID,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomNotes.Add(newNotes);
                bool save = ctx.SaveChanges() == 1;
                var newRoom = new Room
                {
                    OwnerID = _userID,
                    RoomName = newRoomCreate.RoomName,
                    GameType = newRoomCreate.GameType,
                    RoomNotesID = newNotes.NotesID,
                    DateOfCreation = DateTimeOffset.UtcNow,
                };
                ctx.Rooms.Add(newRoom);
                return (ctx.SaveChanges() == 1 && save == true);
            }
        }
        //update as GM Room settings
        public bool UpdateRoom(int roomId, RoomGMUpdateSettings gmRoomUpdates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID).SingleOrDefault(e => e.RoomID == roomId);
                if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "" && gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
                {
                    room.RoomName = gmRoomUpdates.RoomName;
                    room.GameType = gmRoomUpdates.GameType;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else if (gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
                {
                    room.GameType = gmRoomUpdates.GameType;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "")
                {
                    room.RoomName = gmRoomUpdates.RoomName;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //update as GM room notes
        public bool UpdateRoomNotes(int roomId, RoomGMUpdateNotes gmNote)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                var rNotes = ctx.RoomNotes.Single(e => e.NotesID == room.RoomNotesID);
                if (gmNote.PlayerOneNotes != null && gmNote.PlayerOneNotes != "") { rNotes.PlayerOneNotes = gmNote.PlayerOneNotes; };
                if (gmNote.PlayerTwoNotes != null && gmNote.PlayerTwoNotes != "") { rNotes.PlayerTwoNotes = gmNote.PlayerTwoNotes; };
                if (gmNote.PlayerThreeNotes != null && gmNote.PlayerThreeNotes != "") { rNotes.PlayerThreeNotes = gmNote.PlayerThreeNotes; };
                if (gmNote.PlayerFourNotes != null && gmNote.PlayerFourNotes != "") { rNotes.PlayerFourNotes = gmNote.PlayerFourNotes; };
                if (gmNote.PlayerFiveNotes != null && gmNote.PlayerFiveNotes != "") { rNotes.PlayerFiveNotes = gmNote.PlayerFiveNotes; };
                if (gmNote.PlayerSixNotes != null && gmNote.PlayerSixNotes != "") { rNotes.PlayerSixNotes = gmNote.PlayerSixNotes; };
                if (gmNote.PlayerSevenNotes != null && gmNote.PlayerSevenNotes != "") { rNotes.PlayerSevenNotes = gmNote.PlayerSevenNotes; };

                rNotes.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete room I own
        public bool DeleteGMRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                int count = 0;
                var room = ctx.Rooms.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                var roomNotes = ctx.RoomNotes.SingleOrDefault(e => e.OwnerID == _userID && e.NotesID == room.RoomNotesID);

                var roomUsers = ctx.RoomUsers.Where(e => e.RoomID == roomId);
                foreach (var r in roomUsers) { ctx.RoomUsers.Remove(r); }
                var roomClasses = ctx.RoomClasses.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var c in roomClasses) { ctx.RoomClasses.Remove(c); }
                var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var r in roomRaces) { ctx.RoomRaces.Remove(r); }
                var proSkills = ctx.RoomProficiencies.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var p in proSkills) { ctx.RoomProficiencies.Remove(p); }
                var roomBackgrounds = ctx.RoomBackgrounds.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var b in roomBackgrounds) { ctx.RoomBackgrounds.Remove(b); }
                var roomFeatures = ctx.RoomFeatures.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var f in roomFeatures) { ctx.RoomFeatures.Remove(f); }
                ctx.RoomNotes.Remove(roomNotes);
                ctx.Rooms.Remove(room);

                count = 2 + roomUsers.Count() + roomClasses.Count() + roomRaces.Count() + proSkills.Count() + roomBackgrounds.Count() + roomFeatures.Count();
                return ctx.SaveChanges() == count;
            }
        }
        //add player to room
        public string AddPlayerToRoom(int roomId, string playerUsername)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var username = ctx.Users.SingleOrDefault(e => e.UserName == playerUsername);
                if (username == null) return "Username not found";
                var room = ctx.Rooms.Single(e => e.RoomID == roomId);
                var isPlayerAlreadyInRoom = room.RoomUsers.SingleOrDefault(e => e.PlayerUsername == playerUsername && e.PlayerID == Guid.Parse(username.Id));
                if (isPlayerAlreadyInRoom == null)
                {
                    if (room.RoomUsers.Count() < 7)
                    {
                        var newRoomUser = new RoomUsers
                        {
                            RoomID = roomId,
                            PlayerID = Guid.Parse(username.Id),
                            PlayerUsername = username.UserName,
                            DateOfCreation = DateTimeOffset.UtcNow
                        };
                        room.RoomUsers.Add(newRoomUser);
                        if (ctx.SaveChanges() == 2) return "Player added to room"; // or is this 1??
                        else return "Player not added to room";
                    }
                    else return "Already 7 players in room";
                }
                else return "Player already in room";
            }
        }
        //Remove player from my room
        public bool RemovePlayerFromGMRoom(int roomId, string playerUsername)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomUser = ctx.RoomUsers.Single(e => e.RoomID == roomId && e.PlayerUsername == playerUsername);
                var charInsta = ctx.CharacterInstances.SingleOrDefault(e => e.OwnerID == roomUser.PlayerID && e.RoomID == roomId);
                charInsta.RoomID = null;
                ctx.RoomUsers.Remove(roomUser);
                return ctx.SaveChanges() == 2;
            }
        }
        //delete myself from a room as player
        public bool RemoveSelfAsPlayerFromRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomUsers = ctx.RoomUsers.SingleOrDefault(e => e.RoomID == roomId);
                var charInsta = ctx.CharacterInstances.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                charInsta.RoomID = null;
                ctx.RoomUsers.Remove(roomUsers);
                return ctx.SaveChanges() == 2;
            }
        }
        //create character instance
        public string CreateInstancedCharacter(InstanceCreate character)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldCharacter = ctx.CharacterInstances.SingleOrDefault(e => e.RoomID == character.RoomID);
                if (oldCharacter == null)
                {
                    var newChar = new CharacterInstanced
                    {
                        OwnerID = _userID,
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
                        CarryWeight = character.Strength * 15,
                        PlatinumPieces = character.PlatinumPieces,
                        GoldPieces = character.GoldPieces,
                        ElectrumPieces = character.ElectrumPieces,
                        SilverPieces = character.SilverPieces,
                        CopperPieces = character.CopperPieces,
                        CharacterNotes = "",
                        DateOfCreation = DateTimeOffset.UtcNow
                    };
                    ctx.CharacterInstances.Add(newChar);
                    if (ctx.SaveChanges() == 1) return "Character created"; //1 is number of editted table rows
                    else return "Character not created, something went wrong";
                }
                else return "Character instance already in room";
            }
        }


        //characteristic controls for room
        //add class to a room
        public bool AddClassToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomClass = new RoomClasses
                {
                    OwnerID = _userID,
                    ClassID = classId,
                    RoomID = roomId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                room.RoomClasses.Add(roomClass);
                return ctx.SaveChanges() == 1; //or is this 2??
            }
        }
        //remove class from room
        public bool RemoveClassToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.RoomClasses.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.ClassID == classId);
                ctx.RoomClasses.Remove(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //add class to a room
        public bool AddBackgroundsToRoom(int backgroundId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomBack = new RoomBackgrounds
                {
                    OwnerID = _userID,
                    RoomID = roomId,
                    BackgroundID = backgroundId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                room.RoomBackgrounds.Add(roomBack);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove class from room
        public bool RemoveBackgroundsToRoom(int backgroundId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomBack = ctx.RoomBackgrounds.Single(e => e.OwnerID == _userID && e.BackgroundID == backgroundId && e.RoomID == roomId);
                ctx.RoomBackgrounds.Remove(roomBack);
                return ctx.SaveChanges() == 1;
            }
        }
        //add feature to my room
        public bool AddFeatureToRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomFeat = new RoomFeatures
                {
                    OwnerID = _userID,
                    RoomID = roomId,
                    FeatureID = id,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                room.RoomFeatures.Add(roomFeat);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove feature from my room
        public bool RemoveFeatureFromRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomFeat = ctx.RoomFeatures.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.FeatureID == id);
                ctx.RoomFeatures.Remove(roomFeat);
                return ctx.SaveChanges() == 1;
            }
        }
        //add race to a room
        public bool AddRaceToRoom(int raceId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = new RoomRaces
                {
                    OwnerID = _userID,
                    RaceID = raceId,
                    RoomID = roomId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                room.RoomRaces.Add(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove race from room
        public bool RemoveRaceFromRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.RoomRaces.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.RaceID == id);
                ctx.RoomRaces.Remove(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //add feature to my room
        public bool AddSkillToRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomSkill = new RoomProficiencies
                {
                    OwnerID = _userID,
                    RoomID = roomId,
                    ProficiencySkillID = id,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                room.RoomProficiencies.Add(roomSkill);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove feature from my room
        public bool RemoveSkillFromRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.RoomProficiencies.Single(e => e.OwnerID == _userID && e.RoomID == roomId && e.ProficiencySkillID == id);
                ctx.RoomProficiencies.Remove(room);
                return ctx.SaveChanges() == 1;
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
