using ItemHoarder.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers
{
    [Authorize]
    public class BattleController : ApiController
    {
        ///// <summary>
        ///// Get all battle instances in a room (meant for GM view)
        ///// </summary>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //public IHttpActionResult GetAll(int roomId)
        //{
        //    var service = CreateBattleService();
        //    return Ok(service.GetAllBattlesInRoom(roomId));
        //}
        ///// <summary>
        ///// Get battle instance by Id (meant for GM)
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IHttpActionResult Get(int id)
        //{
        //    var service = CreateBattleService();
        //    return Ok(service.GetBattleById(id));
        //}
        ///// <summary>
        ///// Create new battle instance in room I own
        ///// </summary>
        ///// <param name="roomId"></param>
        ///// <returns></returns>
        //public IHttpActionResult Post(int roomId)
        //{
        //    var service = CreateBattleService();
        //    if (service.CreateBattleInstance(roomId)) return Ok();
        //    else return BadRequest("Battle instance not created");
        //}
        ///// <summary>
        ///// change which battle is current
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IHttpActionResult Put(int id)
        //{
        //    var service = CreateBattleService();
        //    if (service.MakeBattleCurrent(id)) return Ok();
        //    else return BadRequest("Battle was not made current");
        //}
        ///// <summary>
        ///// Add monster to battle instance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="monsterIds"></param>
        ///// <returns></returns>
        //public IHttpActionResult PutMonster(int id, List<int> monsterIds)
        //{
        //    var service = CreateBattleService();
        //    if (service.AddMonstersToBattle(id, monsterIds)) return Ok();
        //    else return BadRequest("Monster not added to battle");
        //}
        ///// <summary>
        ///// Add item to battle instance in list of items that WILL drop
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="itemIds"></param>
        ///// <returns></returns>
        //public IHttpActionResult PutItem(int id, List<int> itemIds)
        //{
        //    var service = CreateBattleService();
        //    if (service.AddItemsToBattle(id, itemIds)) return Ok();
        //    else return BadRequest("Items not added to battle");
        //}
        ///// <summary>
        ///// Add item to battle instance in list of items that drop by random chance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="itemIds"></param>
        ///// <returns></returns>
        //public IHttpActionResult PutItemInRandom(int id, List<int> itemIds)
        //{
        //    var service = CreateBattleService();
        //    if (service.AddItemsToRandomListInBattle(id, itemIds)) return Ok();
        //    else return BadRequest("Items not added to battle random list");
        //}
        ///// <summary>
        ///// remove monster from battle instance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="monsterId"></param>
        ///// <returns></returns>
        //public IHttpActionResult DeleteMonster(int id, int monsterId)
        //{
        //    var service = CreateBattleService();
        //    if (service.RemoveMonsterFromBattle(id, monsterId)) return Ok();
        //    else return BadRequest("Monster not removed from battle");
        //}
        ///// <summary>
        ///// remove item from WILL drop item list in battle instance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="itemId"></param>
        ///// <returns></returns>
        //public IHttpActionResult DeleteItem(int id, int itemId)
        //{
        //    var service = CreateBattleService();
        //    if (service.RemoveItemFromBattle(id, itemId)) return Ok();
        //    else return BadRequest("Item not removed from battle");
        //}
        ///// <summary>
        ///// remove item from Random drop item list in battle instance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="itemId"></param>
        ///// <returns></returns>
        //public IHttpActionResult DeleteItemInRandom(int id, int itemId)
        //{
        //    var service = CreateBattleService();
        //    if (service.RemoveItemFromRandomFromBattle(id, itemId)) return Ok();
        //    else return BadRequest("Items not removed from battle random list");
        //}
        ///// <summary>
        ///// delete existing battle instance
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IHttpActionResult Delete(int id)
        //{
        //    var service = CreateBattleService();
        //    if (service.DeleteBattleInstance(id)) return Ok();
        //    else return BadRequest("Battle instance not deleted");
        //}

        //private BattleService CreateBattleService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    return new BattleService(userId);
        //}
    }
}
