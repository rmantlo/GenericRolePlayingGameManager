using ItemHoarder.Data;
using ItemHoarder.Data.ItemStuff;
using ItemHoarder.Models.ItemInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class ItemService
    {
        private readonly Guid _userId;
        public ItemService(Guid userId)
        {
            _userId = userId;
        }
        //Get item index
        public IEnumerable<ItemIndex> GetAllGMItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Items.Where(e => e.OwnerID == _userId).Select(e => new ItemIndex
                {
                    ItemID = e.ItemID,
                    Name = e.Name,
                    ItemRarity = e.ItemRarity,
                    ItemClass = e.ItemClass,
                    ItemPhoto = e.ItemPhoto.ToList()[0]
                }).ToList();
            }
        }
        //get item index of items by character ID
        public IEnumerable<ItemIndex> GetAllOfMyCharacterItems(int charID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.CharacterInstances.Single(e => e.CharInstanceID == charID).InventoryItems.Select(e => new ItemIndex
                {
                    ItemID = e.ItemID,
                    Name = e.OriginalItem.Name,
                    ItemRarity = e.OriginalItem.ItemRarity,
                    ItemClass = e.OriginalItem.ItemClass,
                    IsEquipted = e.IsEquipted,
                    ItemPhoto = e.OriginalItem.ItemPhoto.ToList()[0]
                }).ToList();
            }
        }
        //get GM Item Details by id
        public GMItemDetails GetGMItemById(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.Single(e => e.OwnerID == _userId && e.ItemID == Id);
                return new GMItemDetails
                {
                    ItemID = item.ItemID,
                    Name = item.Name,
                    ItemPhoto = item.ItemPhoto.ToList()[0],
                    Description = item.Description,
                    Weight = item.Weight,
                    HitPoints = item.HitPoints.Split('|').Select(int.Parse).ToList(),
                    ItemRarity = item.ItemRarity,
                    ItemClass = item.ItemClass,
                    ClassType = item.ClassType,
                    ArmorClass = item.ArmorClass,
                    Damage = item.Damage,
                    DamageResiliance = item.DamageResiliance,
                    IsEquiptable = item.IsEquiptable,
                    Strength = item.Strength,
                    Dexterity = item.Dexterity,
                    Constitution = item.Constitution,
                    Intelligence = item.Intelligence,
                    Wisdom = item.Wisdom,
                    Charisma = item.Charisma,
                    DateOfCreation = item.DateOfCreation,
                    DateOfModification = item.DateOfModification
                };
            }
        }
        //get items details of character's item as player or GM
        public PlayerItemDetails GetCharacterItemById(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.InventoryItems.Single(e => (e.OwnerID == _userId || e.OriginalItem.OwnerID == _userId) && e.ItemID == Id);
                return new PlayerItemDetails
                {
                    ItemID = item.ItemID,
                    Name = item.OriginalItem.Name,
                    Description = item.OriginalItem.Description,
                    ItemPhoto = item.OriginalItem.ItemPhoto.ToList()[0],
                    HitPoints = item.ActualHitPoints,
                    CurrentHitPoints = item.CurrentHitPoints,
                    IsEquipted = item.IsEquipted,
                    IsEquiptable = item.OriginalItem.IsEquiptable,
                    Weight = item.OriginalItem.Weight,
                    ItemRarity = item.OriginalItem.ItemRarity,
                    ItemClass = item.OriginalItem.ItemClass,
                    ClassType = item.OriginalItem.ClassType,
                    ArmorClass = item.OriginalItem.ArmorClass,
                    Damage = item.OriginalItem.Damage,
                    DamageResiliance = item.OriginalItem.DamageResiliance,
                    Strength = item.OriginalItem.Strength,
                    Dexterity = item.OriginalItem.Dexterity,
                    Constitution = item.OriginalItem.Constitution,
                    Intelligence = item.OriginalItem.Intelligence,
                    Wisdom = item.OriginalItem.Wisdom,
                    Charisma = item.OriginalItem.Charisma,
                    DateOfCreation = item.DateOfCreation,
                    DateOfModification = item.DateOfModification
                };
            }
        }
        //Create new Item 
        public bool CreateItem(GMItemCreate newItem)
        {
            List<Photo> itemPhotoList = new List<Photo>();
            if (newItem.Upload != null && newItem.Upload.ContentLength > 0)
            {
                var itemPhoto = new Photo
                {
                    PhotoName = System.IO.Path.GetFileName(newItem.Upload.FileName),
                    FileType = FileType.Item,
                    ContentType = newItem.Upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(newItem.Upload.InputStream))
                {
                    itemPhoto.Content = reader.ReadBytes(newItem.Upload.ContentLength);
                }
                itemPhotoList.Add(itemPhoto);
            }
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(new Item
                {
                    OwnerID = _userId,
                    DateOfCreation = DateTimeOffset.UtcNow,
                    Name = newItem.Name,
                    ItemPhoto = itemPhotoList,
                    Weight = newItem.Weight,
                    HitPoints = newItem.FragileHitPoint.ToString() + "|" + newItem.ResilientHitPoint.ToString(),
                    ItemRarity = newItem.ItemRarity,
                    ItemClass = newItem.ItemClass,
                    ClassType = newItem.ClassType,
                    ArmorClass = newItem.ArmorClass,
                    Damage = newItem.Damage,
                    DamageResiliance = newItem.DamageResiliance,
                    IsEquiptable = newItem.IsEquiptable,
                    Strength = newItem.Strength,
                    Dexterity = newItem.Dexterity,
                    Constitution = newItem.Constitution,
                    Intelligence = newItem.Intelligence,
                    Wisdom = newItem.Wisdom,
                    Charisma = newItem.Charisma
                });
                return ctx.SaveChanges() == 1;

            }
        }
        //edit item you own
        public bool EditItem(GMItemEdit edit)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == edit.ItemID);
                List<Photo> itemPhotoList = new List<Photo>();
                if (edit.Upload != null && edit.Upload.ContentLength > 0)
                {
                    var itemPhoto = new Photo
                    {
                        PhotoName = System.IO.Path.GetFileName(edit.Upload.FileName),
                        FileType = FileType.Item,
                        ContentType = edit.Upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(edit.Upload.InputStream))
                    {
                        itemPhoto.Content = reader.ReadBytes(edit.Upload.ContentLength);
                    }
                    itemPhotoList.Add(itemPhoto);
                    item.ItemPhoto = itemPhotoList;
                }
                item.Name = edit.Name;
                item.DateOfModification = DateTimeOffset.UtcNow;
                item.Description = edit.Description;
                item.Weight = edit.Weight;
                item.HitPoints = $"{edit.FragileHitPoint}|{edit.ResilientHitPoint}";
                item.ItemRarity = edit.ItemRarity;
                item.ItemClass = edit.ItemClass;
                item.ClassType = edit.ClassType;
                item.ArmorClass = edit.ArmorClass;
                item.Damage = edit.Damage;
                item.DamageResiliance = edit.DamageResiliance;
                item.IsEquiptable = edit.IsEquiptable;
                item.Strength = edit.Strength;
                item.Dexterity = edit.Dexterity;
                item.Constitution = edit.Constitution;
                item.Intelligence = edit.Intelligence;
                item.Wisdom = edit.Wisdom;
                item.Charisma = edit.Charisma;

                return ctx.SaveChanges() == 1; //with new photo does this change?
            }
        }
        //edit item on character as player or GM (change current hitpoints and equipted status)
        public bool EditCharacterItem(PlayerItemEdit edit)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.InventoryItems.SingleOrDefault(e => e.ItemID == edit.ItemInstanceID);
                if (item.OwnerID == _userId || item.OriginalItem.OwnerID == _userId)
                {
                    item.IsEquipted = edit.IsEquipted;
                    item.CurrentHitPoints = edit.CurrentHitPoints;
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }

        //as GM give item to player char (using itemID, CharacterInstanceID?)
        public bool GiveItemToPlayer(int Id, int charID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == Id);
                if (item != null && IsCharacterInMyRoom(charID) == true)
                {
                    var hitpoints = item.HitPoints.Split('|').Select(int.Parse).ToList();
                    Random rnd = new Random();
                    int hitpoint = rnd.Next(hitpoints[0], hitpoints[1] + 1);
                    var character = ctx.CharacterInstances.SingleOrDefault(e => e.CharInstanceID == charID);
                    character.InventoryItems.Add(new InventoryItem
                    {
                        OwnerID = character.OwnerID,
                        OriginalItemID = Id,
                        ActualHitPoints = hitpoint,
                        CurrentHitPoints = hitpoint,
                        IsEquipted = false,
                        DateOfCreation = DateTimeOffset.UtcNow
                    });
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //as GM or Player take item from player char (using instanceitemID and charinstanceID)
        public bool RemoveItemFromPlayerCharacter(int Id, int charID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var character = ctx.CharacterInstances.SingleOrDefault(e => e.CharInstanceID == charID);
                if (IsCharacterInMyRoom(charID) == true || character.OwnerID == _userId)
                {
                    var instanceItem = ctx.InventoryItems.SingleOrDefault(e => e.OwnerID == character.OwnerID && e.ItemID == Id);
                    character.InventoryItems.Remove(instanceItem);
                    return ctx.SaveChanges() == 1;
                }
                else return false;
            }
        }
        //Delete item using itemID
        public bool DeleteItem(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var item = ctx.Items.SingleOrDefault(e => e.OwnerID == _userId && e.ItemID == Id);
                if (item != null)
                {
                    var inventoryItems = ctx.InventoryItems.Where(e => e.OriginalItemID == Id).ToList();
                    foreach (var i in inventoryItems)
                    {
                        ctx.InventoryItems.Remove(i);
                    }
                    ctx.Items.Remove(item);
                    return ctx.SaveChanges() == 1 + inventoryItems.Count; //check if correct
                }
                else return false;
            }
        }

        //Safety returns bool if character instance is in a room owned by user
        //this prevents people from giving their own chars extra items, items to wrong chars, or items to chars in a room a GM doesn't own
        private bool IsCharacterInMyRoom(int charID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var character = ctx.CharacterInstances.SingleOrDefault(e => e.CharInstanceID == charID);
                var room = ctx.Rooms.SingleOrDefault(e => e.RoomID == character.RoomID && e.OwnerID == _userId);
                if (room != null)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}
