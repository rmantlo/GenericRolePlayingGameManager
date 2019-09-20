using ItemHoarder.Models;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers.Items
{
    [Authorize]
    public class ItemController : ApiController
    {
        /// <summary>
        /// Receive all items created by user in minimal details
        /// </summary>
        public IHttpActionResult GetAll()
        {
            var service = CreateItemService();
            return Ok(service.GetAllGMItems());
        }
        /// <summary>
        /// Receive all items connected to a specific character. both GM and Character owner can call this endpoint, other characters cannot see other characters items
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllItemsForCharacter(int characterID)
        {
            var service = CreateItemService();
            return Ok(service.GetAllOfMyCharacterItems(characterID));
        }
        /// <summary>
        /// Gets item details of owned items using an item id.
        /// </summary>
        /// <param name="id"></param>
        public IHttpActionResult GetMyItemDetails(int id)
        {
            var service = CreateItemService();
            return Ok(service.GetGMItemById(id));
        }
        /// <summary>
        /// Gets item details of items assigned to a character using the inventory item's ID. This ID is different than GM item ID. both GM and owner of the character can retrieve this.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetCharacterItemDetails(int id)
        {
            var service = CreateItemService();
            return Ok(service.GetCharacterItemById(id));
        }
        /// <summary>
        /// Create new item, add new item to database
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public IHttpActionResult Post(GMItemCreate newItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateItemService();
            if (service.CreateItem(newItem)) return Ok();
            else return BadRequest("Item not created");
        }
        /// <summary>
        /// Update existing item, using item ID in model of item with changes
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IHttpActionResult Put(GMItemEdit item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateItemService();
            if (service.EditItem(item)) return Ok();
            else return BadRequest("Item not updated");
        }
        /// <summary>
        /// Update existing item instance on character as GM or character owner. This does not update Item itself, but the instanceItem values, such as current hitpoints and IsEquipted boolean.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IHttpActionResult PutCharacterItem(PlayerItemEdit item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateItemService();
            if (service.EditCharacterItem(item)) return Ok();
            else return BadRequest("Item not updated");
        }
        /// <summary>
        /// Give existing item to instanced character inventory
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IHttpActionResult Post(int itemId, int characterId)
        {
            var service = CreateItemService();
            if (service.GiveItemToPlayer(itemId, characterId)) return Ok();
            else return BadRequest("Item not given to player");
        }

        /// <summary>
        /// Remove item from instanced character's inventory as either GM or character owner
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int itemId, int characterId)
        {
            var service = CreateItemService();
            if (service.RemoveItemFromPlayerCharacter(itemId, characterId)) return Ok();
            else return BadRequest("Item not removed from player");
        }
        /// <summary>
        /// Removed existing item from user's items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateItemService();
            if (service.DeleteItem(id)) return Ok();
            else return BadRequest("Item not deleted");
        }

        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ItemService(userId);
        }
    }
}
