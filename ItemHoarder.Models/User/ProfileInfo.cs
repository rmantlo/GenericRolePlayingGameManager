using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.User
{
    public class ProfileInfo
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public Photo ProfileImage { get; set; }
        public string About { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
