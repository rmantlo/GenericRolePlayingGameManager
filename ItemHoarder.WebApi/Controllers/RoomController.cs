using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers
{
    [Authorize]
    public class RoomController : ApiController
    {
        public bool CreateRoom()
        {
            if(User.IsInRole("GM") || User.IsInRole("Admin"))
            {
                return true;
            }
        }
    }
}
