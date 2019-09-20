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
    public class SkillController : ApiController
    {
        /// <summary>
        /// Get index of all skills I own
        /// </summary>
        /// <returns></returns>
        [Route("api/proficiency-skills")]
        public IHttpActionResult GetAll()
        {
            var service = CreateProService();
            return Ok(service.GetAllMySkills());
        }
        /// <summary>
        /// Get index of skills in a room as GM or Player
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/room/{roomId}")]
        public IHttpActionResult GetByRoom(int roomId)
        {
            var service = CreateProService();
            return Ok(service.GetAllByRoom(roomId));
        }
        /// <summary>
        /// get skill details by ID
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
        /// Create new skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/create")]
        public IHttpActionResult Post(SkillCreate skill)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateProService();
            if (service.CreateSkill(skill)) return Ok();
            else return BadRequest("Skill not created");
        }
        /// <summary>
        /// Update existing skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        [Route("api/proficiency-skills/update/{id}")]
        public IHttpActionResult Put(int id, SkillCreate skill)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateProService();
            if (service.UpdateSkill(id, skill)) return Ok();
            else return BadRequest("Skill not deleted");
        }
        /// <summary>
        /// Delete existing skill
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
        private SkillService CreateProService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new SkillService(userId);
        }
    }
}
