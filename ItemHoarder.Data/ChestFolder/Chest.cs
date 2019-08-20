using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class Chest
    {
        //limit to 5 chests per room?
        [Key]
        public int ChestID { get; set; }
        public Guid OwnerID { get; set; }
        public int RoomID { get; set; }
        public ICollection<ChestItem> ChestItems { get; set; }
        public ICollection<ChestRandomItem> ChestRandomItems { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
