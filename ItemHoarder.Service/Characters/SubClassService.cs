using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Models.Characters.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class SubClassService
    {
        private readonly Guid _userId;
        public SubClassService(Guid userId)
        {
            _userId = userId;
        }
        //get all subclasses as GM
        public IEnumerable<SubClassIndex> GetAllMySubClasses()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.SubClasses.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new SubClassIndex
                {
                    SubClassID = e.SubClassID,
                    GameType = e.GameType,
                    SubClassName = e.SubClassName,
                    SubClassDesc = e.Description,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToList();
            }
        }
        //get all subclasses belonging to a class as gm and player
        public IEnumerable<SubClassIndex> GetAllByClass(int classId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var aClass = ctx.CharacterClasses.SingleOrDefault(e => e.ClassID == classId);
                return aClass.SubClasses.Select(e => new SubClassIndex
                {
                    SubClassID = e.SubClassID,
                    GameType = e.SubClass.GameType,
                    SubClassName = e.SubClass.SubClassName,
                    SubClassDesc = e.SubClass.Description,
                    DateOfCreation = e.SubClass.DateOfCreation,
                    DateOfModification = e.SubClass.DateOfModification
                }).ToList();
            }
        }
        //get subclass details by id as gm and player
        public SubClassDetails GetSubClassById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var subClass = ctx.SubClasses.SingleOrDefault(e => e.SubClassID == id);
                Dictionary<int, string> parentClasses = new Dictionary<int, string>();
                Dictionary<int, string> features = new Dictionary<int, string>();
                Dictionary<int, string> appliedSpells = new Dictionary<int, string>();
                Dictionary<int, string> otherSpells = new Dictionary<int, string>();
                foreach (var c in subClass.Classes)
                {
                    parentClasses.Add(c.ClassID, c.Class.ClassName);
                }
                foreach (var f in subClass.Features)
                {
                    features.Add(f.FeatureID, f.Feature.FeatureName);
                }
                foreach (var s in subClass.AppliedSpellIDs.Split('|').ToList())
                {
                    var spell = ctx.Spells.SingleOrDefault(e => e.AttackID == int.Parse(s));
                    if (spell != null)
                    {
                        appliedSpells.Add(spell.AttackID, spell.Name);
                    }
                }
                foreach (var s in subClass.ListOfSpellIDs.Split('|').ToList())
                {
                    var spell = ctx.Spells.SingleOrDefault(e => e.AttackID == int.Parse(s));
                    if (spell != null)
                    {
                        otherSpells.Add(spell.AttackID, spell.Name);
                    }
                }
                return new SubClassDetails
                {
                    SubClassID = subClass.SubClassID,
                    GameType = subClass.GameType,
                    SubClassName = subClass.SubClassName,
                    SubClassDesc = subClass.Description,
                    ParentClasses = parentClasses,
                    FeatureIDs = features,
                    AppliedSpells = appliedSpells,
                    SpellIDs = otherSpells,
                    WeaponProficiencies = subClass.WeaponProficiencies.Split('|').ToList(),
                    ArmorProficiencies = subClass.ArmorProficiencies.Split('|').ToList(),
                    ToolProficiencies = subClass.ToolProficiencies.Split('|').ToList(),
                    DateOfCreation = subClass.DateOfCreation,
                    DateOfModification = subClass.DateOfModification
                };
            }
        }
        //create new subclass (on existing class)
        public bool CreateSubClass(SubClassCreate subClass)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var newClass = new CharacterSubClass
                {
                    OwnerID = _userId,
                    GameType = subClass.GameType,
                    SubClassName = subClass.SubClassName,
                    Description = subClass.SubClassDesc,
                    AppliedSpellIDs = String.Join("|", subClass.AppliedSpells),
                    ListOfSpellIDs = String.Join("|", subClass.SpellIDs),
                    WeaponProficiencies = String.Join("|", subClass.WeaponProficiencies),
                    ArmorProficiencies = String.Join("|", subClass.ArmorProficiencies),
                    ToolProficiencies = String.Join("|", subClass.ToolProficiencies),
                    IsDeactivated = false,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.SubClasses.Add(newClass);
                if (ctx.SaveChanges() == 1)
                {
                    if (subClass.ParentClassIDs == null && subClass.FeatureIDs == null)
                    {
                        return true;
                    }
                    if (subClass.FeatureIDs != null)
                    {
                        List<CharacterFeatList> features = new List<CharacterFeatList>();
                        foreach (var f in subClass.FeatureIDs)
                        {
                            features.Add(new CharacterFeatList
                            {
                                DateOfCreation = DateTimeOffset.UtcNow,
                                SubClassID = newClass.SubClassID,
                                FeatureID = f
                            });
                        }
                        newClass.Features = features;
                    }
                    if (subClass.ParentClassIDs != null)
                    {
                        List<CharClassSubConnection> classes = new List<CharClassSubConnection>();
                        foreach (var p in subClass.ParentClassIDs)
                        {
                            classes.Add(new CharClassSubConnection
                            {
                                OwnerID = _userId,
                                ClassID = p,
                                SubClassID = newClass.SubClassID,
                                DateOfCreation = DateTimeOffset.UtcNow
                            });
                        }
                        newClass.Classes = classes;
                    }
                    if (subClass.ParentClassIDs == null || subClass.FeatureIDs == null)
                    {
                        return ctx.SaveChanges() == 1;
                    }
                    return ctx.SaveChanges() == 2;
                }
                else return false;
            }
        }
        //update existing subclass
        public bool UpdateSubClass(int id, SubClassEdit subClass)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var sub = ctx.SubClasses.SingleOrDefault(e => e.OwnerID == _userId && e.SubClassID == id);
                sub.GameType = subClass.GameType;
                sub.SubClassName = subClass.SubClassName;
                sub.Description = subClass.SubClassDesc;
                sub.AppliedSpellIDs = String.Join("|", subClass.AppliedSpells);
                sub.ListOfSpellIDs = String.Join("|", subClass.SpellIDs);
                sub.WeaponProficiencies = String.Join("|", subClass.WeaponProficiencies);
                sub.ArmorProficiencies = String.Join("|", subClass.ArmorProficiencies);
                sub.ToolProficiencies = String.Join("|", subClass.ToolProficiencies);
                sub.DateOfModification = DateTimeOffset.UtcNow;
                ctx.SaveChanges();
                int count = 0;
                foreach (var c in subClass.ParentClassIDsToAdd)
                {
                    if (sub.Classes.SingleOrDefault(e => e.ClassID == c) == null)
                    {
                        sub.Classes.Add(new CharClassSubConnection
                        {
                            OwnerID = _userId,
                            ClassID = c,
                            SubClassID = id,
                            DateOfCreation = DateTimeOffset.UtcNow
                        });
                        count++;
                    }
                }
                foreach (var c in subClass.FeatureIDsToAdd)
                {
                    if (sub.Features.SingleOrDefault(e => e.FeatureID == c) == null)
                    {
                        sub.Features.Add(new CharacterFeatList
                        {
                            FeatureID = c,
                            SubClassID = id,
                            DateOfCreation = DateTimeOffset.UtcNow
                        });
                        count++;
                    }
                }
                return ctx.SaveChanges() == count; //check if correct
            }
        }
        //delete subclass
        public bool DeleteSubClass(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var sub = ctx.SubClasses.Single(e => e.OwnerID == _userId && e.SubClassID == id);
                foreach (var c in sub.Classes)
                {
                    sub.Classes.Remove(c);
                }
                foreach (var f in sub.Features)
                {
                    sub.Features.Remove(f);
                }
                sub.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + sub.Classes.Count + sub.Features.Count;
            }
        }
    }
}
