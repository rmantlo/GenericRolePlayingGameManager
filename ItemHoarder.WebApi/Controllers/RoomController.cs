using ItemHoarder.Models.Characters.Instanced;
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
    //Player room endpoints
    [Authorize]
    [RoutePrefix("api")]
    public class RoomController : ApiController
    {
        ////get Rooms as GM
        //[HttpGet]
        //[Route("rooms/owned")]
        //public IHttpActionResult GetAllGMRooms()
        //{
        //    var service = CreateRoomService();
        //    var rooms = service.GetOwnedRooms();
        //    return Ok(rooms);
        //}
        ////get Rooms as Player
        //[HttpGet]
        //[Route("rooms/player")]
        //public IHttpActionResult GetAllPlayerRooms()
        //{
        //    var service = CreateRoomService();
        //    var rooms = service.GetPlayerRooms();
        //    return Ok(rooms);
        //}
        ////Get Room by ID as Player
        //[HttpGet]
        //[Route("room/details/{roomId}")]
        //public IHttpActionResult GetRoom(int roomId)
        //{
        //    var service = CreateRoomService();
        //    var result = service.GetRoomById(roomId);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else return Ok("No room found");
        //}
        ////Create room as GM
        //[HttpPost]
        //[Route("room/create")]
        //public IHttpActionResult Post(RoomGMCreate newRoomData)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var service = CreateRoomService();
        //    if (!service.CreateRoom(newRoomData)) return BadRequest("Room not created");
        //    else return Ok();
        //}
        ////update Room info as GM
        //[HttpPut]
        //[Route("room/updateRoom/{roomId}")]
        //public IHttpActionResult PutGMRoomUpdate(int roomId, RoomGMUpdateSettings updates)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var service = CreateRoomService();
        //    if (!service.UpdateRoom(roomId, updates)) return BadRequest("Room not updated");
        //    else return Ok();
        //}
        ////update RoomNotes as GM
        //[HttpPut]
        //[Route("room/updateNotes/{roomId}")]
        //public IHttpActionResult PutGMRoomNotes(int roomId, RoomGMUpdateNotes updates)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var service = CreateRoomService();
        //    if (!service.UpdateRoomNotes(roomId, updates)) return BadRequest("Room notes not updated");
        //    else return Ok();
        //}

        //[HttpPut]
        //[Route("room/addPlayer/{roomId}")]
        //public IHttpActionResult PutPlayerInRoom(int roomId, string Username)
        //{
        //    if (Username == null) return BadRequest("Player Username not recieved");
        //    var service = CreateRoomService();
        //    string response = service.AddPlayerToRoom(roomId, Username);
        //    if (response == "Player added to room") return Ok(response);
        //    else return BadRequest(response);
        //}
        ////Remove Player from room as GM
        //[HttpDelete]
        //[Route("room/removePlayer/{roomId}")]
        //public IHttpActionResult DeletePlayerFromMyRoom(int roomId, string playerUsername)
        //{
        //    if (playerUsername == null) return BadRequest("username not received");
        //    var service = CreateRoomService();
        //    if (!service.RemovePlayerFromGMRoom(roomId, playerUsername)) return BadRequest("Player not removed");
        //    else return Ok();
        //}
        ////Remove self from room as Player
        //[HttpDelete]
        //[Route("room/removeSelf/{roomId}")]
        //public IHttpActionResult DeleteSelfFromRoom(int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (!service.RemoveSelfAsPlayerFromRoom(roomId)) return BadRequest("Not removed from room");
        //    else return Ok();
        //}
        ////Delete Room as GM
        //[HttpDelete]
        //[Route("room/delete/{roomId}")]
        //public IHttpActionResult DeleteMyRoom(int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (!service.DeleteGMRoom(roomId)) return BadRequest("Room not deleted");
        //    else return Ok();
        //}

        ///// <summary>
        ///// Create new instanced character in room
        ///// </summary>
        ///// <param name="newCharacter"></param>
        ///// <returns></returns>
        //[Route("room/player/create")]
        //public IHttpActionResult PostCharacter(InstanceCreate newCharacter)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var service = CreateRoomService();
        //    if (service.CreateInstancedCharacter(newCharacter) == "Character created") return Ok();
        //    else if (service.CreateInstancedCharacter(newCharacter) == "Character instance already in room") return BadRequest("Character instance already in room");
        //    else if (service.CreateInstancedCharacter(newCharacter) == "Character not created, something went wrong") return BadRequest("Character not created, something went wrong");
        //    else return BadRequest("Instance of character not created");
        //}

        ///// <summary>
        ///// Add existing class to a room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/class")]
        //public IHttpActionResult PostClass(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.AddClassToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Class not added to room");
        //}
        ///// <summary>
        ///// Remove existing class from a room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/class/delete")]
        //public IHttpActionResult DeleteClass(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.RemoveClassToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Class not removed from room");
        //}
        ///// <summary>
        ///// Add existing race to a room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/race")]
        //public IHttpActionResult PostRace(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.AddRaceToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Race not added to room");
        //}
        ///// <summary>
        ///// Remove existing race from a room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/race/delete")]
        //public IHttpActionResult DeleteRace(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.RemoveRaceFromRoom(id, roomId)) return Ok();
        //    else return BadRequest("Race not removed from room");
        //}
        ///// <summary>
        ///// Add background to owned room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/background")]
        //public IHttpActionResult PostBackground(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.AddBackgroundsToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Background not added to room");
        //}
        ///// <summary>
        ///// remove background from owned room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/background/delete")]
        //public IHttpActionResult DeleteBackground(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.RemoveBackgroundsToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Background not removed from room");
        //}
        ///// <summary>
        ///// Add existing skill to room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/skill")]
        //public IHttpActionResult PostSkill(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.AddSkillToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Skill not added to room");
        //}
        ///// <summary>
        ///// Remove existing skills from room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/skill/delete")]
        //public IHttpActionResult DeleteSkill(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.RemoveSkillFromRoom(id, roomId)) return Ok();
        //    else return BadRequest("Skill not removed from room");
        //}
        ///// <summary>
        ///// Add existing feature to existing room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/feature")]
        //public IHttpActionResult PostFeature(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.AddFeatureToRoom(id, roomId)) return Ok();
        //    else return BadRequest("Feature not added to room");
        //}
        ///// <summary>
        ///// Remove existing feature from existing room
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //[Route("room/feature/delete")]
        //public IHttpActionResult DeleteFeature(int id, int roomId)
        //{
        //    var service = CreateRoomService();
        //    if (service.RemoveFeatureFromRoom(id, roomId)) return Ok();
        //    else return BadRequest("Feature not removed from room");
        //}
        //private RoomService CreateRoomService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    return new RoomService(userId);
        //}
    }
}
