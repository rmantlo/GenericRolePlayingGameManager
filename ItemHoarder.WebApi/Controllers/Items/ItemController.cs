using ItemHoarder.Models;
using ItemHoarder.Models.ItemInventory;
using ItemHoarder.Service.Items;
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
        /// Receive all items created by user
        /// </summary>
        public IHttpActionResult GetAll()
        {
            var service = CreateItemService();
            return Ok(service.GetAllItems());
        }

        /// <summary>
        /// Gets item by item id
        /// </summary>
        /// <param name="id"></param>
        public IHttpActionResult Get(int id)
        {
            var service = CreateItemService();
            return Ok(service.GetItemById(id));
        }
        //create item
        /// <summary>
        /// Create new item, add new item to database
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        public IHttpActionResult Post(ItemCreate newItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateItemService();
            if (service.CreateItem(newItem)) return Ok();
            else return BadRequest("Item not created");
        }
        //update item
        /// <summary>
        /// Update existing item, using item ID and model of item with changes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, ItemCreate item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateItemService();
            if (service.UpdateItem(id, item)) return Ok();
            else return BadRequest("Item not updated");
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
        /// Remove item from instanced character's inventory
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int itemId, int characterId)
        {
            var service = CreateItemService();
            if (service.RemoveItemFromPlayer(itemId, characterId)) return Ok();
            else return BadRequest("Item not removed from player");
        }


        private ItemService CreateItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ItemService(userId);
        }
    }
}
