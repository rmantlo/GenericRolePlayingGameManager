﻿using ItemHoarder.Data;
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
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID).ToList();
                List<RoomGMDisplay> displayRooms = new List<RoomGMDisplay>();
                foreach (var i in room)
                {
                    var roomNotes = ctx.RoomNotes.Single(e => e.OwnerID == _userID && e.RoomID == i.RoomID);
                    var roomPlayers = ctx.RoomUsers.Where(e => e.RoomID == i.RoomID).ToList();
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
                var roomUsers = ctx.RoomUsers.Where(e => e.PlayerID == _userID).ToList();
                foreach (var r in roomUsers)
                {
                    var room = ctx.Rooms.Single(e => e.RoomID == r.RoomID);

                    var gmUsername = ctx.Users.Single(e => e.Id == room.OwnerID.ToString());
                    var roomPlayers = ctx.RoomUsers.Where(e => e.RoomID == room.RoomID).ToList();
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
            using (var ctx = new ApplicationDbContext())
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
        public bool UpdateGMRoom(int roomId, RoomGMUpdateSettings gmRoomUpdates)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var room = ctx.Rooms.Where(e => e.OwnerID == _userID).SingleOrDefault(e => e.RoomID == roomId);
                if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "" && gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
                {
                    room.RoomName = gmRoomUpdates.RoomName;
                    room.GameType = gmRoomUpdates.GameType;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else if (gmRoomUpdates.GameType != null && gmRoomUpdates.GameType != "")
                {
                    room.GameType = gmRoomUpdates.GameType;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else if (gmRoomUpdates.RoomName != null && gmRoomUpdates.RoomName != "")
                {
                    room.RoomName = gmRoomUpdates.RoomName;
                    room.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //update as GM room notes
        public bool UpdateRoomNotes(int roomId, RoomGMUpdateNotes gmNote)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rNotes = ctx.RoomNotes.Single(e => e.OwnerID == _userID && e.RoomID == roomId);
                if (gmNote.PlayerOneNotes != null && gmNote.PlayerOneNotes != "") { rNotes.PlayerOneNotes = gmNote.PlayerOneNotes; };
                if (gmNote.PlayerTwoNotes != null && gmNote.PlayerTwoNotes != "") { rNotes.PlayerTwoNotes = gmNote.PlayerTwoNotes; };
                if (gmNote.PlayerThreeNotes != null && gmNote.PlayerThreeNotes != "") { rNotes.PlayerThreeNotes = gmNote.PlayerThreeNotes; };
                if (gmNote.PlayerFourNotes != null && gmNote.PlayerFourNotes != "") { rNotes.PlayerFourNotes = gmNote.PlayerFourNotes; };
                if (gmNote.PlayerFiveNotes != null && gmNote.PlayerFiveNotes != "") { rNotes.PlayerFiveNotes = gmNote.PlayerFiveNotes; };
                if (gmNote.PlayerSixNotes != null && gmNote.PlayerSixNotes != "") { rNotes.PlayerSixNotes = gmNote.PlayerSixNotes; };
                if (gmNote.PlayerSevenNotes != null && gmNote.PlayerSevenNotes != "") { rNotes.PlayerSevenNotes = gmNote.PlayerSevenNotes; };

                rNotes.DateOfModification = DateTimeOffset.UtcNow;
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
                bool save = ctx.SaveChanges() == 1;
                var newNotes = new RoomNotes
                {
                    RoomID = newRoom.RoomID,
                    OwnerID = _userID,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.RoomNotes.Add(newNotes);
                return (ctx.SaveChanges() == 1 && save == true);
            }
        }
        //Add(Create) Player to room as GM
        public string AddPlayerToRoom(int roomId, string playerUsername)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var username = ctx.Users.SingleOrDefault(e => e.UserName == playerUsername);
                if (username == null) return "Username not found";
                var playerListOfRoom = ctx.RoomUsers.Where(e => e.RoomID == roomId);
                var isPlayerAlreadyInRoom = ctx.RoomUsers.SingleOrDefault(e => e.RoomID == roomId && e.PlayerID == _userID);
                if (isPlayerAlreadyInRoom == null)
                {
                    if (playerListOfRoom.Count() < 7)
                    {
                        var newRoomUser = new RoomUsers
                        {
                            RoomID = roomId,
                            PlayerID = Guid.Parse(username.Id),
                            PlayerUsername = username.UserName,
                            DateOfCreation = DateTimeOffset.UtcNow
                        };
                        ctx.RoomUsers.Add(newRoomUser);
                        if (ctx.SaveChanges() == 1) return "Player added to room";
                        else return "Player not added to room";
                    }
                    else return "Already 7 players in room";
                }
                else return "Player already in room";
            }
        }
        //delete my room as GM
        public bool DeleteGMRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                int count = 0;
                var room = ctx.Rooms.SingleOrDefault(e => e.OwnerID == _userID && e.RoomID == roomId);
                ctx.Rooms.Remove(room);
                if (ctx.SaveChanges() == 1)
                {
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

                    count = 1 + roomUsers.Count() + roomClasses.Count() + roomRaces.Count() + proSkills.Count() + roomBackgrounds.Count() + roomFeatures.Count();
                    return ctx.SaveChanges() == count;
                }
                else return false;
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
