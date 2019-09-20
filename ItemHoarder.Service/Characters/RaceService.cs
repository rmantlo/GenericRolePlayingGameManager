using ItemHoarder.Data;
using ItemHoarder.Data.CharacterInfo;
using ItemHoarder.Data.RoomFolder;
using ItemHoarder.Models.Characters.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Characters
{
    public class RaceService
    {
        private readonly Guid _userId;
        public RaceService(Guid userId)
        {
            _userId = userId;
        }
        //get all my races
        public IEnumerable<RaceIndex> GetAllMyRaces()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.CharacterRaces.Where(e => e.OwnerID == _userId && e.IsDeactivated == false).Select(e => new RaceIndex
                {
                    RaceID = e.RaceID,
                    GameTag = e.GameTag,
                    RaceName = e.RaceName,
                    VisualDescription = e.VisualDescription,
                    DateOfCreation = e.DateOfCreation,
                    DateOfModification = e.DateOfModification
                }).ToArray();
                return results;
            }
        }
        //get races by room
        public IEnumerable<RaceIndex> GetAllByRoom(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var rooms = ctx.RoomRaces.Where(e => e.RoomID == roomId).ToList();
                List<RaceIndex> roomRaces = new List<RaceIndex>();
                foreach (var r in rooms)
                {
                    var race = ctx.CharacterRaces.Single(e => e.RaceID == r.RaceID);
                    var raceDisplay = new RaceIndex
                    {
                        RaceID = race.RaceID,
                        GameTag = race.GameTag,
                        RaceName = race.RaceName,
                        VisualDescription = race.VisualDescription,
                        DateOfCreation = race.DateOfCreation,
                        DateOfModification = race.DateOfModification
                    };
                    roomRaces.Add(raceDisplay);
                }

                return roomRaces;
            }
        }
        //get race by id
        public RaceDetails GetRaceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var race = ctx.CharacterRaces.Single(e => e.RaceID == id);
                var raceDisplay = new RaceDetails
                {
                    RaceID = race.RaceID,
                    GameTag = race.GameTag,
                    RaceName = race.RaceName,
                    VisualDescription = race.VisualDescription,
                    Speed = race.Speed,
                    Size = race.Size,
                    Languages = race.Languages.Split('|').ToList(),
                    WeaponProficiencies = race.WeaponProficiencies.Split('|').ToList(),
                    ArmorProficiencies = race.ArmorProficiencies.Split('|').ToList(),
                    ToolProficiencies = race.ToolProficiencies.Split('|').ToList(),
                    DefensiveRacialTrait = race.DefensiveRacialTrait.Split('|').ToList(),
                    FeatRacialTrait = race.FeatRacialTrait.Split('|').ToList(),
                    MagicalRacialTrait = race.MagicalRacialTrait.Split('|').ToList(),
                    SensesRacialTrait = race.SensesRacialTrait.Split('|').ToList(),
                    Strength = race.Strength,
                    Dexterity = race.Dexterity,
                    Constitution = race.Constitution,
                    Intelligence = race.Intelligence,
                    Wisdom = race.Wisdom,
                    Charisma = race.Charisma,
                    DateOfCreation = race.DateOfCreation,
                    DateOfModification = race.DateOfModification
                };
                return raceDisplay;
            }
        }
        //create race
        public bool CreateRace(RaceCreate race)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newRace = new CharacterRace
                {
                    OwnerID = _userId,
                    RaceName = race.RaceName,
                    VisualDescription = race.VisualDescription,
                    Speed = race.Speed,
                    Size = race.Size,
                    Languages = String.Join("|", race.Languages),
                    WeaponProficiencies = String.Join("|", race.WeaponProficiencies),
                    ArmorProficiencies = String.Join("|", race.ArmorProficiencies),
                    ToolProficiencies = String.Join("|", race.ToolProficiencies),
                    DefensiveRacialTrait = String.Join("|", race.DefensiveRacialTrait),
                    FeatRacialTrait = String.Join("|", race.FeatRacialTrait),
                    MagicalRacialTrait = String.Join("|", race.MagicalRacialTrait),
                    SensesRacialTrait = String.Join("|", race.SensesRacialTrait),
                    Strength = race.Strength,
                    Dexterity = race.Dexterity,
                    Constitution = race.Constitution,
                    Intelligence = race.Intelligence,
                    Wisdom = race.Wisdom,
                    Charisma = race.Charisma,
                    IsDeactivated = false,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.CharacterRaces.Add(newRace);
                return ctx.SaveChanges() == 1;
            }
        }
        //update race
        public bool UpdateRace(int id, RaceCreate race)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.CharacterRaces.SingleOrDefault(e => e.OwnerID == _userId && e.RaceID == id);
                if (result != null)
                {
                    result.RaceName = race.RaceName;
                    result.GameTag = race.GameTag;
                    result.VisualDescription = race.VisualDescription;
                    result.Speed = race.Speed;
                    result.Size = race.Size;
                    result.Languages = String.Join("|", race.Languages);
                    result.WeaponProficiencies = String.Join("|", race.WeaponProficiencies);
                    result.ArmorProficiencies = String.Join("|", race.ArmorProficiencies);
                    result.ToolProficiencies = String.Join("|", race.ToolProficiencies);
                    result.DefensiveRacialTrait = String.Join("|", race.DefensiveRacialTrait);
                    result.FeatRacialTrait = String.Join("|", race.FeatRacialTrait);
                    result.MagicalRacialTrait = String.Join("|", race.MagicalRacialTrait);
                    result.SensesRacialTrait = String.Join("|", race.SensesRacialTrait);
                    result.Strength = race.Strength;
                    result.Dexterity = race.Dexterity;
                    result.Constitution = race.Constitution;
                    result.Intelligence = race.Intelligence;
                    result.Wisdom = race.Wisdom;
                    result.Charisma = race.Charisma;
                    result.DateOfModification = DateTimeOffset.UtcNow;
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //delete race: just switch to deactivated = true
        public bool DeleteRace(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var roomRaces = ctx.RoomRaces.Where(e => e.OwnerID == _userId && e.RaceID == id).ToList();
                foreach (var r in roomRaces)
                {
                    ctx.RoomRaces.Remove(r);
                }
                var race = ctx.CharacterRaces.Single(e => e.OwnerID == _userId && e.RaceID == id);
                race.IsDeactivated = true;
                return ctx.SaveChanges() == 1 + roomRaces.Count();
            }
        }
    }
}
