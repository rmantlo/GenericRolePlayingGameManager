using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ItemHoarder.Models.User
{
    public class ProfileCreate
    {
        public HttpPostedFileBase PhotoUpload { get; set; }
        public string About { get; set; }
    }
}
