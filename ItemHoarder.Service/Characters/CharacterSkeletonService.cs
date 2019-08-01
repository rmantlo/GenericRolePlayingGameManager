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

        public IEnumerable<CharacterSkeleton> GetAllMyCharacterSkele()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterSkeletons.Where(e => e.OwnerID == _userId);
            }
        }
        //get single char skele by id
        public CharacterSkeleton GetCharSkeletonById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterSkeletons.SingleOrDefault(e => e.OwnerID == _userId && e.CharacterID == id); 
            }
        }
        //update char skele
        public bool CreateNewCharSkeleton(CharSkeletonCreate skeleton)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var created = new CharacterSkeleton
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    FirstName = skeleton.FirstName,
                    LastName = skeleton.LastName,
                    Gender = skeleton.Gender,

                };
                ctx.CharacterSkeletons.Add(created);
                return ctx.SaveChanges() == 1;
            }
        }
        //delete char skele (keep instanced or not?) because of foreign key need to remove instance
    }
}
