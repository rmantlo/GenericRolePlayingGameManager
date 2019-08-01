using ItemHoarder.Data;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class RoomService
    {
        private readonly Guid _userID;
        public RoomService(Guid userId)
        {
            _userID = userId;
        }
        //get my rooms as GM
        public IEnumerable<RoomGMDisplay> GetGMRooms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID);
                List<RoomGMDisplay> displayRooms = new List<RoomGMDisplay>();
                foreach (var i in room)
                {
                    var roomNotes = ctx.RoomNotes.Single(e => e.OwnerID == _userID && e.RoomID == i.RoomID);
                    var roomPlayers = ctx.RoomUsers.Where(e => e.RoomID == i.RoomID);
                    List<string> usernames = new List<string>();
                    foreach (var n in roomPlayers)
                    {
                        usernames.Add(n.PlayerUsername);
                    }

                    var display = new RoomGMDisplay
                    {
                        RoomID = i.RoomID,
                        RoomName = i.RoomName,
                        GameType = i.GameType,
                        PlayerOneNotes = roomNotes.PlayerOneNotes,
                        PlayerTwoNotes = roomNotes.PlayerTwoNotes,
                        PlayerThreeNotes = roomNotes.PlayerThreeNotes,
                        PlayerFourNotes = roomNotes.PlayerFourNotes,
                        PlayerFiveNotes = roomNotes.PlayerFiveNotes,
                        PlayerSixNotes = roomNotes.PlayerSixNotes,
                        PlayerSevenNotes = roomNotes.PlayerSevenNotes,
                        GeneralNotes = roomNotes.GeneralNotes,
                        PlayerUsernames = usernames,
                        DateOfCreation = i.DateOfCreation
                    };
                    displayRooms.Add(display);
                }
                return displayRooms;
            }
        }
        //get my rooms as Player
        public IEnumerable<RoomPlayerDisplay> GetPlayerRooms()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<RoomPlayerDisplay> displayRooms = new List<RoomPlayerDisplay>();
                var roomUsers = ctx.RoomUsers.Where(e => e.PlayerID == _userID);
                foreach (var r in roomUsers)
                {
                    var room = ctx.Rooms.Single(e => e.RoomID == r.RoomID);

                    var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString());
                    var roomPlayers = ctx.RoomUsers.Where(e => e.RoomID == room.RoomID);
                    List<string> players = new List<string>();
                    foreach (var u in roomPlayers)
                    {
                        players.Add(u.PlayerUsername);
                    }
                    var display = new RoomPlayerDisplay
                    {
                        RoomID = room.RoomID,
                        RoomCreatorUsername = gmUsername.UserName,
                        RoomName = room.RoomName,
                        GameType = room.GameType,
                        PlayerUsernames = players,
                        DateOfCreation = room.DateOfCreation
                    };
                    displayRooms.Add(display);
                }
                return displayRooms;
            }
        }
        //get GM room by Id
        public RoomGMDisplay GetGMRoomById(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                var roomNotes = ctx.RoomNotes.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                var players = ctx.RoomUsers.Where(e => e.RoomID == roomId);
                List<string> usernames = new List<string>();
                foreach (var n in players)
                {
                    usernames.Add(n.PlayerUsername);
                }

                var display = new RoomGMDisplay
                {
                    RoomID = room.RoomID,
                    RoomName = room.RoomName,
                    GameType = room.GameType,
                    PlayerOneNotes = roomNotes.PlayerOneNotes,
                    PlayerTwoNotes = roomNotes.PlayerTwoNotes,
                    PlayerThreeNotes = roomNotes.PlayerThreeNotes,
                    PlayerFourNotes = roomNotes.PlayerFourNotes,
                    PlayerFiveNotes = roomNotes.PlayerFiveNotes,
                    PlayerSixNotes = roomNotes.PlayerSixNotes,
                    PlayerSevenNotes = roomNotes.PlayerSevenNotes,
                    GeneralNotes = roomNotes.GeneralNotes,
                    PlayerUsernames = usernames,
                    DateOfCreation = room.DateOfCreation
                };
                return display;
            }
        }
        //get player room by Id
        public RoomPlayerDisplay GetPlayerRoomById(int roomId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Single(e => e.RoomID == roomId);
                var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString());
                var roomPlayers = ctx.RoomUsers.Where(e => e.RoomID == room.RoomID);
                List<string> players = new List<string>();
                foreach (var u in roomPlayers)
                {
                    players.Add(u.PlayerUsername);
                }
                var display = new RoomPlayerDisplay
                {
                    RoomID = room.RoomID,
                    RoomCreatorUsername = gmUsername.UserName,
                    RoomName = room.RoomName,
                    GameType = room.GameType,
                    PlayerUsernames = players,
                    DateOfCreation = room.DateOfCreation
                };
                return display;
            }
        }
        //update as GM Room settings
        public bool UpdateGMRoom(RoomGMUpdateSettings gmRoomUpdates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID).SingleOrDefault(e => e.RoomID == gmRoomUpdates.RoomID);
                room.RoomName = gmRoomUpdates.RoomName;
                room.GameType = gmRoomUpdates.GameType;
                room.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //update as GM room notes
        public bool UpdateRoomNotes(RoomGMUpdateNotes gmRoomUpdates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomNotes = ctx.RoomNotes.Single(e => e.OwnerID == _userID && e.RoomID == gmRoomUpdates.RoomID);
                roomNotes.PlayerOneNotes = gmRoomUpdates.PlayerOneNotes;
                roomNotes.PlayerTwoNotes = gmRoomUpdates.PlayerTwoNotes;
                roomNotes.PlayerThreeNotes = gmRoomUpdates.PlayerThreeNotes;
                roomNotes.PlayerFourNotes = gmRoomUpdates.PlayerFourNotes;
                roomNotes.PlayerFiveNotes = gmRoomUpdates.PlayerFiveNotes;
                roomNotes.PlayerSixNotes = gmRoomUpdates.PlayerSixNotes;
                roomNotes.PlayerSevenNotes = gmRoomUpdates.PlayerSevenNotes;
                roomNotes.GeneralNotes = gmRoomUpdates.GeneralNotes;
                roomNotes.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //Create Room (and notes) as GM
        public bool CreateGMRoom(RoomGMCreate newRoomCreate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newRoom = new Room
                {
                    OwnerID = _userID,
                    RoomName = newRoomCreate.RoomName,
                    GameType = newRoomCreate.GameType,
                    DateOfCreation = DateTimeOffset.UtcNow,
                };
                ctx.Rooms.Add(newRoom);
                var newNotes = new RoomNotes
                {
                    OwnerID = _userID,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomNotes.Add(newNotes);
                return ctx.SaveChanges() == 2;
            }
        }
        //Add(Create) Player to room as GM
        public bool AddPlayerToRoom(int roomId, string playerUsername)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var username = ctx.Users.Single(e => e.UserName == playerUsername);
                var playerListOfRoom = ctx.RoomUsers.Where(e => e.RoomID == roomId);
                var isPlayerAlreadyInRoom = ctx.RoomUsers.SingleOrDefault(e => e.RoomID == roomId && e.PlayerID == _userID);
                if (playerListOfRoom.Count() < 7 && isPlayerAlreadyInRoom == null)
                {
                    var newRoomUser = new RoomUsers
                    {
                        RoomID = roomId,
                        PlayerID = Guid.Parse(username.Id),
                        PlayerUsername = username.UserName,
                        DateOfCreation = DateTimeOffset.UtcNow
                    };
                    ctx.RoomUsers.Add(newRoomUser);
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //delete my room as GM
        public bool DeleteGMRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                ctx.Rooms.Remove(room);

                var roomNotes = ctx.RoomNotes.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                ctx.RoomNotes.Remove(roomNotes);

                var roomUsers = ctx.RoomUsers.Where(e => e.RoomID == roomId);
                foreach (var r in roomUsers) { ctx.RoomUsers.Remove(r); }
                var roomClasses = ctx.RoomClasses.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var c in roomClasses) { ctx.RoomClasses.Remove(c); }
                var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var r in roomRaces) { ctx.RoomRaces.Remove(r); }
                var proSkills = ctx.RoomProficiencies.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var p in proSkills) { ctx.RoomProficiencies.Remove(p); }
                var roomBackgrounds = ctx.RoomBackgrounds.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var b in roomBackgrounds) { ctx.RoomBackgrounds.Remove(b); }
                var roomFeatures = ctx.RoomFeatures.Where(e => e.OwnerID == _userID && e.RoomID == roomId);
                foreach (var f in roomFeatures) { ctx.RoomFeatures.Remove(f); }

                int count = 2 + roomUsers.Count() + roomClasses.Count() + roomRaces.Count() + proSkills.Count() + roomBackgrounds.Count() + roomFeatures.Count();

                return ctx.SaveChanges() == count;
            }
        }
        //delete myself from a room as player
        public bool RemoveSelfAsPlayerFromRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomUsers = ctx.RoomUsers.SingleOrDefault(e => e.RoomID == roomId);
                ctx.RoomUsers.Remove(roomUsers);
                var charInsta = ctx.CharacterInstances.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                charInsta.RoomID = null;
                return ctx.SaveChanges() == 2;
            }
        }
        //Gm remove player from room
        public bool RemovePlayerFromGMRoom(int roomId, string playerUsername)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomUser = ctx.RoomUsers.Single(e => e.RoomID == roomId && e.PlayerUsername == playerUsername);
                ctx.RoomUsers.Remove(roomUser);
                return ctx.SaveChanges() == 1;
            }
        }

    }
}
