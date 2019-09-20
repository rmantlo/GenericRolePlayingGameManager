using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characters.Skeleton
{
    public class SkeletonDetails
    {
        public int CharacterID { get; set; }
        public Guid OwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeOfGender Gender { get; set; }
        public string VisualDescription { get; set; }
        public string BackgroundDescription { get; set; }
        public string CharacterNotes { get; set; }
        public double HeightInInches { get; set; }
        public double WeightInPounds { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
