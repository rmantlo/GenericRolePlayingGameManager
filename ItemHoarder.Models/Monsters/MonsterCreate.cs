using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ItemHoarder.Models.Monsters
{
    public class MonsterCreate
    {
        public string MonsterName { get; set; }
        public HttpPostedFileBase PhotoUpload { get; set; }
        public string MonsterDescription { get; set; }
        public TypeOfMonster MonsterType { get; set; }
        public string Environment { get; set; }
        public TypeOfAlignment? Alignment { get; set; }
        public double ChallengeRating { get; set; }
        public int HitPoints { get; set; }
        public string Size { get; set; }
        public int? Speed { get; set; }
        public int? WaterSpeed { get; set; }
        public int? FlySpeed { get; set; }
        public int? BurrowSpeed { get; set; }
        public int? ClimbSpeed { get; set; }
        public int ArmorClass { get; set; }
        //Con, 2
        public Dictionary<string, int> SavingThrows { get; set; }
        public List<string> Languages { get; set; }
        public List<string> Senses { get; set; }
        //int skillID, int modStat
        public Dictionary<int, int> SkillInfo { get; set; }
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Constitution { get; set; }
        public double Intelligence { get; set; }
        public double Wisdom { get; set; }
        public double Charisma { get; set; }
    }
}
