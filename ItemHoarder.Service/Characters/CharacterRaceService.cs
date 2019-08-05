using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class CharacterRaceService
    {
        private readonly Guid _userId;
        public CharacterRaceService(Guid userId)
        {
            _userId = userId;
        }
        //get all my races
        public IEnumerable<RaceDisplay> GetAllMyRaces()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.CharacterRaces.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new RaceDisplay
                {
                    RaceID = e.RaceID,
                    Name = e.Name,
                    Speed = e.Speed,
                    Size = e.Size,
                    Languages = e.Languages,
                    Trait = e.Trait,
                    TraitDescription = e.TraitDescription,
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
        //get race by id
        public RaceDisplay GetMyRaceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var race = ctx.CharacterRaces.Single(e => e.OwnerID == _userId && e.RaceID == id);
                var raceDisplay = new RaceDisplay
                {
                    RaceID = race.RaceID,
                    Name = race.Name,
                    Speed = race.Speed,
                    Size = race.Size,
                    Languages = race.Languages,
                    Trait = race.Trait,
                    TraitDescription = race.TraitDescription,
                    Strength = race.Strength,
                    Dexterity = race.Dexterity,
                    Constitution = race.Constitution,
                    Intelligence = race.Intelligence,
                    Wisdom = race.Wisdom,
                    Charisma = race.Charisma
                };
                return raceDisplay;
            }
        }
        //get races by room?
        public IEnumerable<RaceDisplay> GetMyRacesByRoomId(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomRaces.Where(e => e.OwnerID == _userId && e.RoomID == roomId).ToList();
                List<RaceDisplay> roomRaces = new List<RaceDisplay>();
                foreach (var r in rooms)
                {
                    var race = ctx.CharacterRaces.Single(e => e.OwnerID == _userId && e.RaceID == r.RaceID);
                    var raceDisplay = new RaceDisplay
                    {
                        RaceID = race.RaceID,
                        Name = race.Name,
                        Speed = race.Speed,
                        Size = race.Size,
                        Languages = race.Languages,
                        Trait = race.Trait,
                        TraitDescription = race.TraitDescription,
                        Strength = race.Strength,
                        Dexterity = race.Dexterity,
                        Constitution = race.Constitution,
                        Intelligence = race.Intelligence,
                        Wisdom = race.Wisdom,
                        Charisma = race.Charisma
                    };
                    roomRaces.Add(raceDisplay);
                }

                return roomRaces;
            }
        }
        //get room races as player
        public IEnumerable<RaceDisplay> GetRacesOfRoomImIn(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //check roomId againsts roomUsers to make sure Im actually in the room
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId).ToString());
                List<RaceDisplay> roomRaces = new List<RaceDisplay>();
                if (AmInRoom)
                {
                    var rooms = ctx.RoomRaces.Where(e => e.RoomID == roomId).ToList();
                    foreach (var r in rooms)
                    {
                        var race = ctx.CharacterRaces.Single(e => e.OwnerID == r.OwnerID && e.RaceID == r.RaceID);
                        var raceDisplay = new RaceDisplay
                        {
                            RaceID = race.RaceID,
                            Name = race.Name,
                            Speed = race.Speed,
                            Size = race.Size,
                            Languages = race.Languages,
                            Trait = race.Trait,
                            TraitDescription = race.TraitDescription,
                            Strength = race.Strength,
                            Dexterity = race.Dexterity,
                            Constitution = race.Constitution,
                            Intelligence = race.Intelligence,
                            Wisdom = race.Wisdom,
                            Charisma = race.Charisma
                        };
                        roomRaces.Add(raceDisplay);
                    }
                    return roomRaces;
                }
                else return null;
            }
        }
        //create race
        public bool CreateRace(RaceCreate race)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newRace = new CharacterRace
                {
                    Name = race.Name,
                    Speed = race.Speed,
                    Size = race.Size,
                    Languages = race.Languages,
                    Trait = race.Trait,
                    TraitDescription = race.TraitDescription,
                    Strength = race.Strength,
                    Dexterity = race.Dexterity,
                    Constitution = race.Constitution,
                    Intelligence = race.Intelligence,
                    Wisdom = race.Wisdom,
                    Charisma = race.Charisma,
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterRaces.Add(newRace);
                return ctx.SaveChanges() == 1;
            }
        }
        //update race
        public bool UpdateRace(int id, RaceCreate race)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //didnt put security around this update because lazy
                var result = ctx.CharacterRaces.Single(e => e.OwnerID == _userId && e.RaceID == id);
                result.Name = race.Name;
                result.Speed = race.Speed;
                result.Size = race.Size;
                result.Languages = race.Languages;
                result.Trait = race.Trait;
                result.TraitDescription = race.TraitDescription;
                result.Strength = race.Strength;
                result.Dexterity = race.Dexterity;
                result.Constitution = race.Constitution;
                result.Intelligence = race.Intelligence;
                result.Wisdom = race.Wisdom;
                result.Charisma = race.Charisma;
                return ctx.SaveChanges() == 1;
            }
        }
        //add race to a room
        public bool AddRaceToRoom(int raceId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = new RoomRaces
                {
                    OwnerID = _userId,
                    RaceID = raceId,
                    RoomID = roomId,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomRaces.Add(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //remove race from room
        public bool RemoveRaceFromRoom(int id, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.RoomRaces.Single(e => e.OwnerID == _userId && e.RoomID == roomId && e.RaceID == id);
                ctx.RoomRaces.Remove(result);
                return ctx.SaveChanges() == 1;
            }
        }
        //delete race: just switch to deactivated = true
        public bool DeleteRace(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userId && e.RaceID == id).ToList();
                foreach (var r in roomRaces)
                {
                    ctx.RoomRaces.Remove(r);
                }
                var race = ctx.CharacterRaces.Single(e => e.OwnerID == _userId && e.RaceID == id);
                race.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + roomRaces.Count();
            }
        }
    }
}
