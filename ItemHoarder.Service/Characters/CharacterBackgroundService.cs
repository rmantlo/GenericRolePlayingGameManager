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
    public class CharacterBackgroundService
    {
        private readonly Guid _userId;
        public CharacterBackgroundService(Guid userId)
        {
            _userId = userId;
        }
        //get all my classes
        public IEnumerable<BackgroundDisplay> GetAllMyBackgrounds()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.CharacterBackgrounds.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new BackgroundDisplay
                {
                    BackgroundID = e.BackgroundID,
                    BackgroundName = e.BackgroundName,
                    BackgroundDescription = e.BackgroundDescription,
                    Proficiencies = e.Proficiencies,
                    Strength = e.Strength,
                    Dexterity = e.Dexterity,
                    Constitution = e.Constitution,
                    Intelligence = e.Intelligence,
                    Wisdom = e.Wisdom,
                    Charisma = e.Charisma
                });
                return results;
            }
        }
        //get classes by room?
        public IEnumerable<BackgroundDisplay> GetBackgroundsByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomBackgrounds.Where(e => e.OwnerID == _userId && e.RoomID == roomId).ToList();
                List<BackgroundDisplay> roomBackgrounds = new List<BackgroundDisplay>();
                foreach (var b in rooms)
                {
                    var backgrounds = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == b.BackgroundID);
                    var backgroundDisplay = new BackgroundDisplay
                    {
                        BackgroundID = backgrounds.BackgroundID,
                        BackgroundName = backgrounds.BackgroundName,
                        BackgroundDescription = backgrounds.BackgroundDescription,
                        Proficiencies = backgrounds.Proficiencies,
                        Strength = backgrounds.Strength,
                        Dexterity = backgrounds.Dexterity,
                        Constitution = backgrounds.Constitution,
                        Intelligence = backgrounds.Intelligence,
                        Wisdom = backgrounds.Wisdom,
                        Charisma = backgrounds.Charisma
                    };
                    roomBackgrounds.Add(backgroundDisplay);
                }
                return roomBackgrounds;
            }
        }
        //get class by id
        public BackgroundDisplay GetBackgroundsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var background = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == id);
                var backgroundDisplay = new BackgroundDisplay
                {
                    BackgroundID = background.BackgroundID,
                    BackgroundName = background.BackgroundName,
                    BackgroundDescription = background.BackgroundDescription,
                    Proficiencies = background.Proficiencies,
                    Strength = background.Strength,
                    Dexterity = background.Dexterity,
                    Constitution = background.Constitution,
                    Intelligence = background.Intelligence,
                    Wisdom = background.Wisdom,
                    Charisma = background.Charisma
                };
                return backgroundDisplay;
            }
        }
        //get classes in room as player
        public IEnumerable<BackgroundDisplay> GetBackgroundsInRoomAsPlayer(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId));
                List<BackgroundDisplay> roomBackgrounds = new List<BackgroundDisplay>();
                if (AmInRoom)
                {
                    var rooms = ctx.RoomBackgrounds.Where(e => e.RoomID == roomId).ToList();
                    foreach (var r in rooms)
                    {
                        var background = ctx.CharacterBackgrounds.Single(e => e.OwnerID == r.OwnerID && e.BackgroundID == r.BackgroundID);
                        var backgroundDisplay = new BackgroundDisplay
                        {
                            BackgroundID = background.BackgroundID,
                            BackgroundName = background.BackgroundName,
                            BackgroundDescription = background.BackgroundDescription,
                            Proficiencies = background.Proficiencies,
                            Strength = background.Strength,
                            Dexterity = background.Dexterity,
                            Constitution = background.Constitution,
                            Intelligence = background.Intelligence,
                            Wisdom = background.Wisdom,
                            Charisma = background.Charisma
                        };
                        roomBackgrounds.Add(backgroundDisplay);
                    }
                    return roomBackgrounds;
                }
                else return null;
            }
        }
        //create class
        public bool CreateBackgrounds(BackgroundCreate newBackground)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var background = new CharacterBackground
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    BackgroundName = newBackground.BackgroundName,
                    BackgroundDescription = newBackground.BackgroundDescription,
                    Proficiencies = newBackground.Proficiencies,
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
        //update class
        public bool UpdateBackgrounds(int id, BackgroundCreate updates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldBack = ctx.CharacterBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == id);
                oldBack.BackgroundName = updates.BackgroundName;
                oldBack.BackgroundDescription = updates.BackgroundDescription;
                oldBack.Proficiencies = updates.Proficiencies;
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
        //add class to a room
        public bool AddBackgroundsToRoom(int backgroundId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomBack = new RoomBackgrounds
                {
                    OwnerID = _userId,
                    RoomID = roomId,
                    BackgroundID = backgroundId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomBackgrounds.Add(roomBack);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove class from room
        public bool RemoveBackgroundsToRoom(int backgroundId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomBack = ctx.RoomBackgrounds.Single(e => e.OwnerID == _userId && e.BackgroundID == backgroundId && e.RoomID == roomId);
                ctx.RoomBackgrounds.Remove(roomBack);
                return ctx.SaveChanges() == 1;
            }
        }
        //delete class
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
