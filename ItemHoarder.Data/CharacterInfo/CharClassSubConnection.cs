using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharClassSubConnection
    {
        [Key]
        public int ClassSubConnectionID { get; set; }
        public Guid OwnerID { get; set; }
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual CharacterClass Class { get; set; }
        [ForeignKey("SubClass")]
        public int SubClassID { get; set; }
        public virtual CharacterSubClass SubClass { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
