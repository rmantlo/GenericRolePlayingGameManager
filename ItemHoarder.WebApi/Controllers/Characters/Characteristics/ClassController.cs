using ItemHoarder.Models.Characters.Classes;
using ItemHoarder.Service.Characters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers.Characters.Characteristics
{
    [Authorize]
    public class ClassController : ApiController
    {
        /// <summary>
        /// Get all my classes
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {
            var service = CreateClassService();
            return Ok(service.GetAllMyClasses());
        }
        /// <summary>
        /// Get classes i own by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/class/{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateClassService();
            return Ok(service.GetClassById(id));
        }
        /// <summary>
        /// Get classes by room they are in
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/class/room/{roomId}")]
        public IHttpActionResult GetByRoom(int roomId)
        {
            var service = CreateClassService();
            return Ok(service.GetClassesByRoom(roomId));
        }
        /// <summary>
        /// Get classes in room as player
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/class/{roomId}")]
        public IHttpActionResult GetByRoomAsPlayer(int roomId)
        {
            var service = CreateClassService();
            return Ok(service.GetClassesInRoomAsPlayer(roomId));
        }
        /// <summary>
        /// Create new class
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
        [Route("api/class/create")]
        public IHttpActionResult Post(ClassCreate newClass)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateClassService();
            if (service.CreateClass(newClass)) return Ok();
            else return BadRequest("Class not created");
        }
        /// <summary>
        /// Update an existing class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        [Route("api/class/update/{id}")]
        public IHttpActionResult Put(int id, ClassCreate update)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateClassService();
            if (service.UpdateClass(id, update)) return Ok();
            else return BadRequest("Class not updated");
        }
        /// <summary>
        /// Add existing class to a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/class")]
        public IHttpActionResult Post(int id, int roomId)
        {
            var service = CreateClassService();
            if (service.AddClassToRoom(id, roomId)) return Ok();
            else return BadRequest("Class not added to room");
        }
        /// <summary>
        /// Remove existing class from a room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/class/delete")]
        public IHttpActionResult Delete(int id, int roomId)
        {
            var service = CreateClassService();
            if (service.RemoveClassToRoom(id, roomId)) return Ok();
            else return BadRequest("Class not removed from room");
        }
        /// <summary>
        /// Delete existing class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/race/delete")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateClassService();
            if (service.DeleteClass(id)) return Ok();
            else return BadRequest("Class not deleted");
        }


        private CharacterClassService CreateClassService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new CharacterClassService(userId);
        }
    }
}
