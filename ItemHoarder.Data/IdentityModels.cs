using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.ItemStuff;
using ItemHoarder.Data;
using ItemHoarder.Data.RoomFolder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ItemHoarder.Data.BattleFolder;
using ItemHoarder.Data.SpellsAndOther;

namespace ItemHoarder.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public enum ThemeType
    {
        Night,
        Blue,
        Green,
        Red,
        Purple,
        Pastel
    }
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(1)]
        public ThemeType TypeOfTheme { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<DiceSetting> Dice { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomNotes> RoomNotes { get; set; }
        public DbSet<RoomUsers> RoomUsers { get; set; }
        public DbSet<RoomClasses> RoomClasses { get; set; }
        public DbSet<RoomRaces> RoomRaces { get; set; }
        public DbSet<RoomFeatures> RoomFeatures { get; set; }
        public DbSet<RoomBackgrounds> RoomBackgrounds { get; set; }
        public DbSet<RoomSkills> RoomProficiencies { get; set; }

        public DbSet<CharacterSkeleton> CharacterSkeletons { get; set; }
        public DbSet<CharacterBackground> CharacterBackgrounds { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<CharacterClassList> CharacterClassList { get; set; }
        public DbSet<CharacterSubClass> SubClasses { get; set; }
        public DbSet<CharClassSubConnection> ClassSubConnection { get; set; }
        public DbSet<CharacterRace> CharacterRaces { get; set; }
        public DbSet<CharacterFeatures> CharacterFeatures { get; set; }
        public DbSet<CharacterFeatList> CharacterFeatList { get; set; }
        public DbSet<CharProficiencySkills> CharProficiencySkills { get; set; }
        public DbSet<CharacterSkill> ProficiencySkills { get; set; }
        public DbSet<CharacterInstanced> CharacterInstances { get; set; }

        public DbSet<Monster> Monsters { get; set; }
        public DbSet<MonsterSkills> MonsterSkills { get; set; }

        public DbSet<BattleInstance> BattleInstances { get; set; }
        public DbSet<BattleMonsters> BattleMonsterList { get; set; }
        public DbSet<BattleItem> BattleItemList { get; set; }
        public DbSet<BattleRandomItem> BattleRandomItemList { get; set; }

        public DbSet<Chest> Chests { get; set; }
        public DbSet<ChestItem> ChestItems { get; set; }
        public DbSet<ChestRandomItem> ChestRandomItems { get; set; }

        public DbSet<Spells> Spells { get; set; }
        public DbSet<CharacterSpells> CharacterSpells { get; set; }

        public DbSet<Condition> Conditions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }
        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }

        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {
            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.UserId);
            }
        }

    }
}