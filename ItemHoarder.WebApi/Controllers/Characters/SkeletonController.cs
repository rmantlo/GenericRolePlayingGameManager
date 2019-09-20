using ItemHoarder.Models.Characters.Skeleton;
using ItemHoarder.Service.Characters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers.Characters
{
    [Authorize]
    [RoutePrefix("api/character-skeleton")]
    public class SkeletonController : ApiController
    {
        /// <summary>
        /// Get all character skeletons created by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var service = CreateCharSkeleService();
            var results = service.GetAllMyCharacterSkele();
            return Ok(results);
        }
        /// <summary>
        /// Get character skeleton by id, character note's removed if user is not skeleton owner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateCharSkeleService();
            var result = service.GetCharSkeletonById(id);
            return Ok(result);
        }
        /// <summary>
        /// Create new character skeleton
        /// </summary>
        /// <param name="skeleton"></param>
        /// <returns></returns>
        [Route("create")]
        public IHttpActionResult Post(SkeletonCreate skeleton)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCharSkeleService();
            if (service.CreateNewCharSkeleton(skeleton)) return Ok();
            else return BadRequest("Character skeleton not created");
        }
        /// <summary>
        /// Update existing character skeleton
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skeleton"></param>
        /// <returns></returns>
        [Route("update/{id}")]
        public IHttpActionResult Put(int id, SkeletonCreate skeleton)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCharSkeleService();
            if (service.UpdateCharSkeleton(id, skeleton)) return Ok();
            else return BadRequest("Character skeleton not updated");
        }
        /// <summary>
        /// Delete existing character skeleton by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCharSkeleService();
            var result = service.DeleteCharSkeleton(id);
            if (result == "Skeleton deleted") return Ok();
            else if (result == "Instance exists using skeleton") return BadRequest("Character skeleton could not be deleted. Skeleton is using in an Instanced Character. Please delete instance first.");
            else return BadRequest("Character skeleton could not be deleted");
        }

        private SkeletonService CreateCharSkeleService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new SkeletonService(userId);
        }
    }
}
