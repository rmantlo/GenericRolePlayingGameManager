using ItemHoarder.Data;
using ItemHoarder.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class UserService
    {
        private readonly Guid _userId;
        public UserService(Guid userId)
        {
            _userId = userId;
        }
        //get profile info
        public UserProfile GetProfileInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Profiles.SingleOrDefault(e => e.UserID == _userId);
            }
        }
        //create profile info
        public bool CreateProfileInfo(ProfileCreate profile)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.SingleOrDefault(e => e.Id == _userId.ToString());

                var newProfile = new UserProfile
                {
                    UserID = _userId,
                    Username = user.UserName,
                    ProfileImage = profile.ProfileImage,
                    About = profile.About,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    DateOfModification = DateTimeOffset.UtcNow
                };
                ctx.Profiles.Add(newProfile);
                return ctx.SaveChanges() == 1;
            }
        }
        //update profile info
        public bool UpdateProfileInfo(ProfileCreate profile)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldProfile = ctx.Profiles.SingleOrDefault(e => e.UserID == _userId);
                if (profile.ProfileImage != null && profile.ProfileImage != "") { oldProfile.ProfileImage = profile.ProfileImage; };
                if(profile.About != null && profile.About != "") { oldProfile.About = profile.About; };
                oldProfile.DateOfModification = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }

        //delete profile info
        public bool DeleteProfileInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.Profiles.Single(e => e.UserID == _userId);
                ctx.Profiles.Remove(profile);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
