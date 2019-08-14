using ItemHoarder.Data;
using ItemHoarder.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class MonsterService
    {
        private readonly Guid _userId;
        public MonsterService(Guid userId)
        {
            _userId = userId;
        }
        //get all my monsters
        public IEnumerable<MonsterDisplay> GetAllMonsters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.Monsters.Where(e => e.OwnerID == _userId).ToList();
                List<MonsterDisplay> monsters = new List<MonsterDisplay>();
                foreach (var e in results)
                {
                    Dictionary<string, int> savingThrows = new Dictionary<string, int>();
                    var save = e.SavingThrows.Split('|');
                    foreach (var s in save)
                    {
                        var split = s.Split(' ');

                        savingThrows.Add(split[0], int.Parse(split[1].TrimStart('+')));
                    }
                    List<MonsterSkillDisplay> monsterSkills = new List<MonsterSkillDisplay>();
                    foreach (var skill in e.Skills)
                    {
                        var skillDisplay = new MonsterSkillDisplay
                        {
                            SkillID = skill.SkillID,
                            SkillName = skill.Skills.Name,
                            SkillDescription = skill.Skills.Description,
                            StatApplied = skill.Skills.StatApplied,
                            ModStatNumber = skill.ModStat
                        };
                        monsterSkills.Add(skillDisplay);
                    }
                    var monster = new MonsterDisplay
                    {
                        MonsterID = e.MonsterID,
                        OwnerID = e.OwnerID,
                        MonsterName = e.MonsterName,
                        MonsterDescription = e.MonsterDescription,
                        MonsterType = e.MonsterType,
                        Environment = e.Environment,
                        Alignment = e.Alignment,
                        ChallengeRating = e.ChallengeRating,
                        ProficiencyBonus = e.ProficiencyBonus,
                        ExperienceGain = e.ExperienceGain,
                        HitPoints = e.HitPoints,
                        Size = e.Size,
                        Speed = e.Speed,
                        WaterSpeed = e.WaterSpeed,
                        FlySpeed = e.FlySpeed,
                        BurrowSpeed = e.BurrowSpeed,
                        ClimbSpeed = e.ClimbSpeed,
                        ArmorClass = e.ArmorClass,
                        SavingThrows = savingThrows,
                        Languages = e.Languages.Split('|').ToList(),
                        Senses = e.Senses.Split('|').ToList(),
                        Skills = monsterSkills,
                        Strength = e.Strength,
                        Dexterity = e.Dexterity,
                        Constitution = e.Constitution,
                        Intelligence = e.Intelligence,
                        Wisdom = e.Wisdom,
                        Charisma = e.Charisma,
                        StrMod = e.StrMod,
                        DexMod = e.DexMod,
                        ConMod = e.ConMod,
                        IntMod = e.IntMod,
                        WisMod = e.WisMod,
                        ChaMod = e.ChaMod,
                        DateOfCreation = e.DateOfCreation,
                        DateOfModification = e.DateOfModification
                    };
                    monsters.Add(monster);
                }
                return monsters;
            }
        }
        //get monster by ID
        public MonsterDisplay GetMonsterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var m = ctx.Monsters.Single(e => e.OwnerID == _userId && e.MonsterID == id);
                Dictionary<string, int> savingThrows = new Dictionary<string, int>();
                var save = m.SavingThrows.Split('|');
                foreach (var s in save)
                {
                    var split = s.Split(' ');

                    savingThrows.Add(split[0], int.Parse(split[1].TrimStart('+')));
                }
                List<MonsterSkillDisplay> monsterSkills = new List<MonsterSkillDisplay>();
                foreach (var skill in m.Skills)
                {
                    var skillDisplay = new MonsterSkillDisplay
                    {
                        SkillID = skill.SkillID,
                        SkillName = skill.Skills.Name,
                        SkillDescription = skill.Skills.Description,
                        StatApplied = skill.Skills.StatApplied,
                        ModStatNumber = skill.ModStat
                    };
                    monsterSkills.Add(skillDisplay);
                }
                return new MonsterDisplay
                {
                    MonsterID = m.MonsterID,
                    OwnerID = m.OwnerID,
                    MonsterName = m.MonsterName,
                    MonsterDescription = m.MonsterDescription,
                    MonsterType = m.MonsterType,
                    Environment = m.Environment,
                    Alignment = m.Alignment,
                    ChallengeRating = m.ChallengeRating,
                    ProficiencyBonus = m.ProficiencyBonus,
                    ExperienceGain = m.ExperienceGain,
                    HitPoints = m.HitPoints,
                    Size = m.Size,
                    Speed = m.Speed,
                    WaterSpeed = m.WaterSpeed,
                    FlySpeed = m.FlySpeed,
                    BurrowSpeed = m.BurrowSpeed,
                    ClimbSpeed = m.ClimbSpeed,
                    ArmorClass = m.ArmorClass,
                    SavingThrows = savingThrows,
                    Languages = m.Languages.Split('|').ToList(),
                    Senses = m.Senses.Split('|').ToList(),
                    Skills = monsterSkills,
                    Strength = m.Strength,
                    Dexterity = m.Dexterity,
                    Constitution = m.Constitution,
                    Intelligence = m.Intelligence,
                    Wisdom = m.Wisdom,
                    Charisma = m.Charisma,
                    StrMod = m.StrMod,
                    DexMod = m.DexMod,
                    ConMod = m.ConMod,
                    IntMod = m.IntMod,
                    WisMod = m.WisMod,
                    ChaMod = m.ChaMod,
                    DateOfCreation = m.DateOfCreation,
                    DateOfModification = m.DateOfModification
                };
            }
        }
        //create new monster
        public bool CreateMonster(MonsterCreate monster)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string savingThrows = "";
                foreach (var s in monster.SavingThrows)
                {
                    savingThrows += $"|{s.Key} +{s.Value}";
                }
                var newMonster = new Monster
                {
                    OwnerID = _userId,
                    MonsterName = monster.MonsterName,
                    MonsterDescription = monster.MonsterDescription,
                    MonsterType = monster.MonsterType,
                    Environment = monster.Environment,
                    Alignment = monster.Alignment,
                    ChallengeRating = monster.ChallengeRating,
                    ProficiencyBonus = MonsterProficiencyBonus(monster.ChallengeRating),
                    ExperienceGain = MonsterExperiencePoints(monster.ChallengeRating),
                    HitPoints = monster.HitPoints,
                    Size = monster.Size,
                    Speed = monster.Speed,
                    WaterSpeed = monster.WaterSpeed,
                    FlySpeed = monster.FlySpeed,
                    BurrowSpeed = monster.BurrowSpeed,
                    ClimbSpeed = monster.ClimbSpeed,
                    ArmorClass = monster.ArmorClass,
                    SavingThrows = savingThrows.Substring(1),
                    Languages = String.Join("|", monster.Languages),
                    Senses = String.Join("|", monster.Senses),
                    Strength = monster.Strength,
                    Dexterity = monster.Dexterity,
                    Constitution = monster.Constitution,
                    Intelligence = monster.Intelligence,
                    Wisdom = monster.Wisdom,
                    Charisma = monster.Charisma,
                    StrMod = StatModifier(monster.Strength),
                    DexMod = StatModifier(monster.Dexterity),
                    ConMod = StatModifier(monster.Constitution),
                    IntMod = StatModifier(monster.Intelligence),
                    WisMod = StatModifier(monster.Wisdom),
                    ChaMod = StatModifier(monster.Charisma),
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.Monsters.Add(newMonster);
                ctx.SaveChanges();
                foreach (var s in monster.SkillInfo)
                {
                    newMonster.Skills.Add(new MonsterSkills
                    {
                        MonsterID = newMonster.MonsterID,
                        SkillID = s.Key,
                        ModStat = s.Value,
                        DateOfCreation = DateTimeOffset.UtcNow
                    });
                }
                return ctx.SaveChanges() == monster.SkillInfo.Count;
            }
        }
        //update existing monster
        public bool UpdateMonster(int id, MonsterCreate monster)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var m = ctx.Monsters.Single(e => e.OwnerID == _userId && e.MonsterID == id);
                string savingThrows = "";
                foreach (var s in monster.SavingThrows)
                {
                    savingThrows += $"|{s.Key} +{s.Value}";
                }

                foreach (var skill in monster.SkillInfo)
                {
                    if (m.Skills.SingleOrDefault(e => e.SkillID == skill.Key && e.MonsterID == id) == null)
                    {
                        var newSkill = new MonsterSkills
                        {
                            MonsterID = m.MonsterID,
                            SkillID = skill.Key,
                            ModStat = skill.Value
                        };
                        m.Skills.Add(newSkill);
                    }
                }

                m.MonsterName = monster.MonsterName;
                m.MonsterDescription = monster.MonsterDescription;
                m.MonsterType = monster.MonsterType;
                m.Environment = monster.Environment;
                m.Alignment = monster.Alignment;
                m.ChallengeRating = monster.ChallengeRating;
                m.ProficiencyBonus = MonsterProficiencyBonus(monster.ChallengeRating);
                m.ExperienceGain = MonsterExperiencePoints(monster.ChallengeRating);
                m.HitPoints = monster.HitPoints;
                m.Size = monster.Size;
                m.Speed = monster.Speed;
                m.WaterSpeed = monster.WaterSpeed;
                m.FlySpeed = monster.FlySpeed;
                m.BurrowSpeed = monster.BurrowSpeed;
                m.ClimbSpeed = monster.ClimbSpeed;
                m.ArmorClass = monster.ArmorClass;
                m.SavingThrows = savingThrows.Substring(1);
                m.Languages = String.Join("|", monster.Languages);
                m.Senses = String.Join("|", monster.Senses);
                m.Strength = monster.Strength;
                m.Dexterity = monster.Dexterity;
                m.Constitution = monster.Constitution;
                m.Intelligence = monster.Intelligence;
                m.Wisdom = monster.Wisdom;
                m.Charisma = monster.Charisma;
                m.StrMod = StatModifier(monster.Strength);
                m.DexMod = StatModifier(monster.Dexterity);
                m.ConMod = StatModifier(monster.Constitution);
                m.IntMod = StatModifier(monster.Intelligence);
                m.WisMod = StatModifier(monster.Wisdom);
                m.ChaMod = StatModifier(monster.Charisma);
                m.DateOfModification = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        //delete monster
        public bool DeleteMonster(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var monsterSkills = ctx.MonsterSkills.Where(e => e.MonsterID == id).ToList();
                foreach(var m in monsterSkills)
                {
                    ctx.MonsterSkills.Remove(m);
                }
                var monstersInBattle = ctx.BattleMonsterList.Where(e => e.OwnerID == _userId && e.MonsterID == id).ToList();
                foreach(var m in monstersInBattle)
                {
                    ctx.BattleMonsterList.Remove(m);
                }
                var monster = ctx.Monsters.Single(e => e.OwnerID == _userId && e.MonsterID == id);
                ctx.Monsters.Remove(monster);

                return ctx.SaveChanges() == (1 + monsterSkills.Count + monstersInBattle.Count);
            }
        }

        private int StatModifier(double stat)
        {
            if (stat == 1) return -5;
            else if (stat == 2 || stat == 3) return -4;
            else if (stat == 4 || stat == 5) return -3;
            else if (stat == 6 || stat == 7) return -2;
            else if (stat == 8 || stat == 9) return -1;
            else if (stat == 10 || stat == 11) return 0;
            else if (stat == 12 || stat == 13) return 1;
            else if (stat == 14 || stat == 15) return 2;
            else if (stat == 16 || stat == 17) return 3;
            else if (stat == 18 || stat == 19) return 4;
            else if (stat == 20 || stat == 21) return 5;
            else if (stat == 22 || stat == 23) return 6;
            else if (stat == 24 || stat == 25) return 7;
            else if (stat == 26 || stat == 27) return 8;
            else if (stat == 28 || stat == 29) return 9;
            else if (stat == 30) return 10;
            else return 0;
        }
        private int MonsterProficiencyBonus(double cr)
        {
            if (cr == 0) return 2;
            else if (cr == 0.125) return 2;
            else if (cr == 0.25) return 2;
            else if (cr == 0.5) return 2;
            else if (cr == 1) return 2;
            else if (cr == 2) return 2;
            else if (cr == 3) return 2;
            else if (cr == 4) return 2;
            else if (cr == 5) return 3;
            else if (cr == 6) return 3;
            else if (cr == 7) return 3;
            else if (cr == 8) return 3;
            else if (cr == 9) return 4;
            else if (cr == 10) return 4;
            else if (cr == 11) return 4;
            else if (cr == 12) return 4;
            else if (cr == 13) return 5;
            else if (cr == 14) return 5;
            else if (cr == 15) return 5;
            else if (cr == 16) return 5;
            else if (cr == 17) return 6;
            else if (cr == 18) return 6;
            else if (cr == 19) return 6;
            else if (cr == 20) return 6;
            else if (cr == 21) return 7;
            else if (cr == 22) return 7;
            else if (cr == 23) return 7;
            else if (cr == 24) return 7;
            else if (cr == 25) return 8;
            else if (cr == 26) return 8;
            else if (cr == 27) return 8;
            else if (cr == 28) return 8;
            else if (cr == 29) return 9;
            else if (cr == 30) return 9;
            else return 0;
        }
        private int MonsterExperiencePoints(double cr)
        {
            if (cr == 0) return 10;
            else if (cr == 0.125) return 25;
            else if (cr == 0.25) return 50;
            else if (cr == 0.5) return 100;
            else if (cr == 1) return 200;
            else if (cr == 2) return 450;
            else if (cr == 3) return 700;
            else if (cr == 4) return 1100;
            else if (cr == 5) return 1800;
            else if (cr == 6) return 2300;
            else if (cr == 7) return 2900;
            else if (cr == 8) return 3900;
            else if (cr == 9) return 5000;
            else if (cr == 10) return 5900;
            else if (cr == 11) return 7200;
            else if (cr == 12) return 8400;
            else if (cr == 13) return 10000;
            else if (cr == 14) return 11500;
            else if (cr == 15) return 13000;
            else if (cr == 16) return 15000;
            else if (cr == 17) return 18000;
            else if (cr == 18) return 20000;
            else if (cr == 19) return 22000;
            else if (cr == 20) return 25000;
            else if (cr == 21) return 33000;
            else if (cr == 22) return 41000;
            else if (cr == 23) return 50000;
            else if (cr == 24) return 62000;
            else if (cr == 25) return 75000;
            else if (cr == 26) return 90000;
            else if (cr == 27) return 105000;
            else if (cr == 28) return 120000;
            else if (cr == 29) return 135000;
            else if (cr == 30) return 155000;
            else return 0;
        }
    }
}
