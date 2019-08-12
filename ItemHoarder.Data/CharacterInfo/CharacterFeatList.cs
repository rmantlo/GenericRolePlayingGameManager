using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.CharacterInfo
{
    public class CharacterFeatList
    {
        [Key]
        public int ID { get; set; }
        public int CharInstanceID { get; set; }
        [ForeignKey("Feature")]
        public int FeatureID { get; set; }
        public virtual CharacterFeatures Feature { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
