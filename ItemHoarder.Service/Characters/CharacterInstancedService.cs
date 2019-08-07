using ItemHoarder.Data;
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
    public class CharacterInstancedService
    {
        private readonly Guid _userId;
        public CharacterInstancedService(Guid userId)
        {
            _userId = userId;
        }
        //get all character instance information
        //get character info by id
        public InstanceDisplay GetCharacterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var inventory = ctx.Inventories.Single(e => e.CharInstanceID == id);
                List<InstanceItemDisplay> itemList = new List<InstanceItemDisplay>();
                foreach (var i in inventory.InventoryItems)
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
                        SkillPrerequisite = f.SkillPrerequisite,
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
                        Strength = s.Strength,
                        Dexterity = s.Dexterity,
                        Constitution = s.Constitution,
                        Intelligence = s.Intelligence,
                        Wisdom = s.Wisdom,
                        Charisma = s.Charisma
                    };
                    skillList.Add(skill);
                }
                var result = ctx.CharacterInstances.Single(e => e.OwnerID == _userId && e.CharInstanceID == id);
                var character = new InstanceDisplay
                {
                    CharInstanceID = result.CharInstanceID,
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
                        ProficiencySkills = result.Class.ProficiencySkills,
                        Strength = result.Class.Strength,
                        Dexterity = result.Class.Dexterity,
                        Constitution = result.Class.Constitution,
                        Intelligence = result.Class.Intelligence,
                        Wisdom = result.Class.Wisdom,
                        Charisma = result.Class.Charisma
                    },
                    Features = featList,
                    ProficiencySkills = skillList,
                    InventoryItems = itemList,
                    Alignment = result.Alignment,
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
        //update character instance
        //Cant change class, race, or background
        //can change alignment, attacksAndSpells, characterNotes, money, strength etc,
        //hit points, carry weight determined by system IF DND game, otherwise it is manual
    }
}
