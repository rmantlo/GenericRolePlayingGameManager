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
    public class CharacterFeatureService
    {
        private readonly Guid _userId;
        public CharacterFeatureService(Guid userId)
        {
            _userId = userId;
        }
        //get all mine
        public IEnumerable<FeatureDisplay> GetAllMyFeatures()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterFeatures.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new FeatureDisplay
                {
                    FeatureID = e.FeatureID,
                    FeatureName = e.FeatureName,
                    Description = e.Description,
                    RaceIdPrerequisite = e.RaceIdPrerequisite,
                    ClassIdPrerequisite = e.ClassIdPrerequisite,
                    StatPrerequisite = e.StatPrerequisite,
                    LvlPrerequisite = e.LvlPrerequisite,
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
        public FeatureDisplay GetFeatureById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var e = ctx.CharacterFeatures.Single(a => a.OwnerID == _userId && a.FeatureID == id);
                var feature = new FeatureDisplay
                {
                    FeatureID = e.FeatureID,
                    FeatureName = e.FeatureName,
                    Description = e.Description,
                    RaceIdPrerequisite = e.RaceIdPrerequisite,
                    ClassIdPrerequisite = e.ClassIdPrerequisite,
                    StatPrerequisite = e.StatPrerequisite,
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
        //get mine by room
        public IEnumerable<FeatureDisplay> GetFeaturesByMyRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomFeatures.Where(e => e.OwnerID == _userId && e.RoomID == roomId).ToList();
                List<FeatureDisplay> roomFeatures = new List<FeatureDisplay>();
                foreach (var b in rooms)
                {
                    var features = ctx.CharacterFeatures.Single(e => e.OwnerID == _userId && e.FeatureID == b.FeatureID);
                    var featureDisplay = new FeatureDisplay
                    {
                        FeatureID = features.FeatureID,
                        FeatureName = features.FeatureName,
                        Description = features.Description,
                        RaceIdPrerequisite = features.RaceIdPrerequisite,
                        ClassIdPrerequisite = features.ClassIdPrerequisite,
                        StatPrerequisite = features.StatPrerequisite,
                        LvlPrerequisite = features.LvlPrerequisite,
                        Strength = features.Strength,
                        Dexterity = features.Dexterity,
                        Constitution = features.Constitution,
                        Intelligence = features.Intelligence,
                        Wisdom = features.Wisdom,
                        Charisma = features.Charisma,
                        DateOfCreation = features.DateOfCreation,
                        DateOfModification = features.DateOfModification
                    };
                    roomFeatures.Add(featureDisplay);
                }
                return roomFeatures;
            }
        }
        //get features by room as player
        public IEnumerable<FeatureDisplay> GetFeaturesByRoomAsPlayer(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId));
                List<FeatureDisplay> roomFeatures = new List<FeatureDisplay>();
                if (AmInRoom)
                {
                    var rooms = ctx.RoomFeatures.Where(e => e.RoomID == roomId).ToList();
                    foreach (var r in rooms)
                    {
                        var feature = ctx.CharacterFeatures.Single(e => e.OwnerID == r.OwnerID && e.FeatureID == r.FeatureID);
                        var featureDisplay = new FeatureDisplay
                        {
                            FeatureID = feature.FeatureID,
                            FeatureName = feature.FeatureName,
                            Description = feature.Description,
                            RaceIdPrerequisite = feature.RaceIdPrerequisite,
                            ClassIdPrerequisite = feature.ClassIdPrerequisite,
                            StatPrerequisite = feature.StatPrerequisite,
                            LvlPrerequisite = feature.LvlPrerequisite,
                            Strength = feature.Strength,
                            Dexterity = feature.Dexterity,
                            Constitution = feature.Constitution,
                            Intelligence = feature.Intelligence,
                            Wisdom = feature.Wisdom,
                            Charisma = feature.Charisma
                        };
                        roomFeatures.Add(featureDisplay);
                    }
                    return roomFeatures;
                }
                else return null;
            }
        }
        //create feature
        public bool CreateFeature(FeatureCreate features)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newFeature = new CharacterFeatures
                {
                    OwnerID = _userId,
                    FeatureName = features.FeatureName,
                    Description = features.Description,
                    RaceIdPrerequisite = features.RaceIdPrerequisite,
                    ClassIdPrerequisite = features.ClassIdPrerequisite,
                    StatPrerequisite = features.StatPrerequisite,
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
                var oldFeat = ctx.CharacterFeatures.Single(e => e.OwnerID == _userId && e.FeatureID == id);
                oldFeat.FeatureName = feature.FeatureName;
                oldFeat.Description = feature.Description;
                oldFeat.RaceIdPrerequisite = feature.RaceIdPrerequisite;
                oldFeat.ClassIdPrerequisite = feature.ClassIdPrerequisite;
                oldFeat.StatPrerequisite = feature.StatPrerequisite;
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
                ctx.CharacterFeatures.Remove(feature);
                return ctx.SaveChanges() == 1 + rooms.Count();
            }
        }
    }
}
