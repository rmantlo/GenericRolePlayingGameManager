using ItemHoarder.Data;
using ItemHoarder.Data.BattleFolder;
using ItemHoarder.Models.BattleInstances;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class BattleService
    {
        private readonly Guid _userId;
        public BattleService(Guid userId)
        {
            _userId = userId;
        }
        //dont need to get all instances regardless of room! too much!
        //get all battles in a room
        public IEnumerable<BattleGMDisplay> GetAllBattlesInRoom(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.BattleInstances.Where(e => e.RoomID == id).ToList();
                List<BattleGMDisplay> battles = new List<BattleGMDisplay>();
                foreach (var b in results)
                {
                    List<MonsterDisplay> bMonsters = new List<MonsterDisplay>();
                    foreach (var m in b.Monsters)
                    {
                        Dictionary<string, int> savingThrows = new Dictionary<string, int>();
                        var save = m.Monster.SavingThrows.Split('|');
                        foreach (var s in save)
                        {
                            var split = s.Split(' ');

                            savingThrows.Add(split[0], int.Parse(split[1].TrimStart('+')));
                        }
                        List<MonsterSkillDisplay> monsterSkills = new List<MonsterSkillDisplay>();
                        foreach (var skill in m.Monster.Skills)
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
                        bMonsters.Add(new MonsterDisplay
                        {
                            MonsterID = m.MonsterID,
                            OwnerID = m.OwnerID,
                            MonsterName = m.Monster.MonsterName,
                            MonsterDescription = m.Monster.MonsterDescription,
                            MonsterType = m.Monster.MonsterType,
                            Environment = m.Monster.Environment,
                            Alignment = m.Monster.Alignment,
                            ChallengeRating = m.Monster.ChallengeRating,
                            ProficiencyBonus = m.Monster.ProficiencyBonus,
                            ExperienceGain = m.Monster.ExperienceGain,
                            HitPoints = m.Monster.HitPoints,
                            Size = m.Monster.Size,
                            Speed = m.Monster.Speed,
                            WaterSpeed = m.Monster.WaterSpeed,
                            FlySpeed = m.Monster.FlySpeed,
                            BurrowSpeed = m.Monster.BurrowSpeed,
                            ClimbSpeed = m.Monster.ClimbSpeed,
                            ArmorClass = m.Monster.ArmorClass,
                            SavingThrows = savingThrows,
                            Languages = m.Monster.Languages.Split('|').ToList(),
                            Senses = m.Monster.Senses.Split('|').ToList(),
                            Skills = monsterSkills,
                            Strength = m.Monster.Strength,
                            Dexterity = m.Monster.Dexterity,
                            Constitution = m.Monster.Constitution,
                            Intelligence = m.Monster.Intelligence,
                            Wisdom = m.Monster.Wisdom,
                            Charisma = m.Monster.Charisma,
                            StrMod = m.Monster.StrMod,
                            DexMod = m.Monster.DexMod,
                            ConMod = m.Monster.ConMod,
                            IntMod = m.Monster.IntMod,
                            WisMod = m.Monster.WisMod,
                            ChaMod = m.Monster.ChaMod,
                            DateOfCreation = m.Monster.DateOfCreation,
                            DateOfModification = m.Monster.DateOfModification

                        });
                    }
                    List<ItemDisplay> bItems = new List<ItemDisplay>();
                    foreach (var m in b.ItemDrops)
                    {
                        bItems.Add(new ItemDisplay
                        {
                            ItemID = m.ItemID,
                            Name = m.Item.Name,
                            Description = m.Item.Description,
                            Weight = m.Item.Weight,
                            HitPoints = m.Item.HitPoints,
                            ItemRarity = m.Item.ItemRarity,
                            ItemClass = m.Item.ItemClass,
                            ClassType = m.Item.ClassType,
                            ArmorClass = m.Item.ArmorClass,
                            Damage = m.Item.Damage,
                            DamageResiliance = m.Item.DamageResiliance,
                            IsEquiptable = m.Item.IsEquiptable,
                            Strength = m.Item.Strength,
                            Dexterity = m.Item.Dexterity,
                            Constitution = m.Item.Constitution,
                            Intelligence = m.Item.Intelligence,
                            Wisdom = m.Item.Wisdom,
                            Charisma = m.Item.Charisma,
                            DateOfCreation = m.Item.DateOfCreation,
                            DateOfModification = m.Item.DateOfModification
                        });
                    }
                    List<ItemDisplay> bRandomItems = new List<ItemDisplay>();
                    foreach (var m in b.ItemRandomDrops)
                    {
                        bRandomItems.Add(new ItemDisplay
                        {
                            ItemID = m.ItemID,
                            Name = m.Item.Name,
                            Description = m.Item.Description,
                            Weight = m.Item.Weight,
                            HitPoints = m.Item.HitPoints,
                            ItemRarity = m.Item.ItemRarity,
                            ItemClass = m.Item.ItemClass,
                            ClassType = m.Item.ClassType,
                            ArmorClass = m.Item.ArmorClass,
                            Damage = m.Item.Damage,
                            DamageResiliance = m.Item.DamageResiliance,
                            IsEquiptable = m.Item.IsEquiptable,
                            Strength = m.Item.Strength,
                            Dexterity = m.Item.Dexterity,
                            Constitution = m.Item.Constitution,
                            Intelligence = m.Item.Intelligence,
                            Wisdom = m.Item.Wisdom,
                            Charisma = m.Item.Charisma,
                            DateOfCreation = m.Item.DateOfCreation,
                            DateOfModification = m.Item.DateOfModification
                        });
                    }

                    var battle = new BattleGMDisplay
                    {
                        BattleID = b.BattleID,
                        RoomID = b.RoomID,
                        IsCurrent = b.IsCurrent,
                        MonsterList = bMonsters,
                        ItemDropList = bItems,
                        RandomItemDropList = bRandomItems,
                        DateOfCreation = b.DateOfCreation,
                        DateOfModification = b.DateOfModification
                    };
                    battles.Add(battle);
                }
                return battles;
            }
        }
        //get battle by id
        public BattleGMDisplay GetBattleById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var b = ctx.BattleInstances.Single(e => e.BattleID == id);

                List<MonsterDisplay> bMonsters = new List<MonsterDisplay>();
                foreach (var m in b.Monsters)
                {
                    Dictionary<string, int> savingThrows = new Dictionary<string, int>();
                    var save = m.Monster.SavingThrows.Split('|');
                    foreach (var s in save)
                    {
                        var split = s.Split(' ');

                        savingThrows.Add(split[0], int.Parse(split[1].TrimStart('+')));
                    }
                    List<MonsterSkillDisplay> monsterSkills = new List<MonsterSkillDisplay>();
                    foreach (var skill in m.Monster.Skills)
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
                    bMonsters.Add(new MonsterDisplay
                    {
                        MonsterID = m.MonsterID,
                        OwnerID = m.OwnerID,
                        MonsterName = m.Monster.MonsterName,
                        MonsterDescription = m.Monster.MonsterDescription,
                        MonsterType = m.Monster.MonsterType,
                        Environment = m.Monster.Environment,
                        Alignment = m.Monster.Alignment,
                        ChallengeRating = m.Monster.ChallengeRating,
                        ProficiencyBonus = m.Monster.ProficiencyBonus,
                        ExperienceGain = m.Monster.ExperienceGain,
                        HitPoints = m.Monster.HitPoints,
                        Size = m.Monster.Size,
                        Speed = m.Monster.Speed,
                        WaterSpeed = m.Monster.WaterSpeed,
                        FlySpeed = m.Monster.FlySpeed,
                        BurrowSpeed = m.Monster.BurrowSpeed,
                        ClimbSpeed = m.Monster.ClimbSpeed,
                        ArmorClass = m.Monster.ArmorClass,
                        SavingThrows = savingThrows,
                        Languages = m.Monster.Languages.Split('|').ToList(),
                        Senses = m.Monster.Senses.Split('|').ToList(),
                        Skills = monsterSkills,
                        Strength = m.Monster.Strength,
                        Dexterity = m.Monster.Dexterity,
                        Constitution = m.Monster.Constitution,
                        Intelligence = m.Monster.Intelligence,
                        Wisdom = m.Monster.Wisdom,
                        Charisma = m.Monster.Charisma,
                        StrMod = m.Monster.StrMod,
                        DexMod = m.Monster.DexMod,
                        ConMod = m.Monster.ConMod,
                        IntMod = m.Monster.IntMod,
                        WisMod = m.Monster.WisMod,
                        ChaMod = m.Monster.ChaMod,
                        DateOfCreation = m.Monster.DateOfCreation,
                        DateOfModification = m.Monster.DateOfModification

                    });
                }
                List<ItemDisplay> bItems = new List<ItemDisplay>();
                foreach (var m in b.ItemDrops)
                {
                    bItems.Add(new ItemDisplay
                    {
                        ItemID = m.ItemID,
                        Name = m.Item.Name,
                        Description = m.Item.Description,
                        Weight = m.Item.Weight,
                        HitPoints = m.Item.HitPoints,
                        ItemRarity = m.Item.ItemRarity,
                        ItemClass = m.Item.ItemClass,
                        ClassType = m.Item.ClassType,
                        ArmorClass = m.Item.ArmorClass,
                        Damage = m.Item.Damage,
                        DamageResiliance = m.Item.DamageResiliance,
                        IsEquiptable = m.Item.IsEquiptable,
                        Strength = m.Item.Strength,
                        Dexterity = m.Item.Dexterity,
                        Constitution = m.Item.Constitution,
                        Intelligence = m.Item.Intelligence,
                        Wisdom = m.Item.Wisdom,
                        Charisma = m.Item.Charisma,
                        DateOfCreation = m.Item.DateOfCreation,
                        DateOfModification = m.Item.DateOfModification
                    });
                }
                List<ItemDisplay> bRandomItems = new List<ItemDisplay>();
                foreach (var m in b.ItemRandomDrops)
                {
                    bRandomItems.Add(new ItemDisplay
                    {
                        ItemID = m.ItemID,
                        Name = m.Item.Name,
                        Description = m.Item.Description,
                        Weight = m.Item.Weight,
                        HitPoints = m.Item.HitPoints,
                        ItemRarity = m.Item.ItemRarity,
                        ItemClass = m.Item.ItemClass,
                        ClassType = m.Item.ClassType,
                        ArmorClass = m.Item.ArmorClass,
                        Damage = m.Item.Damage,
                        DamageResiliance = m.Item.DamageResiliance,
                        IsEquiptable = m.Item.IsEquiptable,
                        Strength = m.Item.Strength,
                        Dexterity = m.Item.Dexterity,
                        Constitution = m.Item.Constitution,
                        Intelligence = m.Item.Intelligence,
                        Wisdom = m.Item.Wisdom,
                        Charisma = m.Item.Charisma,
                        DateOfCreation = m.Item.DateOfCreation,
                        DateOfModification = m.Item.DateOfModification
                    });
                }

                var battle = new BattleGMDisplay
                {
                    BattleID = b.BattleID,
                    RoomID = b.RoomID,
                    IsCurrent = b.IsCurrent,
                    MonsterList = bMonsters,
                    ItemDropList = bItems,
                    RandomItemDropList = bRandomItems,
                    DateOfCreation = b.DateOfCreation,
                    DateOfModification = b.DateOfModification
                };
                return battle
            }
        }
        //create battle
        public bool CreateBattleInstance(int roomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.BattleInstances.Add(new BattleInstance
                {
                    RoomID = roomId
                });
                return ctx.SaveChanges() == 1;
            }
        }
        //make a battle instance the current battle instance
        public bool MakeBattleCurrent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                battle.IsCurrent = true;
                var oldBattle = ctx.BattleInstances.SingleOrDefault(e => e.RoomID == battle.RoomID && e.IsCurrent == true);
                if (oldBattle != null)
                {
                    oldBattle.IsCurrent = false;
                    return ctx.SaveChanges() == 2;
                }
                else
                {
                    return ctx.SaveChanges() == 1;
                }
            }
        }
        //add monster to battle
        public bool AddMonstersToBattle(int id, List<int> monsterIds)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                foreach (var i in monsterIds)
                {
                    if (ctx.Monsters.SingleOrDefault(e => e.MonsterID == i) == null)
                    {
                        return false;
                    }
                    else
                    {
                        battle.Monsters.Add(new BattleMonsters
                        {
                            OwnerID = _userId,
                            BattleID = battle.BattleID,
                            MonsterID = i,
                            DateOfCreation = DateTimeOffset.UtcNow
                        });
                    }
                }
                return ctx.SaveChanges() >= 1;
            }
        }
        //remove monster from battle
        public bool RemoveMonsterFromBattle(int id, int monsterId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                var monster = ctx.BattleMonsterList.First(e => e.MonsterID == monsterId);
                if (battle.Monsters.Contains(monster))
                {
                    ctx.BattleMonsterList.Remove(monster);
                }

                return ctx.SaveChanges() == 1;
            }
        }
        //add mandatory item to battle
        public bool AddItemsToBattle(int id, List<int> itemIds)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                foreach (var i in itemIds)
                {
                    if (ctx.Items.SingleOrDefault(e => e.ItemID == i) == null)
                    {
                        return false;
                    }
                    else
                    {
                        battle.ItemDrops.Add(new BattleItem
                        {
                            OwnerID = _userId,
                            BattleID = battle.BattleID,
                            ItemID = i,
                            DateOfCreation = DateTimeOffset.UtcNow
                        });
                    }
                }
                return ctx.SaveChanges() >= 1;
            }
        }
        //remove mandatory item to battle
        public bool RemoveItemFromBattle(int id, int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                var item = ctx.BattleItemList.First(e => e.ItemID == itemId);
                if (battle.ItemDrops.Contains(item))
                {
                    ctx.BattleItemList.Remove(item);
                }
                return ctx.SaveChanges() == 1;
            }
        }
        //add item to randomitems in battle
        public bool AddItemsToRandomListInBattle(int id, List<int> itemIds)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                foreach (var i in itemIds)
                {
                    if (ctx.Items.SingleOrDefault(e => e.ItemID == i) == null)
                    {
                        return false;
                    }
                    else
                    {
                        battle.ItemRandomDrops.Add(new BattleRandomItem
                        {
                            OwnerID = _userId,
                            BattleID = battle.BattleID,
                            ItemID = i,
                            DateOfCreation = DateTimeOffset.UtcNow
                        });
                    }
                }
                return ctx.SaveChanges() >= 1;
            }
        }
        //remove mandatory item from randomitems in battle
        public bool RemoveItemFromRandomFromBattle(int id, int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                var item = ctx.BattleRandomItemList.First(e => e.ItemID == itemId);
                if (battle.ItemRandomDrops.Contains(item))
                {
                    ctx.BattleRandomItemList.Remove(item);
                }
                return ctx.SaveChanges() == 1;
            }
        }
        //delete battle instance
        public bool DeleteBattleInstance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var battle = ctx.BattleInstances.Single(e => e.BattleID == id);
                var bMonsters = ctx.BattleMonsterList.Where(e => e.BattleID == id).ToList();
                foreach (var b in bMonsters)
                {
                    ctx.BattleMonsterList.Remove(b);
                }
                var bItems = ctx.BattleItemList.Where(e => e.BattleID == id).ToList();
                foreach (var b in bItems)
                {
                    ctx.BattleItemList.Remove(b);
                }
                var bRandomItems = ctx.BattleRandomItemList.Where(e => e.BattleID == id).ToList();
                foreach (var b in bRandomItems)
                {
                    ctx.BattleRandomItemList.Remove(b);
                }
                return ctx.SaveChanges() == (1 + bMonsters.Count + bItems.Count + bRandomItems.Count);
            }
        }
    }
}
