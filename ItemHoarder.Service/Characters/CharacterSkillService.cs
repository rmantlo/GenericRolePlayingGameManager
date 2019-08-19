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
    public class CharacterSkillService
    {
        private readonly Guid _userId;
        public CharacterSkillService(Guid userId)
        {
            _userId = userId;
        }
        public IEnumerable<SkillDisplay> GetAllMySkills()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.ProficiencySkills.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new SkillDisplay
                {
                    ID = e.ID,
                    Name = e.Name,
                    Description = e.Description,
                    ClassesApplied = e.ClassesApplied,
                    RacesApplied = e.RacesApplied,
                    BackgroundsApplied = e.BackgroundsApplied,
                    StatApplied = e.StatApplied,
                    Strength = e.Strength,
                    Dexterity = e.Dexterity,
                    Constitution = e.Constitution,
                    Intelligence = e.Intelligence,
                    Wisdom = e.Wisdom,
                    Charisma = e.Charisma,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToArray();
            }
        }
        //get by id
        public SkillDisplay GetSkillById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.ID == id);
                return new SkillDisplay
                {
                    ID = skill.ID,
                    Name = skill.Name,
                    Description = skill.Description,
                    ClassesApplied = skill.ClassesApplied,
                    RacesApplied = skill.RacesApplied,
                    BackgroundsApplied = skill.BackgroundsApplied,
                    StatApplied = skill.StatApplied,
                    Strength = skill.Strength,
                    Dexterity = skill.Dexterity,
                    Constitution = skill.Constitution,
                    Intelligence = skill.Intelligence,
                    Wisdom = skill.Wisdom,
                    Charisma = skill.Charisma,
                    DateOfCreation = skill.DateOfCreation,
                    DateOfModification = skill.DateOfModification
                };
            }
        }
        //get mine by room
        public IEnumerable<SkillDisplay> GetSkillsByMyRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomProficiencies.Where(e => e.OwnerID == _userId && e.RoomID == roomId);
                List<SkillDisplay> skills = new List<SkillDisplay>();
                foreach (var s in rooms)
                {
                    var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.ID == s.ProficiencySkillID);
                    var skillDisplay = new SkillDisplay
                    {
                        ID = skill.ID,
                        Name = skill.Name,
                        Description = skill.Description,
                        ClassesApplied = skill.ClassesApplied,
                        RacesApplied = skill.RacesApplied,
                        BackgroundsApplied = skill.BackgroundsApplied,
                        StatApplied = skill.StatApplied,
                        Strength = skill.Strength,
                        Dexterity = skill.Dexterity,
                        Constitution = skill.Constitution,
                        Intelligence = skill.Intelligence,
                        Wisdom = skill.Wisdom,
                        Charisma = skill.Charisma,
                        DateOfCreation = skill.DateOfCreation,
                        DateOfModification = skill.DateOfModification
                    };
                    skills.Add(skillDisplay);
                }
                return skills;
            }
        }
        //get features by room as player
        public IEnumerable<SkillDisplay> GetSkillsByRoomAsPlayer(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId));
                List<SkillDisplay> skills = new List<SkillDisplay>();
                if (AmInRoom)
                {
                    var rooms = ctx.RoomProficiencies.Where(e => e.RoomID == roomId);
                    foreach (var s in rooms)
                    {
                        var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == s.OwnerID && e.ID == s.ProficiencySkillID);
                        var skillDisplay = new SkillDisplay
                        {
                            ID = skill.ID,
                            Name = skill.Name,
                            Description = skill.Description,
                            ClassesApplied = skill.ClassesApplied,
                            RacesApplied = skill.RacesApplied,
                            BackgroundsApplied = skill.BackgroundsApplied,
                            StatApplied = skill.StatApplied,
                            Strength = skill.Strength,
                            Dexterity = skill.Dexterity,
                            Constitution = skill.Constitution,
                            Intelligence = skill.Intelligence,
                            Wisdom = skill.Wisdom,
                            Charisma = skill.Charisma
                        };
                        skills.Add(skillDisplay);
                    }
                    return skills;
                }
                else return null;
            }
        }
        //create feature
        public bool CreateSkill(SkillCreate skill)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var proSkill = new CharacterSkill
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    Name = skill.Name,
                    Description = skill.Description,
                    ClassesApplied = skill.ClassesApplied,
                    RacesApplied = skill.RacesApplied,
                    BackgroundsApplied = skill.BackgroundsApplied,
                    StatApplied = skill.StatApplied,
                    Strength = skill.Strength,
                    Dexterity = skill.Dexterity,
                    Constitution = skill.Constitution,
                    Intelligence = skill.Intelligence,
                    Wisdom = skill.Wisdom,
                    Charisma = skill.Charisma
                };
                ctx.ProficiencySkills.Add(proSkill);
                return ctx.SaveChanges() == 1;
            }
        }
        //update feature
        public bool UpdateSkill(int id, SkillCreate update)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.ID == id);
                skill.Name = update.Name;
                skill.Description = update.Description;
                skill.ClassesApplied = update.ClassesApplied;
                skill.RacesApplied = update.RacesApplied;
                skill.BackgroundsApplied = update.BackgroundsApplied;
                skill.StatApplied = update.StatApplied;
                skill.Strength = update.Strength;
                skill.Dexterity = update.Dexterity;
                skill.Constitution = update.Constitution;
                skill.Intelligence = update.Intelligence;
                skill.Wisdom = update.Wisdom;
                skill.Charisma = update.Charisma;
                skill.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete feature
        public bool DeleteSkill(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomProficiencies.Where(e => e.OwnerID == _userId && e.ProficiencySkillID == id).ToList();
                foreach (var r in rooms)
                {
                    ctx.RoomProficiencies.Remove(r);
                }
                var skill = ctx.ProficiencySkills.Single(e => e.OwnerID == _userId && e.ID == id);
                ctx.ProficiencySkills.Remove(skill);
                return ctx.SaveChanges() == 1 + rooms.Count();
            }
        }
    }
}
