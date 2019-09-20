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
    public class SubClassController : ApiController
    {
        /// <summary>
        /// Gets and index of all user's created subclasses
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {
            var service = CreateSubService();
            return Ok(service.GetAllMySubClasses());
        }
        /// <summary>
        /// Gets index of all subclasses attached to a particular class. Both GM and Player can recieve these.
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public IHttpActionResult GetAllByClass(int classId)
        {
            var service = CreateSubService();
            return Ok(service.GetAllByClass(classId));
        }
        /// <summary>
        /// gets details of subclass by subclass id. Both GM and player can recieve this
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetSubClassDetails(int id)
        {
            var service = CreateSubService();
            return Ok(service.GetSubClassById(id));
        }
        /// <summary>
        /// Create new subclass
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public IHttpActionResult Post(SubClassCreate sub)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateSubService();
            if (service.CreateSubClass(sub)) return Ok();
            else return BadRequest("SubClass not created");
        }
        /// <summary>
        /// update an existing subclass you own
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, SubClassEdit sub)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateSubService();
            if (service.UpdateSubClass(id, sub)) return Ok();
            else return BadRequest("SubClass not created");
        }
        /// <summary>
        /// delete subclass you own
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSubService();
            if (service.DeleteSubClass(id)) return Ok();
            else return BadRequest("SubClass not deleted");
        }

        private SubClassService CreateSubService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new SubClassService(userId);
        }
    }
}
