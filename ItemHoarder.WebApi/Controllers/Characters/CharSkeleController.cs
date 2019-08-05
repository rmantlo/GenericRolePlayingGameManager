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
    public class CharSkeleController : ApiController
    {
        //get all char skeletons
        //[Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var service = CreateCharSkeleService();
            var results = service.GetAllMyCharacterSkele();
            return Ok(results);
        }
        //get char skeleton by ID
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateCharSkeleService();
            var result = service.GetCharSkeletonById(id);
            return Ok(result);
        }
        //create char skeleton
        [Route("create")]
        public IHttpActionResult Post(CharSkeletonCreate skeleton)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCharSkeleService();
            if (service.CreateNewCharSkeleton(skeleton)) return Ok();
            else return BadRequest("Character skeleton not created");
        }
        //update char skeleton
        [Route("update/{id}")]
        public IHttpActionResult Put(int id, CharSkeletonCreate skeleton)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCharSkeleService();
            if (service.UpdateCharSkeleton(id, skeleton)) return Ok();
            else return BadRequest("Character skeleton not updated");
        }
        //delete char skeleton
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCharSkeleService();
            var result = service.DeleteCharSkeleton(id);
            if (result == "Skeleton deleted") return Ok();
            else if (result == "Instance exists using skeleton") return BadRequest("Character skeleton could not be deleted. Skeleton is using in an Instanced Character. Please delete instance first.");
            else return BadRequest("Character skeleton could not be deleted");
        }

        private CharacterSkeletonService CreateCharSkeleService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new CharacterSkeletonService(userId);
        }
    }
}
