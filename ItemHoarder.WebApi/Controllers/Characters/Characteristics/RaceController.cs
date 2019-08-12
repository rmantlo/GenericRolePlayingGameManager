using ItemHoarder.Models.Characters.Races;
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
    public class RaceController : ApiController
    {
        /// <summary>
        /// Get all of my races
        /// </summary>
        /// <returns></returns>
        [Route("api/race")]
        public IHttpActionResult GetAll()
        {
            var service = CreateRaceService();
            return Ok(service.GetAllMyRaces());
        }
        /// <summary>
        /// Get race by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/race/{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateRaceService();
            return Ok(service.GetMyRaceById(id));
        }
        /// <summary>
        /// Get races by room ID
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/race/room/{roomId}")]
        public IHttpActionResult GetByRoom(int roomId)
        {
            var service = CreateRaceService();
            return Ok(service.GetMyRacesByRoomId(roomId));
        }
        /// <summary>
        /// Get races of room im a player in
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/race/{roomId}")]
        public IHttpActionResult GetByRoomAsPlayer(int roomId)
        {
            var service = CreateRaceService();
            return Ok(service.GetRacesOfRoomAsPlayer(roomId));
        }
        /// <summary>
        /// Create new race
        /// </summary>
        /// <param name="newRace"></param>
        /// <returns></returns>
        [Route("api/race/create")]
        public IHttpActionResult Post(RaceCreate newRace)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateRaceService();
            if (service.CreateRace(newRace)) return Ok();
            else return BadRequest("Race not created");
        }
        /// <summary>
        /// Update existing race
        /// </summary>
        /// <param name="id"></param>
        /// <param name="race"></param>
        /// <returns></returns>
        [Route("api/race/update/{id}")]
        public IHttpActionResult Put(int id, RaceCreate race)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateRaceService();
            if (service.UpdateRace(id, race)) return Ok();
            else return BadRequest("Race not updated");
        }
        /// <summary>
        /// Delete existing race
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/race/delete")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateRaceService();
            if (service.DeleteRace(id)) return Ok();
            else return BadRequest("Race not deleted");
        }
        private CharacterRaceService CreateRaceService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new CharacterRaceService(userId);
        }
    }
}
