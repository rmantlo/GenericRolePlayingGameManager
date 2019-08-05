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
    [RoutePrefix("api")]
    public class RoomController : ApiController
    {
        //get Rooms as GM
        [HttpGet]
        [Route("rooms/owned")]
        public IHttpActionResult GetAllGMRooms()
        {
            var service = CreateRoomService();
            var rooms = service.GetGMRooms();
            return Ok(rooms);
        }
        //get Rooms as Player
        [HttpGet]
        [Route("rooms/player")]
        public IHttpActionResult GetAllPlayerRooms()
        {
            var service = CreateRoomService();
            var rooms = service.GetPlayerRooms();
            return Ok(rooms);
        }
        //Get room by Id as GM
        [HttpGet]
        [Route("room/owned/{roomId}")]
        public IHttpActionResult GetGMRoom(int roomId)
        {
            var service = CreateRoomService();
            var result = service.GetGMRoomById(roomId);
            if (result != null)
            {
                return Ok(result);
            }
            else return Ok("No room found");
        }
        //Get Room by ID as Player
        [HttpGet]
        [Route("room/player/{roomId}")]
        public IHttpActionResult GetPlayerRoom(int roomId)
        {
            var service = CreateRoomService();
            var result = service.GetPlayerRoomById(roomId);
            if (result != null)
            {
                return Ok(result);
            }
            else return Ok("No room found");
        }
        //Create room as GM
        [HttpPost]
        [Route("room/create")]
        public IHttpActionResult Post(RoomGMCreate newRoomData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var service = CreateRoomService();
            if (!service.CreateGMRoom(newRoomData)) return BadRequest("Room not created");
            else return Ok();
        }
        //Add Players to room
        [HttpPut]
        [Route("room/addPlayer/{roomId}")]
        public IHttpActionResult PutPlayerInRoom(int roomId, string Username)
        {
            if (Username == null) return BadRequest("Player Username not recieved");
            var service = CreateRoomService();
            string response = service.AddPlayerToRoom(roomId, Username);
            if (response == "Player added to room") return Ok(response);
            else return BadRequest(response);
        }
        //update Room info as GM
        [HttpPut]
        [Route("room/updateRoom/{roomId}")]
        public IHttpActionResult PutGMRoomUpdate(int roomId, RoomGMUpdateSettings updates)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateRoomService();
            if (!service.UpdateGMRoom(roomId, updates)) return BadRequest("Room not updated");
            else return Ok();
        }
        //update RoomNotes as GM
        [HttpPut]
        [Route("room/updateNotes/{roomId}")]
        public IHttpActionResult PutGMRoomNotes(int roomId, RoomGMUpdateNotes updates)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateRoomService();
            if (!service.UpdateRoomNotes(roomId, updates)) return BadRequest("Room notes not updated");
            else return Ok();
        }
        //Remove Player from room as GM
        [HttpDelete]
        [Route("room/removePlayer/{roomId}")]
        public IHttpActionResult DeletePlayerFromMyRoom(int roomId, string playerUsername)
        {
            if (playerUsername == null) return BadRequest("username not received");
            var service = CreateRoomService();
            if (!service.RemovePlayerFromGMRoom(roomId, playerUsername)) return BadRequest("Player not removed");
            else return Ok();
        }
        //Remove self from room as Player
        [HttpDelete]
        [Route("room/removeSelf/{roomId}")]
        public IHttpActionResult DeleteSelfFromRoom(int roomId)
        {
            var service = CreateRoomService();
            if (!service.RemoveSelfAsPlayerFromRoom(roomId)) return BadRequest("Not removed from room");
            else return Ok();
        }
        //Delete Room as GM
        [HttpDelete]
        [Route("room/delete/{roomId}")]
        public IHttpActionResult DeleteMyRoom(int roomId)
        {
            var service = CreateRoomService();
            if (!service.DeleteGMRoom(roomId)) return BadRequest("Room not deleted");
            else return Ok();
        }
        private RoomService CreateRoomService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new RoomService(userId);
        }
    }
}
