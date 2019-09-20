using ItemHoarder.Models.Characters.Features;
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
    public class FeaturesController : ApiController
    {
        /// <summary>
        /// Get all features you own
        /// </summary>
        /// <returns></returns>
        [Route("api/features")]
        public IHttpActionResult GetAll()
        {
            var service = CreateFeatureService();
            return Ok(service.GetAllMyFeatures());
        }
        /// <summary>
        /// get all features in a room as GM or Player
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [Route("api/features/room/{roomId}")]
        public IHttpActionResult GetByRoom(int roomId)
        {
            var service = CreateFeatureService();
            return Ok(service.GetAllByRoom(roomId));
        }
        /// <summary>
        /// Get feature details by Id as GM or Player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/features/{id}")]
        public IHttpActionResult Get(int id)
        {
            var service = CreateFeatureService();
            return Ok(service.GetFeatureById(id));
        }
        /// <summary>
        /// Create new feature
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        [Route("api/features/create")]
        public IHttpActionResult Post(FeatureCreate feature)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateFeatureService();
            if (service.CreateFeature(feature)) return Ok();
            else return BadRequest("Feature not created");
        }
        /// <summary>
        /// Update existing feature
        /// </summary>
        /// <param name="id"></param>
        /// <param name="feature"></param>
        /// <returns></returns>
        [Route("api/features/update/{id}")]
        public IHttpActionResult Put(int id, FeatureCreate feature)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateFeatureService();
            if (service.UpdateFeature(id, feature)) return Ok();
            else return BadRequest("Update not saved");
        }
        /// <summary>
        /// Delete feature
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/features/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateFeatureService();
            if (service.DeleteFeature(id)) return Ok();
            else return BadRequest("Feature not deleted");
        }
        private FeatureService CreateFeatureService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new FeatureService(userId);
        }
    }
}
