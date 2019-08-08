using ItemHoarder.Models.Characters.ProficiencySkills;
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
    public class ProficiencySkillsController : ApiController
    {
        /// <summary>
        /// Get all proficiency skills I own
        /// </summary>
        /// <returns></returns>
        [Route("api/proficiency-skills")]
        public IHttpActionResult GetAll()
        {
            var service = CreateProService();
            return Ok(service.GetAllMySkills());
        }
        /// <summary>
        /// get pro skill details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateProService();
            return Ok(service.GetSkillById(id));
        }
        /// <summary>
        /// Get all pro skills in a room as owner
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/room/{roomId}")]
        public IHttpActionResult GetByRoom(int roomId)
        {
            var service = CreateProService();
            return Ok(service.GetSkillsByMyRoom(roomId));
        }
        /// <summary>
        /// Get all pro skills in a room as player
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/proficiency-skills/{roomId}")]
        public IHttpActionResult GetByRoomAsPlayer(int roomId)
        {
            var service = CreateProService();
            return Ok(service.GetSkillsByRoomAsPlayer(roomId));
        }
        /// <summary>
        /// Create new proficiency skill
        /// </summary>
        /// <param name="background"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/create")]
        public IHttpActionResult Post(SkillCreate background)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateProService();
            if (service.CreateSkill(background)) return Ok();
            else return BadRequest("Skill not created");
        }
        /// <summary>
        /// Update existing proficiency skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/update/{id}")]
        public IHttpActionResult Put(int id, SkillCreate background)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateProService();
            if (service.UpdateSkill(id, background)) return Ok();
            else return BadRequest("Skill not deleted");
        }
        /// <summary>
        /// Add existing skill to room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/proficiency-skills/{id}")]
        public IHttpActionResult AddSkillToRoom(int id, int roomId)
        {
            var service = CreateProService();
            if (service.AddSkillToRoom(id, roomId)) return Ok();
            else return BadRequest("Skill not added to room");
        }
        /// <summary>
        /// Remove existing skills from room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/room/proficiency-skills/delete")]
        public IHttpActionResult DeleteSkillFromRoom(int id, int roomId)
        {
            var service = CreateProService();
            if (service.RemoveSkillFromRoom(id, roomId)) return Ok();
            else return BadRequest("Skill not removed from room");
        }
        /// <summary>
        /// Delete existing proficiency skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateProService();
            if (service.DeleteSkill(id)) return Ok();
            else return BadRequest("Skill not deleted");
        }
        private CharacterSkillService CreateProService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new CharacterSkillService(userId);
        }
    }
}
