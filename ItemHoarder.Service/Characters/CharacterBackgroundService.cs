using ItemHoarder.Data;
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

            }
        }
        //get classes by room?
        public BackgroundDisplay GetBackgroundsByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //get class by id
        public BackgroundDisplay GetBackgroundsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //get classes in room as player
        public IEnumerable<BackgroundDisplay> GetBackgroundsInRoomAsPlayer(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                bool AmInRoom = Convert.ToBoolean(ctx.RoomUsers.Select(e => e.RoomID == roomId && e.PlayerID == _userId));
            }
        }
        //create class
        public bool CreateBackgrounds(BackgroundCreate newClass)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //update class
        public bool UpdateBackgrounds(int id, BackgroundCreate updates)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //add class to a room
        public bool AddBackgroundsToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //remove class from room
        public bool RemoveBackgroundsToRoom(int classId, int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
        //delete class
        public bool DeleteBackgrounds(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

            }
        }
    }
}
