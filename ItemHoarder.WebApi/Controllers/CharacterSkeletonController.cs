using ItemHoarder.Service.Characters;
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
    public class CharacterSkeletonController : ApiController
    {
        //get all char skeletons
        //get char skeleton by ID
        //create char skeleton
        //update char skeleton
        //delete char skeleton

        private CharacterSkeletonService CreateCharSkeleService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new CharacterSkeletonService(userId);
        }
    }
}
