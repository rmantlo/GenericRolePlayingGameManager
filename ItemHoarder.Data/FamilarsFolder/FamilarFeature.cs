using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.FamilarsFolder
{
    public class FamilarFeature
    {
        [Key]
        public int ID { get; set; }
        public string FeatName { get; set; }
        public string FeatDescription { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
