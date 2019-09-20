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
        public ProfileInfo GetProfileInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.Profiles.SingleOrDefault(e => e.UserID == _userId);
                return new ProfileInfo
                {
                    UserID = profile.UserID,
                    Username = profile.Username,
                    ProfileImage = profile.Photo.ToList()[0],
                    About = profile.About,
                    DateOfCreation = profile.DateOfCreation,
                    DateOfModification = profile.DateOfModification
                };
            }
        }
        //get other persons info
        public ProfileInfo GetProfileInfoByUsername(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var profile = ctx.Profiles.SingleOrDefault(e => e.Username == username);
                return new ProfileInfo
                {
                    Username = profile.Username,
                    ProfileImage = profile.Photo.ToList()[0],
                    About = profile.About,
                    DateOfCreation = profile.DateOfCreation
                };
            }
        }
        //create profile info
        public bool CreateProfileInfo()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.SingleOrDefault(e => e.Id == _userId.ToString());
                if (ctx.Profiles.SingleOrDefault(e => e.UserID == _userId) != null)
                {
                    //List<Photo> photoList = new List<Photo>();
                    //if (profile.PhotoUpload != null && profile.PhotoUpload.ContentLength > 0)
                    //{
                    //    var photo = new Photo
                    //    {
                    //        PhotoName = System.IO.Path.GetFileName(profile.PhotoUpload.FileName),
                    //        FileType = FileType.Profile,
                    //        ContentType = profile.PhotoUpload.ContentType
                    //    };
                    //    using (var reader = new System.IO.BinaryReader(profile.PhotoUpload.InputStream))
                    //    {
                    //        photo.Content = reader.ReadBytes(profile.PhotoUpload.ContentLength);
                    //    }
                    //    photoList.Add(photo);
                    //}
                    var newProfile = new UserProfile
                    {
                        UserID = _userId,
                        Username = user.UserName,
                        Photo = null,
                        About = null,
                        DateOfCreation = DateTimeOffset.UtcNow,
                        DateOfModification = DateTimeOffset.UtcNow
                    };
                    ctx.Profiles.Add(newProfile);
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //update profile info
        public bool UpdateProfileInfo(ProfileCreate profile)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldProfile = ctx.Profiles.SingleOrDefault(e => e.UserID == _userId);
                if (oldProfile != null)
                {
                    List<Photo> photoList = new List<Photo>();
                    if (profile.PhotoUpload != null && profile.PhotoUpload.ContentLength > 0)
                    {
                        var photo = new Photo
                        {
                            PhotoName = System.IO.Path.GetFileName(profile.PhotoUpload.FileName),
                            FileType = FileType.Profile,
                            ContentType = profile.PhotoUpload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(profile.PhotoUpload.InputStream))
                        {
                            photo.Content = reader.ReadBytes(profile.PhotoUpload.ContentLength);
                        }
                        photoList.Add(photo);
                    }
                    oldProfile.About = profile.About;
                    oldProfile.Photo = photoList;
                    oldProfile.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1; //check if correct, may be 2
                }
                else return false;
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
