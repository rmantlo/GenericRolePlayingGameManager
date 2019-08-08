using ItemHoarder.Data;
using ItemHoarder.Data.ItemStuff;
using ItemHoarder.Models;
using ItemHoarder.Models.ItemInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service.Items
{
    public class ItemService
    {
        private readonly Guid _userId;
        public ItemService(Guid userId)
        {
            _userId = userId;
        }
        //dont need to add items to rooms
        //get all my items
        public IEnumerable<ItemDisplay> GetAllItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var results = ctx.Items.Where(e => e.OwnerID == _userId).Select(e => new ItemDisplay
                {
                    ItemID = e.ItemID,
                    Name = e.Name,
                    Description = e.Description,
                    Weight = e.Weight,
                    HitPoints = e.HitPoints,
                    ItemRarity = e.ItemRarity,
                    ItemClass = e.ItemClass,
                    ClassType = e.ClassType,
                    Damage = e.Damage,
                    DamageResiliance = e.DamageResiliance,
                    IsEquiptable = e.IsEquiptable,
                    Strength = e.Strength,
                    Dexterity = e.Dexterity,
                    Constitution = e.Constitution,
                    Intelligence = e.Intelligence,
                    Wisdom = e.Wisdom,
                    Charisma = e.Charisma
                }).OrderBy(e => e.Name).ToArray();
                return results;
            }
        }
        //get item by ID
        public ItemDisplay GetItemById(int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == itemId);
                var itemDisplay = new ItemDisplay
                {
                    ItemID = item.ItemID,
                    Name = item.Name,
                    Description = item.Description,
                    Weight = item.Weight,
                    HitPoints = item.HitPoints,
                    ItemRarity = item.ItemRarity,
                    ItemClass = item.ItemClass,
                    ClassType = item.ClassType,
                    Damage = item.Damage,
                    DamageResiliance = item.DamageResiliance,
                    IsEquiptable = item.IsEquiptable,
                    Strength = item.Strength,
                    Dexterity = item.Dexterity,
                    Constitution = item.Constitution,
                    Intelligence = item.Intelligence,
                    Wisdom = item.Wisdom,
                    Charisma = item.Charisma
                };
                return itemDisplay;
            }
        }
        //create item
        public bool CreateItem(ItemCreate item)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var newItem = new Item
                {
                    OwnerID = _userId,
                    Name = item.Name,
                    Description = item.Description,
                    Weight = item.Weight,
                    HitPoints = item.HitPoints,
                    ItemRarity = item.ItemRarity,
                    ItemClass = item.ItemClass,
                    ClassType = item.ClassType,
                    Damage = item.Damage,
                    DamageResiliance = item.DamageResiliance,
                    IsEquiptable = item.IsEquiptable,
                    Strength = item.Strength,
                    Dexterity = item.Dexterity,
                    Constitution = item.Constitution,
                    Intelligence = item.Intelligence,
                    Wisdom = item.Wisdom,
                    Charisma = item.Charisma,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                ctx.Items.Add(newItem);
                return ctx.SaveChanges() == 1;
            }
        }
        //update Item
        public bool UpdateItem(int itemId, ItemCreate item)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var result = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == itemId);
                result.Name = item.Name;
                result.Description = item.Description;
                result.Weight = item.Weight;
                result.HitPoints = item.HitPoints;
                result.ItemRarity = item.ItemRarity;
                result.ItemClass = item.ItemClass;
                result.ClassType = item.ClassType;
                result.Damage = item.Damage;
                result.DamageResiliance = item.DamageResiliance;
                result.IsEquiptable = item.IsEquiptable;
                result.Strength = item.Strength;
                result.Dexterity = item.Dexterity;
                result.Constitution = item.Constitution;
                result.Intelligence = item.Intelligence;
                result.Wisdom = item.Wisdom;
                result.Charisma = item.Charisma;
                result.DateOfModification = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        //Delete item
        public bool DeleteItem(int itemId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == itemId);
                ctx.Items.Remove(item);
                return ctx.SaveChanges() == 1;
            }
        }

        //Give item to instanced char in my room
        public bool GiveItemToPlayer(int itemId, int charId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == itemId);
                var hitpoints = item.HitPoints.Split('|');
                var randomNum = RandomNumber(int.Parse(hitpoints[0]), int.Parse(hitpoints[1]));
                var itemCopy = new InventoryItem
                {
                    OriginalItemID = item.ItemID,
                    CreatorID = item.OwnerID,
                    Name = item.Name,
                    Description = item.Description,
                    Weight = item.Weight,
                    HitPoints = randomNum,
                    ItemRarity = item.ItemRarity,
                    ItemClass = item.ItemClass,
                    ClassType = item.ClassType,
                    Damage = item.Damage,
                    DamageResiliance = item.DamageResiliance,
                    IsEquiptable = item.IsEquiptable,
                    IsEquipted = false,
                    Strength = item.Strength,
                    Dexterity = item.Dexterity,
                    Constitution = item.Constitution,
                    Intelligence = item.Intelligence,
                    Wisdom = item.Wisdom,
                    Charisma = item.Charisma,
                    DateOfCreation = DateTimeOffset.UtcNow
                };
                var charInventory = ctx.CharacterInstances.Single(e => e.CharInstanceID == charId);
                charInventory.InventoryItems.Add(itemCopy);
                return ctx.SaveChanges() == 2;
            }
        }
        //remove item from instanced char in my room
        public bool RemoveItemFromPlayer(int itemId, int charId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var charInventory = ctx.CharacterInstances.Single(e => e.CharInstanceID == charId);
                var item = charInventory.InventoryItems.Single(e => e.ItemID == itemId && e.CreatorID == _userId);
                charInventory.InventoryItems.Remove(item);
                return ctx.SaveChanges() == 1;
            }
        }
        //random number generator
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
