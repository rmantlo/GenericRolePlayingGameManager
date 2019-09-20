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
        /// Get classes index info by roomID as GM or player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetClassesByRoom(int id)
        {
            var service = CreateClassService();
            return Ok(service.GetClassById(id));
        }
        /// <summary>
        /// Get class details by class id as player or GM
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetClassDetails(int id)
        {
            var service = CreateClassService();
            return Ok(service.GetClassById(id));
        }
        /// <summary>
        /// Create new class
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
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
        public IHttpActionResult Put(int id, ClassCreate update)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateClassService();
            if (service.UpdateClass(id, update)) return Ok();
            else return BadRequest("Class not updated");
        }
        /// <summary>
        /// Delete existing class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateClassService();
            if (service.DeleteClass(id)) return Ok();
            else return BadRequest("Class not deleted");
        }


        private ClassService CreateClassService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ClassService(userId);
        }
    }
}
