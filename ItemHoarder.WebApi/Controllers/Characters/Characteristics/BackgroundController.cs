using ItemHoarder.Models.Characters.Backgrounds;
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
    public class BackgroundController : ApiController
    {
        /// <summary>
        /// Get all index backgrounds you own
        /// </summary>
        /// <returns></returns>
        [Route("api/background")]
        public IHttpActionResult GetAll()
        {
            var service = CreateBackgroundService();
            return Ok(service.GetAllMyBackgrounds());
        }
        /// <summary>
        /// get all index backgrounds in room, as either GM or Player
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public IHttpActionResult GetAllByRoom(int roomId)
        {
            var service = CreateBackgroundService();
            return Ok(service.GetBackgroundsByRoom(roomId));
        }
        /// <summary>
        /// get background details by id as GM or Player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/background/{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateBackgroundService();
            return Ok(service.GetBackgroundsById(id));
        }
        /// <summary>
        /// Create new Background
        /// </summary>
        /// <param name="background"></param>
        /// <returns></returns>
        public IHttpActionResult Post(BackgroundCreate background)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateBackgroundService();
            if (service.CreateBackgrounds(background)) return Ok();
            else return BadRequest("Background not created");
        }
        /// <summary>
        /// Update existing background
        /// </summary>
        /// <param name="id"></param>
        /// <param name="background"></param>
        /// <returns></returns>
        [Route("api/background/update/{id}")]
        public IHttpActionResult Put(int id, BackgroundCreate background)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateBackgroundService();
            if (service.UpdateBackgrounds(id, background)) return Ok();
            else return BadRequest("Background not updated");
        }
        /// <summary>
        /// delete background
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/background/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateBackgroundService();
            if (service.DeleteBackgrounds(id)) return Ok();
            else return BadRequest("Background not deleted");
        }


        private BackgroundService CreateBackgroundService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new BackgroundService(userId);
        }
    }
}
