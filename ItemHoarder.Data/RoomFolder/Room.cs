using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("RoomNotes")]
        public int RoomNotesID { get; set; }
        public virtual RoomNotes RoomNotes { get; set; }
        public virtual ICollection<RoomUsers> RoomUsers { get; set; }
        public virtual ICollection<RoomClasses> RoomClasses { get; set; }
        public virtual ICollection<RoomBackgrounds> RoomBackgrounds { get; set; }
        public virtual ICollection<RoomRaces> RoomRaces { get; set; }
        public virtual ICollection<RoomProficiencies> RoomProficiencies { get; set; }
        public virtual ICollection<RoomFeatures> RoomFeatures { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
