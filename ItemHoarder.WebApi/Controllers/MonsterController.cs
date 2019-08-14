using ItemHoarder.Models.Monsters;
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
    public class MonsterController : ApiController
    {
        /// <summary>
        /// Get all my monsters
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {
            var service = CreateMonsterService();
            return Ok(service.GetAllMonsters());
        }
        /// <summary>
        /// Get monster details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            var service = CreateMonsterService();
            return Ok(service.GetMonsterById(id));
        }
        /// <summary>
        /// Create new monster
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        public IHttpActionResult Post(MonsterCreate monster)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateMonsterService();
            if (service.CreateMonster(monster)) return Ok();
            else return BadRequest("Monster not created");
        }
        /// <summary>
        /// update existing monster
        /// </summary>
        /// <param name="id"></param>
        /// <param name="monster"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, MonsterCreate monster)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateMonsterService();
            if (service.UpdateMonster(id, monster)) return Ok();
            else return BadRequest("Monster not updated");
        }
        /// <summary>
        /// delete existing monster
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateMonsterService();
            if (service.DeleteMonster(id)) return Ok();
            else return BadRequest("Monster not deleted");
        }
        private MonsterService CreateMonsterService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new MonsterService(userId);
        }
    }
}
