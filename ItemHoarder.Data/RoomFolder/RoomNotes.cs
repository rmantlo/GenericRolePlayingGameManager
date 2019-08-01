using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.RoomFolder
{
    public class RoomNotes
    {
        [Key]
        public int RoomID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string PlayerOneNotes { get; set; }
        public string PlayerTwoNotes { get; set; }
        public string PlayerThreeNotes { get; set; }
        public string PlayerFourNotes { get; set; }
        public string PlayerFiveNotes { get; set; }
        public string PlayerSixNotes { get; set; }
        public string PlayerSevenNotes { get; set; }
        public string GeneralNotes { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
