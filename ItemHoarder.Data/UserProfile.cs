using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class UserProfile
    {
        [Key]
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string About { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
