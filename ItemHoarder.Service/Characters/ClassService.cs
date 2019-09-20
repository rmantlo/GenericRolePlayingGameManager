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
    public class ClassService
    {
        private readonly Guid _userId;
        public ClassService(Guid userId)
        {
            _userId = userId;
        }
        //get all my classes, only GM can see
        public IEnumerable<ClassIndex> GetAllMyClasses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterClasses.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new ClassIndex
                {
                    ClassID = e.ClassID,
                    GameType = e.GameTag,
                    ClassName = e.ClassName,
                    ClassDescription = e.ClassDescription,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToList();
            }
        }
        //get class index list by room both gm and player can see
        public IEnumerable<ClassIndex> GetClassesByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomClasses.Where(e => e.RoomID == roomId).ToList();
                List<ClassIndex> roomClasses = new List<ClassIndex>();
                foreach (var c in rooms)
                {
                    var aClass = ctx.CharacterClasses.Single(e => e.ClassID == c.ClassID);
                    var classDisplay = new ClassIndex
                    {
                        ClassID = aClass.ClassID,
                        GameType = aClass.GameTag,
                        ClassName = aClass.ClassName,
                        ClassDescription = aClass.ClassDescription,
                        DateOfCreation = aClass.DateOfCreation,
                        DateOfModification = aClass.DateOfModification
                    };
                    roomClasses.Add(classDisplay);
                }
                return roomClasses;
            }
        }
        //get class details by id, GM and players can see
        public ClassDetails GetClassById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var aClass = ctx.CharacterClasses.Single(e => e.ClassID == id);
                Dictionary<int, string> subClassList = new Dictionary<int, string>();
                foreach (var s in aClass.SubClasses)
                {
                    subClassList.Add(s.SubClassID, s.SubClass.SubClassName);
                }
                var classDisplay = new ClassDetails
                {
                    ClassID = aClass.ClassID,
                    GameType = aClass.GameTag,
                    ClassName = aClass.ClassName,
                    ClassDescription = aClass.ClassDescription,
                    HitDie = aClass.HitDie,
                    SavingThrows = aClass.SavingThrows.Split('|').ToList(),
                    WeaponProficiencies = aClass.WeaponProficiencies.Split('|').ToList(),
                    ArmorProficiencies = aClass.ArmorProficiencies.Split('|').ToList(),
                    ToolProficiencies = aClass.ToolProficiencies.Split('|').ToList(),
                    SubClasses = subClassList,
                    Strength = aClass.Strength,
                    Dexterity = aClass.Dexterity,
                    Constitution = aClass.Constitution,
                    Intelligence = aClass.Intelligence,
                    Wisdom = aClass.Wisdom,
                    Charisma = aClass.Charisma,
                    DateOfCreation = aClass.DateOfCreation,
                    DateOfModification = aClass.DateOfModification
                };
                return classDisplay;
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
                    GameTag = newClass.GameType,
                    ClassName = newClass.ClassName,
                    ClassDescription = newClass.ClassDescription,
                    HitDie = newClass.HitDie,
                    SavingThrows = String.Join("|", newClass.SavingThrows),
                    WeaponProficiencies = String.Join("|", newClass.WeaponProficiencies),
                    ArmorProficiencies = String.Join("|", newClass.ArmorProficiencies),
                    ToolProficiencies = String.Join("|", newClass.ToolProficiencies),
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
                result.GameTag = updates.GameType;
                result.ClassName = updates.ClassName;
                result.ClassDescription = updates.ClassDescription;
                result.HitDie = updates.HitDie;
                result.SavingThrows = String.Join("|", updates.SavingThrows);
                result.WeaponProficiencies = String.Join("|", updates.WeaponProficiencies);
                result.ArmorProficiencies = String.Join("|", updates.ArmorProficiencies);
                result.ToolProficiencies = String.Join("|", updates.ToolProficiencies);
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
        //delete class, changes to is deactivated, so instance chars still have information.
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
