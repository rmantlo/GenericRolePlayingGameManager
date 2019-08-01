using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.RoomFolder
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string RoomName { get; set; }
        public string GameType { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
