using ItemHoarder.Data;
using ItemHoarder.Models.Characters.Skeleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class CharacterSkeletonService
    {
        private readonly Guid _userId;
        public CharacterSkeletonService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<CharSkeleDisplay> GetAllMyCharacterSkele()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterSkeletons.Where(e => e.OwnerID == _userId).Select(e => new CharSkeleDisplay
                {
                    ID = e.CharacterID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    VisualDescription = e.VisualDescription,
                    BackgroundDescription = e.BackgroundDescription,
                    CharacterNotes = e.CharacterNotes,
                    HeightInInches = e.HeightInInches,
                    WeightInPounds = e.WeightInPounds,
                    PersonalityTraits = e.PersonalityTraits,
                    Ideals = e.Ideals,
                    Bonds = e.Bonds,
                    Flaws = e.Flaws
                }).ToList();
            }
        }
        //get single char skele by id
        public CharSkeleDisplay GetCharSkeletonById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterSkeletons.Where(a => a.OwnerID == _userId).Single(e => e.CharacterID == id);
                return new CharSkeleDisplay
                {
                    ID = result.CharacterID,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Gender = result.Gender,
                    VisualDescription = result.VisualDescription,
                    BackgroundDescription = result.BackgroundDescription,
                    CharacterNotes = result.CharacterNotes,
                    HeightInInches = result.HeightInInches,
                    WeightInPounds = result.WeightInPounds,
                    PersonalityTraits = result.PersonalityTraits,
                    Ideals = result.Ideals,
                    Bonds = result.Bonds,
                    Flaws = result.Flaws
                };
            }
        }
        //create char skele
        public bool CreateNewCharSkeleton(CharSkeletonCreate skeleton)
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
                    WeightInPounds = skeleton.WeightInPounds,
                    PersonalityTraits = skeleton.PersonalityTraits,
                    Ideals = skeleton.Ideals,
                    Bonds = skeleton.Bonds,
                    Flaws = skeleton.Flaws,
                };
                ctx.CharacterSkeletons.Add(created);
                return ctx.SaveChanges() == 1;
            }
        }
        //update char skele
        public bool UpdateCharSkeleton(int id, CharSkeletonCreate update)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterSkeletons.Where(e => e.OwnerID == _userId).Single(e => e.CharacterID == id);
                if (update.FirstName != null && update.FirstName != "") { result.FirstName = update.FirstName; };
                if (update.LastName != null && update.LastName != "") { result.LastName = update.LastName; };
                if (update.VisualDescription != null && update.VisualDescription != "") { result.VisualDescription = update.VisualDescription; };
                if (update.BackgroundDescription != null && update.BackgroundDescription != "") { result.BackgroundDescription = update.BackgroundDescription; };
                if (update.CharacterNotes != null && update.CharacterNotes != "") { result.CharacterNotes = update.CharacterNotes; };
                if (update.PersonalityTraits != null && update.PersonalityTraits != "") { result.PersonalityTraits = update.PersonalityTraits; };
                if (update.Ideals != null && update.Ideals != "") { result.Ideals = update.Ideals; };
                if (update.Bonds != null && update.Bonds != "") { result.Bonds = update.Bonds; };
                if (update.Flaws != null && update.Flaws != "") { result.Flaws = update.Flaws; };

                result.WeightInPounds = update.WeightInPounds;
                result.HeightInInches = update.HeightInInches;
                result.Gender = update.Gender;
                result.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        //delete char skele (keep instanced or not?) because of foreign key need to remove instance
        //if instanced: cannot delete skeleton, alert to delete instance first
        /// <summary>
        /// abc de f g h i j
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteCharSkeleton(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var instances = ctx.CharacterInstances.Where(e => e.OwnerID == _userId && e.CharSkeletonID == id).ToList();
                if (instances != null) return "Instance exists using skeleton";
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
