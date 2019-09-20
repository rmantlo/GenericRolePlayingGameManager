using ItemHoarder.Data;
using ItemHoarder.Models.User;
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
    public class ProfileController : ApiController
    {
        /// <summary>
        /// Get your own profile information
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var service = CreateUserService();
            return Ok(service.GetProfileInfo());
        }
        /// <summary>
        /// Get profile information of player by their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string username)
        {
            var service = CreateUserService();
            return Ok(service.GetProfileInfoByUsername(username));
        }
        /// <summary>
        /// Create new profile, on register, if user already has profile, this returns Badrequest
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Post()
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateUserService();
            if (service.CreateProfileInfo()) return Ok();
            else return BadRequest("Not Created");
        }
        /// <summary>
        /// Update your existing profile
        /// </summary>
        /// <param name="profileUpdate"></param>
        /// <returns></returns>
        public IHttpActionResult Put(ProfileCreate profileUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateUserService();
            if (service.UpdateProfileInfo(profileUpdate)) return Ok();
            else return BadRequest("Not Updated");
        }
        //no endpoint for user profile delete, profile delete is done on user account delete
        private UserService CreateUserService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new UserService(userId);
        }
    }
}
