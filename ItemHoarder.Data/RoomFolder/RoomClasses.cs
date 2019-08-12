using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.RoomFolder
{
    public class RoomClasses
    {
        [Key]
        public int ID { get; set; }
        public Guid OwnerID { get; set; }
        public int RoomID { get; set; }
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual CharacterClass Class { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
