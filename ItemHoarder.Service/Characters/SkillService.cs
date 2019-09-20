using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.ProficiencySkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class SkillService
    {
        private readonly Guid _userId;
        public SkillService(Guid userId)
        {
            _userId = userId;
        }
        public IEnumerable<SkillIndex> GetAllMySkills()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.ProficiencySkills.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new SkillIndex
                {
                    SkillID = e.SkillID,
                    GameTag = e.GameTag,
                    SkillName = e.SkillName,
                    Description = e.Description,
                    AbilityStatApplied = e.AbilityStatApplied,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToArray();
            }
        }
        //get index of skills by room
        public IEnumerable<SkillIndex> GetAllByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomProficiencies.Where(e => e.RoomID == roomId).ToList();
                List<SkillIndex> skills = new List<SkillIndex>();
                foreach (var s in rooms)
                {
                    var skill = ctx.ProficiencySkills.Single(e => e.SkillID == s.SkillID);
                    var skillDisplay = new SkillIndex
                    {
                        SkillID = skill.SkillID,
                        GameTag = skill.GameTag,
                        SkillName = skill.SkillName,
                        Description = skill.Description,
                        AbilityStatApplied = skill.AbilityStatApplied,
                        DateOfCreation = skill.DateOfCreation,
                        DateOfModification = skill.DateOfModification
                    };
                    skills.Add(skillDisplay);
                }
                return skills;
            }
        }
        //get skill details by id
        public SkillDetails GetSkillById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skill = ctx.ProficiencySkills.Single(e => e.SkillID == id);
                Dictionary<int, string> classes = new Dictionary<int, string>();
                foreach (var c in skill.ClassesIDs.Split('|').Select(int.Parse).ToList())
                {
                    var cl = ctx.CharacterClasses.SingleOrDefault(e => e.ClassID == c);
                    classes.Add(cl.ClassID, cl.ClassName);
                }
                Dictionary<int, string> races = new Dictionary<int, string>();
                foreach (var c in skill.RacesIDs.Split('|').Select(int.Parse).ToList())
                {
                    var ra = ctx.CharacterRaces.SingleOrDefault(e => e.RaceID == c);
                    races.Add(ra.RaceID, ra.RaceName);
                }
                Dictionary<int, string> backs = new Dictionary<int, string>();
                foreach (var c in skill.BackgroundsIDs.Split('|').Select(int.Parse).ToList())
                {
                    var b = ctx.CharacterBackgrounds.SingleOrDefault(e => e.BackgroundID == c);
                    backs.Add(b.BackgroundID, b.BackgroundName);
                }
                return new SkillDetails
                {
                    SkillID = skill.SkillID,
                    GameTag = skill.GameTag,
                    SkillName = skill.SkillName,
                    Description = skill.Description,
                    SpecialInfo = skill.SpecialInfo,
                    ActionType = skill.ActionType,
                    AbilityStatApplied = skill.AbilityStatApplied,
                    SkillChecks = skill.SkillChecks,
                    ClassesIDs = classes,
                    RacesIDs = races,
                    BackgroundsIDs = backs,
                    TrainedOnly = skill.TrainedOnly,
                    ArmorCheckPenalty = skill.ArmorCheckPenalty,
                    AttemptDetails = skill.AttemptDetails,
                    Restrictions = skill.Restrictions,
                    DateOfCreation = skill.DateOfCreation,
                    DateOfModification = skill.DateOfModification
                };
            }
        }
        //create skill
        public bool CreateSkill(SkillCreate skill)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var proSkill = new CharacterSkill
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    GameTag = skill.GameTag,
                    SkillName = skill.SkillName,
                    Description = skill.Description,
                    SpecialInfo = skill.SpecialInfo,
                    ClassesIDs = String.Join("|", skill.ClassesIDs),
                    RacesIDs = String.Join("|", skill.RacesIDs),
                    BackgroundsIDs = String.Join("|", skill.BackgroundsIDs),
                    AbilityStatApplied = skill.AbilityStatApplied,
                    SkillChecks = skill.SkillChecks,
                    TrainedOnly = skill.TrainedOnly,
                    ArmorCheckPenalty = skill.ArmorCheckPenalty,
                    ActionType = skill.ActionType,
                    AttemptDetails = skill.AttemptDetails,
                    Restrictions = skill.Restrictions,
                    IsDeactivated = false
                };
                ctx.ProficiencySkills.Add(proSkill);
                return ctx.SaveChanges() == 1;
            }
        }
        //update skill
        public bool UpdateSkill(int id, SkillCreate update)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.SkillID == id);
                skill.SkillName = update.SkillName;
                skill.GameTag = update.GameTag;
                skill.Description = update.Description;
                skill.SpecialInfo = update.SpecialInfo;
                skill.ActionType = update.ActionType;
                skill.AbilityStatApplied = update.AbilityStatApplied;
                skill.SkillChecks = update.SkillChecks;
                skill.ClassesIDs = String.Join("|", update.ClassesIDs);
                skill.RacesIDs = String.Join("|", update.RacesIDs);
                skill.BackgroundsIDs = String.Join("|", update.BackgroundsIDs);
                skill.TrainedOnly = update.TrainedOnly;
                skill.ArmorCheckPenalty = update.ArmorCheckPenalty;
                skill.AttemptDetails = update.AttemptDetails;
                skill.Restrictions = update.Restrictions;
                skill.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete skill
        public bool DeleteSkill(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomProficiencies.Where(e => e.OwnerID == _userId && e.SkillID == id).ToList();
                foreach (var r in rooms)
                {
                    ctx.RoomProficiencies.Remove(r);
                }
                var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.SkillID == id);
                skill.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + rooms.Count();
            }
        }
    }
}
