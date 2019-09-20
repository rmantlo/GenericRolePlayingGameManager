using ItemHoarder.Data;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Backgrounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class BackgroundService
    {
        private readonly Guid _userId;
        public BackgroundService(Guid userId)
        {
            _userId = userId;
        }
        //get all my backgrounds
        public IEnumerable<BackgroundIndex> GetAllMyBackgrounds()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.CharacterBackgrounds.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new BackgroundIndex
                {
                    BackgroundID = e.BackgroundID,
                    GameTag = e.GameTag,
                    BackgroundName = e.BackgroundName,
                    BackgroundDescription = e.BackgroundDescription,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                });
                return results;
            }
        }
        //get backgrounds by room for either GM or player
        public IEnumerable<BackgroundIndex> GetBackgroundsByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomBackgrounds.Where(e => e.RoomID == roomId).ToList();
                List<BackgroundIndex> roomBackgrounds = new List<BackgroundIndex>();
                foreach (var b in rooms)
                {
                    var backgrounds = ctx.CharacterBackgrounds.Single(e => e.BackgroundID == b.BackgroundID);
                    var backgroundDisplay = new BackgroundIndex
                    {
                        BackgroundID = backgrounds.BackgroundID,
                        GameTag = backgrounds.GameTag,
                        BackgroundName = backgrounds.BackgroundName,
                        BackgroundDescription = backgrounds.BackgroundDescription,
                        DateOfCreation = backgrounds.DateOfCreation,
                        DateOfModification = backgrounds.DateOfModification
                    };
                    roomBackgrounds.Add(backgroundDisplay);
                }
                return roomBackgrounds;
            }
        }
        //get background by id
        public BackgroundDetails GetBackgroundsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var background = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == id);
                Dictionary<int, string> features = new Dictionary<int, string>();
                Dictionary<int, string> skills = new Dictionary<int, string>();
                foreach (var f in background.FeatureIDs.Split('|').Select(int.Parse).ToList())
                {
                    var feat = ctx.CharacterFeatures.SingleOrDefault(e => e.FeatureID == f);
                    if (feat != null)
                    {
                        features.Add(feat.FeatureID, feat.FeatureName);
                    }
                }
                foreach (var s in background.SkillIDs.Split('|').Select(int.Parse).ToList())
                {
                    var skill = ctx.ProficiencySkills.SingleOrDefault(e => e.SkillID == s);
                    if (skill != null)
                    {
                        skills.Add(skill.SkillID, skill.SkillName);
                    }
                }
                return new BackgroundDetails
                {
                    BackgroundID = background.BackgroundID,
                    GameTag = background.GameTag,
                    BackgroundName = background.BackgroundName,
                    BackgroundDescription = background.BackgroundDescription,
                    WeaponProficiencies = background.WeaponProficiencies.Split('|').ToList(),
                    ArmorProficiencies = background.ArmorProficiencies.Split('|').ToList(),
                    ToolProficiencies = background.ToolProficiencies.Split('|').ToList(),
                    FeatureIDs = features,
                    SkillIDs = skills,
                    Strength = background.Strength,
                    Dexterity = background.Dexterity,
                    Constitution = background.Constitution,
                    Intelligence = background.Intelligence,
                    Wisdom = background.Wisdom,
                    Charisma = background.Charisma,
                    DateOfCreation = background.DateOfCreation,
                    DateOfModification = background.DateOfModification
                };
            }
        }
        //create background
        public bool CreateBackgrounds(BackgroundCreate newBackground)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var background = new CharacterBackground
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    GameTag = newBackground.GameTag,
                    BackgroundName = newBackground.BackgroundName,
                    BackgroundDescription = newBackground.BackgroundDescription,
                    WeaponProficiencies = String.Join("|", newBackground.WeaponProficiencies),
                    ArmorProficiencies = String.Join("|", newBackground.ArmorProficiencies),
                    ToolProficiencies = String.Join("|", newBackground.ToolProficiencies),
                    SkillIDs = String.Join("|", newBackground.SkillIDs),
                    FeatureIDs = String.Join("|", newBackground.FeatureIDs),
                    Strength = newBackground.Strength,
                    Dexterity = newBackground.Dexterity,
                    Constitution = newBackground.Constitution,
                    Intelligence = newBackground.Intelligence,
                    Wisdom = newBackground.Wisdom,
                    Charisma = newBackground.Charisma,
                    IsDeactivated = false
                };
                ctx.CharacterBackgrounds.Add(background);
                return ctx.SaveChanges() == 1;
            }
        }
        //update background
        public bool UpdateBackgrounds(int id, BackgroundCreate updates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldBack = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == id);
                oldBack.BackgroundName = updates.BackgroundName;
                oldBack.BackgroundDescription = updates.BackgroundDescription;
                oldBack.WeaponProficiencies = String.Join("|", updates.WeaponProficiencies);
                oldBack.ArmorProficiencies = String.Join("|", updates.ArmorProficiencies);
                oldBack.ToolProficiencies = String.Join("|", updates.ToolProficiencies);
                oldBack.SkillIDs = String.Join("|", updates.SkillIDs);
                oldBack.FeatureIDs = String.Join("|", updates.FeatureIDs);
                oldBack.Strength = updates.Strength;
                oldBack.Dexterity = updates.Dexterity;
                oldBack.Constitution = updates.Constitution;
                oldBack.Intelligence = updates.Intelligence;
                oldBack.Wisdom = updates.Wisdom;
                oldBack.Charisma = updates.Charisma;
                oldBack.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete background
        public bool DeleteBackgrounds(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomBacks = ctx.RoomBackgrounds.Where(e => e.OwnerID == _userId && e.BackgroundID == id).ToList();
                foreach (var b in roomBacks)
                {
                    ctx.RoomBackgrounds.Remove(b);
                }
                var background = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == id);
                ctx.CharacterBackgrounds.Remove(background);
                return ctx.SaveChanges() == 1 + roomBacks.Count();
            }
        }
    }
}
