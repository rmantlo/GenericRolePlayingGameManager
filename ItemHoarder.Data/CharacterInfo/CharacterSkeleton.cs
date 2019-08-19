using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum TypeOfGender
    {
        None,
        Male,
        Female,
        Binary,
        TransMale,
        TransFemale,
        Other
    }
    
    public class CharacterSkeleton
    {
        [Key]
        public int CharacterID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Photo> SkeletonPhoto { get; set; }
        public TypeOfGender Gender { get; set; }
        public string VisualDescription { get; set; }
        public string BackgroundDescription { get; set; }
        public string CharacterNotes { get; set; }
        public double HeightInInches { get; set; }
        public double WeightInPounds { get; set; }
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }
        //public virtual ICollection<CharacterInstanced> Instances { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
