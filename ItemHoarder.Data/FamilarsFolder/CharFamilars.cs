using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.FamilarsFolder
{
    public class CharFamilars
    {
        [Key]
        public int ID { get; set; }
        public int CharacterID { get; set; }
        public string Name { get; set; }
        [ForeignKey("Familar")]
        public int FamilarID { get; set; }
        public virtual Familar Familar { get; set; }
        public int HitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public string HitDie { get; set; }
        public virtual ICollection<FamilarFeature> FamilarFeatures { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
    }
}
