using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public class Condition
    {
        [Key]
        public int ConditionID { get; set; }
        public Guid OwnerID { get; set; }
        public string Name { get; set; }
        //of description and effects, etc?
        public string Description { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
