using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class FeatureService
    {
        private readonly Guid _userId;
        public FeatureService(Guid userId)
        {
            _userId = userId;
        }
        //get all my features
        public IEnumerable<FeatureIndex> GetAllMyFeatures()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterFeatures.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new FeatureIndex
                {
                    FeatureID = e.FeatureID,
                    GameTag = e.GameTag,
                    FeatureName = e.FeatureName,
                    Description = e.Description,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToArray();
            }
        }
        //get feature index by room as GM or player
        public IEnumerable<FeatureIndex> GetAllByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.RoomFeatures.Where(e => e.RoomID == roomId).Select(e => new FeatureIndex
                {
                    FeatureID = e.FeatureID,
                    GameTag = e.Feature.GameTag,
                    FeatureName = e.Feature.FeatureName,
                    Description = e.Feature.Description,
                    DateOfCreation = e.Feature.DateOfCreation,
                    DateOfModification = e.Feature.DateOfModification
                });
            }
        }
        //get feature details by id as GM or player
        public FeatureDetails GetFeatureById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var e = ctx.CharacterFeatures.Single(a => a.FeatureID == id);
                Dictionary<int, string> racePre = new Dictionary<int, string>();
                foreach (var r in e.RaceIdPrerequisites.Split('|').Select(int.Parse).ToList())
                {
                    var race = ctx.CharacterRaces.SingleOrDefault(a => a.RaceID == r);
                    racePre.Add(race.RaceID, race.RaceName);
                }
                Dictionary<int, string> classPre = new Dictionary<int, string>();
                foreach (var r in e.ClassIdPrerequisites.Split('|').Select(int.Parse).ToList())
                {
                    var aClass = ctx.CharacterClasses.SingleOrDefault(a => a.ClassID == r);
                    classPre.Add(aClass.ClassID, aClass.ClassName);
                }
                Dictionary<int, string> featPre = new Dictionary<int, string>();
                foreach (var r in e.FeatureIdPrerequisites.Split('|').Select(int.Parse).ToList())
                {
                    var feat = ctx.CharacterFeatures.SingleOrDefault(a => a.FeatureID == r);
                    featPre.Add(feat.FeatureID, feat.FeatureName);
                }
                Dictionary<string, int> stats = new Dictionary<string, int>();
                foreach (var s in e.StatPrerequisite.Split('|').ToList())
                {
                    var split = s.Split(' ').ToList();
                    stats.Add(split[0], int.Parse(split[1]));
                }
                var feature = new FeatureDetails
                {
                    FeatureID = e.FeatureID,
                    GameTag = e.GameTag,
                    FeatureName = e.FeatureName,
                    Description = e.Description,
                    RaceIdPrerequisite = racePre,
                    ClassIdPrerequisite = classPre,
                    FeatureIdPrerequisite = featPre,
                    StatPrerequisite = stats,
                    LvlPrerequisite = e.LvlPrerequisite,
                    Strength = e.Strength,
                    Dexterity = e.Dexterity,
                    Constitution = e.Constitution,
                    Intelligence = e.Intelligence,
                    Wisdom = e.Wisdom,
                    Charisma = e.Charisma,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                };
                return feature;
            }
        }
        //create feature
        public bool CreateFeature(FeatureCreate features)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<string> stat = new List<string>();
                foreach (var s in features.StatPrerequisite)
                {
                    stat.Add($"{s.Key} {s.Value}");
                }
                var newFeature = new CharacterFeatures
                {
                    OwnerID = _userId,
                    GameTag = features.GameTag,
                    FeatureName = features.FeatureName,
                    Description = features.Description,
                    RaceIdPrerequisites = String.Join("|", features.RaceIdPrerequisite),
                    ClassIdPrerequisites = String.Join("|", features.ClassIdPrerequisite),
                    FeatureIdPrerequisites = String.Join("|", features.FeatureIdPrerequisite),
                    StatPrerequisite = String.Join("|", stat),
                    LvlPrerequisite = features.LvlPrerequisite,
                    Strength = features.Strength,
                    Dexterity = features.Dexterity,
                    Constitution = features.Constitution,
                    Intelligence = features.Intelligence,
                    Wisdom = features.Wisdom,
                    Charisma = features.Charisma,
                    IsDeactivated = false,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterFeatures.Add(newFeature);
                return ctx.SaveChanges() == 1;
            }
        }
        //update feature
        public bool UpdateFeature(int id, FeatureCreate feature)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldFeat = ctx.CharacterFeatures.SingleOrDefault(e => e.OwnerID == _userId && e.FeatureID == id);
                if (oldFeat != null)
                {
                    List<string> stat = new List<string>();
                    foreach (var s in feature.StatPrerequisite)
                    {
                        stat.Add($"{s.Key} {s.Value}");
                    }
                    oldFeat.GameTag = feature.GameTag;
                    oldFeat.FeatureName = feature.FeatureName;
                    oldFeat.Description = feature.Description;
                    oldFeat.RaceIdPrerequisites = String.Join("|", feature.RaceIdPrerequisite);
                    oldFeat.ClassIdPrerequisites = String.Join("|", feature.ClassIdPrerequisite);
                    oldFeat.FeatureIdPrerequisites = String.Join("|", feature.FeatureIdPrerequisite);
                    oldFeat.StatPrerequisite = String.Join("|", stat);
                    oldFeat.LvlPrerequisite = feature.LvlPrerequisite;
                    oldFeat.Strength = feature.Strength;
                    oldFeat.Dexterity = feature.Dexterity;
                    oldFeat.Constitution = feature.Constitution;
                    oldFeat.Intelligence = feature.Intelligence;
                    oldFeat.Wisdom = feature.Wisdom;
                    oldFeat.Charisma = feature.Charisma;
                    oldFeat.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //delete feature
        public bool DeleteFeature(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomFeatures.Where(e => e.OwnerID == _userId && e.FeatureID == id).ToList();
                foreach (var f in rooms)
                {
                    ctx.RoomFeatures.Remove(f);
                }
                var feature = ctx.CharacterFeatures.Single(e => e.OwnerID == _userId && e.FeatureID == id);
                feature.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + rooms.Count();
            }
        }
    }
}
