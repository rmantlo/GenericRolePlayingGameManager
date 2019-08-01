using ItemHoarder.Models.Rooms;
using ItemHoarder.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers
{
    [Authorize]
    public class RoomController : ApiController
    {
        //get Rooms as GM
        public IHttpActionResult GetAllGMRooms()
        {
            var service = CreateRoomService();
            var rooms = service.GetGMRooms();
            return Ok(rooms);
        }
        //get Rooms as Player
        public IHttpActionResult GetAllPlayerRooms()
        {
            var service = CreateRoomService();
            var rooms = service.GetPlayerRooms();
            return Ok(rooms);
        }
        //Get room by Id
        //Create room as GM
        public IHttpActionResult Post(RoomGMCreate newRoomData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateRoomService();

            if (!service.CreateGMRoom(newRoomData)) return BadRequest("Room not created");
            else return Ok();
        }
        //Add Players to room
        //update Room info as GM
        //update RoomNotes as GM
        //Remove Player from room as GM
        //Remove self from room as Player
        //Delete Room as GM

        private RoomService CreateRoomService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new RoomService(userId);
        }
    }
}
