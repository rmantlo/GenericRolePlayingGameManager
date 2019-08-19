using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterClassList
    {
        [Key]
        public int ID { get; set; }
        public int CharInstanceID { get; set; }
        [ForeignKey("Class")]
        public int ClassID { get; set; }
        public virtual CharacterClass Class { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
