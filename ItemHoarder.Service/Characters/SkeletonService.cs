using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Skeleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class SkeletonService
    {
        private readonly Guid _userId;
        public SkeletonService(Guid userId)
        {
            _userId = userId;
        }
        //get index of all my skeletons
        public IEnumerable<SkeletonOtherIndex> GetAllMyCharacterSkele()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterSkeletons.Where(e => e.OwnerID == _userId).Select(e => new SkeletonOtherIndex
                {
                    CharacterID = e.CharacterID,
                    OwnerID = e.OwnerID,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Gender = e.Gender,
                    VisualDescription = e.VisualDescription,
                }).ToList();
            }
        }
        //get single char skele by id (diff results if owner or not)
        public SkeletonDetails GetCharSkeletonById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterSkeletons.Single(e => e.CharacterID == id);
                return new SkeletonDetails
                {
                    CharacterID = result.CharacterID,
                    OwnerID = result.OwnerID,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Gender = result.Gender,
                    VisualDescription = result.VisualDescription,
                    BackgroundDescription = result.BackgroundDescription,
                    CharacterNotes = (result.OwnerID == _userId) ? result.CharacterNotes : "NA",
                    HeightInInches = result.HeightInInches,
                    WeightInPounds = result.WeightInPounds,
                    DateOfCreation = result.DateOfCreation,
                    DateOfModification = result.DateOfModification
                };
            }
        }
        //create char skele
        public bool CreateNewCharSkeleton(SkeletonCreate skeleton)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var created = new CharacterSkeleton
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    FirstName = skeleton.FirstName,
                    LastName = skeleton.LastName,
                    Gender = skeleton.Gender,
                    VisualDescription = skeleton.VisualDescription,
                    BackgroundDescription = skeleton.BackgroundDescription,
                    CharacterNotes = skeleton.CharacterNotes,
                    HeightInInches = skeleton.HeightInInches,
                    WeightInPounds = skeleton.WeightInPounds
                };
                ctx.CharacterSkeletons.Add(created);
                return ctx.SaveChanges() == 1;
            }
        }
        //update char skele
        public bool UpdateCharSkeleton(int id, SkeletonCreate update)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterSkeletons.Where(e => e.OwnerID == _userId).Single(e => e.CharacterID == id);
                result.FirstName = update.FirstName;
                result.LastName = update.LastName;
                result.VisualDescription = update.VisualDescription;
                result.BackgroundDescription = update.BackgroundDescription;
                result.CharacterNotes = update.CharacterNotes;
                result.WeightInPounds = update.WeightInPounds;
                result.HeightInInches = update.HeightInInches;
                result.Gender = update.Gender;
                result.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete char skele (keep instanced or not?) because of foreign key need to remove instance
        //if instanced: cannot delete skeleton, alert to delete instance first
        public string DeleteCharSkeleton(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var instances = ctx.CharacterInstances.Where(e => e.OwnerID == _userId && e.CharSkeletonID == id).ToList();
                if (instances != null || instances.Count > 0) return "Instance exists using skeleton";
                else
                {
                    var skeleton = ctx.CharacterSkeletons.Single(e => e.OwnerID == _userId && e.CharacterID == id);
                    ctx.CharacterSkeletons.Remove(skeleton);
                    var save = ctx.SaveChanges() == 1;
                    if (save) return "Skeleton deleted";
                    else return "Skeleton not deleted";
                }
            }
        }
    }
}
