using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.RoomFolder
{
    public class RoomUsers
    {
        [Key]
        public int ID { get; set; }
        public int RoomID { get; set; }
        public Guid PlayerID { get; set; }
        public string PlayerUsername { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
