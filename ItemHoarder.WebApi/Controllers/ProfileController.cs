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
        //get profile info
        public IHttpActionResult Get()
        {
            var service = CreateUserService();
            var user = service.GetProfileInfo();
            var profile = new ProfileInfo
            {
                Username = user.Username,
                ProfileImage = user.ProfileImage,
                About = user.About,
                DateOfCreation = user.DateOfCreation,
                DateOfModification = user.DateOfModification
            };
            return Ok(profile);
        }
        //create profile info
        public IHttpActionResult Post(ProfileCreate profile)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateUserService();
            if (service.CreateProfileInfo(profile)) return Ok();
            else return BadRequest("Not Created");
        }
        //update profile info
        public IHttpActionResult Put(ProfileCreate profileUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateUserService();
            if (service.UpdateProfileInfo(profileUpdate)) return Ok();
            else return BadRequest("Not Updated");
        }
        //delete profile info
        public IHttpActionResult Delete()
        {
            var service = CreateUserService();
            if (service.DeleteProfileInfo()) return Ok();
            else return BadRequest("Not Deleted");
        }
        private UserService CreateUserService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new UserService(userId);
        }
    }
}
