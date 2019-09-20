using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Skeleton
{
    public class SkeletonOtherIndex
    {
        public int CharacterID { get; set; }
        public Guid OwnerID { get; set; }
        public string FullName { get; set; }
        public TypeOfGender Gender { get; set; }
        public string VisualDescription { get; set; }
    }
}
