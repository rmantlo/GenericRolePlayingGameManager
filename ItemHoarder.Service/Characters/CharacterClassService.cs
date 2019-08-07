using ItemHoarder.Data;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class CharacterClassService
    {
        private readonly Guid _userId;
        public CharacterClassService(Guid userId)
        {
            _userId = userId;
        }
        //get all my classes
        public IEnumerable<ClassDisplay> GetAllMyClasses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.CharacterClasses.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new ClassDisplay
                {
                    ClassID = e.ClassID,
                    ClassName = e.ClassName,
                    ClassDescription = e.ClassDescription,
                    HitDie = e.HitDie,
                    SavingThrows = e.SavingThrows,
                    ProficiencySkills = e.ProficiencySkills,
                    Strength = e.Strength,
                    Dexterity = e.Dexterity,
                    Constitution = e.Constitution,
                    Intelligence = e.Intelligence,
                    Wisdom = e.Wisdom,
                    Charisma = e.Charisma
                }).ToArray();
                return results;
            }
        }
        //get classes by room?
        public IEnumerable<ClassDisplay> GetClassesByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomClasses.Where(e => e.OwnerID == _userId && e.RoomID == roomId).ToList();
                List<ClassDisplay> roomClasses = new List<ClassDisplay>();
                foreach (var c in rooms)
                {
                    var aClass = ctx.CharacterClasses.Single(e => e.OwnerID == _userId && e.ClassID == c.ClassID);
                    var classDisplay = new ClassDisplay
                    {
                        ClassID = aClass.ClassID,
                        ClassName = aClass.ClassName,
                        ClassDescription = aClass.ClassDescription,
                        HitDie = aClass.HitDie,
                        SavingThrows = aClass.SavingThrows,
                        ProficiencySkills = aClass.ProficiencySkills,
                        Strength = aClass.Strength,
                        Dexterity = aClass.Dexterity,
                        Constitution = aClass.Constitution,
                        Intelligence = aClass.Intelligence,
                        Wisdom = aClass.Wisdom,
                        Charisma = aClass.Charisma
                    };
                    roomClasses.Add(classDisplay);
                }
                return roomClasses;
            }
        }
        //get class by id
        public ClassDisplay GetClassById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var aClass = ctx.CharacterClasses.Single(e => e.OwnerID == _userId && e.ClassID == id);
                var classDisplay = new ClassDisplay
                {
                    ClassID = aClass.ClassID,
                    ClassName = aClass.ClassName,
                    ClassDescription = aClass.ClassDescription,
                    HitDie = aClass.HitDie,
                    SavingThrows = aClass.SavingThrows,
                    ProficiencySkills = aClass.ProficiencySkills,
                    Strength = aClass.Strength,
                    Dexterity = aClass.Dexterity,
                    Constitution = aClass.Constitution,
                    Intelligence = aClass.Intelligence,
                    Wisdom = aClass.Wisdom,
                    Charisma = aClass.Charisma
                };
                return classDisplay;
            }
        }
        //get classes in room as player
        public IEnumerable<ClassDisplay> GetClassesInRoomAsPlayer(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId));
                List<ClassDisplay> roomClasses = new List<ClassDisplay>();
                if (AmInRoom)
                {
                    var rooms = ctx.RoomClasses.Where(e => e.RoomID == roomId).ToList();
                    foreach (var c in rooms)
                    {
                        var aClass = ctx.CharacterClasses.Single(e => e.OwnerID == c.OwnerID && e.ClassID == c.ClassID);
                        var classDisplay = new ClassDisplay
                        {
                            ClassID = aClass.ClassID,
                            ClassName = aClass.ClassName,
                            ClassDescription = aClass.ClassDescription,
                            HitDie = aClass.HitDie,
                            SavingThrows = aClass.SavingThrows,
                            ProficiencySkills = aClass.ProficiencySkills,
                            Strength = aClass.Strength,
                            Dexterity = aClass.Dexterity,
                            Constitution = aClass.Constitution,
                            Intelligence = aClass.Intelligence,
                            Wisdom = aClass.Wisdom,
                            Charisma = aClass.Charisma
                        };
                        roomClasses.Add(classDisplay);
                    }
                    return roomClasses;
                }
                else return null;
            }
        }
        //create class
        public bool CreateClass(ClassCreate newClass)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var aClass = new CharacterClass
                {
                    OwnerID = _userId,
                    ClassName = newClass.ClassName,
                    ClassDescription = newClass.ClassDescription,
                    HitDie = newClass.HitDie,
                    SavingThrows = newClass.SavingThrows,
                    ProficiencySkills = newClass.ProficiencySkills,
                    Strength = newClass.Strength,
                    Dexterity = newClass.Dexterity,
                    Constitution = newClass.Constitution,
                    Intelligence = newClass.Intelligence,
                    Wisdom = newClass.Wisdom,
                    Charisma = newClass.Charisma,
                    IsDeactivated = false,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterClasses.Add(aClass);
                return ctx.SaveChanges() == 1;
            }
        }
        //update class
        public bool UpdateClass(int id, ClassCreate updates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterClasses.Single(e => e.OwnerID == _userId && e.ClassID == id);
                result.ClassName = updates.ClassName;
                result.ClassDescription = updates.ClassDescription;
                result.HitDie = updates.HitDie;
                result.SavingThrows = updates.SavingThrows;
                result.ProficiencySkills = updates.ProficiencySkills;
                result.Strength = updates.Strength;
                result.Dexterity = updates.Dexterity;
                result.Constitution = updates.Constitution;
                result.Intelligence = updates.Intelligence;
                result.Wisdom = updates.Wisdom;
                result.Charisma = updates.Charisma;
                result.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //add class to a room
        public bool AddClassToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomClass = new RoomClasses
                {
                    OwnerID = _userId,
                    ClassID = classId,
                    RoomID = roomId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomClasses.Add(roomClass);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove class from room
        public bool RemoveClassToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.RoomClasses.Single(e => e.OwnerID == _userId && e.RoomID == roomId && e.ClassID == classId);
                ctx.RoomClasses.Remove(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //delete class
        public bool DeleteClass(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomClasses = ctx.RoomClasses.Where(e => e.OwnerID == _userId && e.ClassID == id).ToList();
                foreach (var c in roomClasses)
                {
                    ctx.RoomClasses.Remove(c);
                }
                var aClass = ctx.CharacterClasses.Single(e => e.OwnerID == _userId && e.ClassID == id);
                aClass.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + roomClasses.Count();
            }
        }
    }
}
