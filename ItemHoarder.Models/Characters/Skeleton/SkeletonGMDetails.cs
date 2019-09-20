using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Skeleton
{
    public class SkeletonGMDetails
    {
        public int CharacterID { get; set; }
        public Guid OwnerID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string VisualDescription { get; set; }
        public string BackgroundDescription { get; set; }
        public double HeightInInches { get; set; }
        public double WeightInPounds { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
