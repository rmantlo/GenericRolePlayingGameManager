using ItemHoarder.Models.Characters.Instanced;
using ItemHoarder.Service.Characters;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItemHoarder.WebApi.Controllers.Characters
{
    [Authorize]
    [RoutePrefix("api/instance-character")]
    public class InstanceCharacterController : ApiController
    {
        ///// <summary>
        ///// Get all owned character instances
        ///// </summary>
        ///// <returns></returns>
        //[Route("")]
        //public IHttpActionResult GetAll()
        //{
        //    var service = CreateInstanceService();
        //    return Ok(service.GetAllMyInstancedCharacters());
        //}
        ///// <summary>
        ///// Get instanced character information by id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Route("details/{id}")]
        //public IHttpActionResult Get(int id)
        //{
        //    var service = CreateInstanceService();
        //    return Ok(service.GetInstancedCharacterById(id));
        //}
        ///// <summary>
        ///// Update existing character's details
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="update"></param>
        ///// <returns></returns>
        //[Route("update/{id}")]
        //public IHttpActionResult Put(int id, InstanceUpdate update)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    var service = CreateInstanceService();
        //    if (service.UpdateInstancedCharacter(id, update)) return Ok();
        //    else return BadRequest("Character not updated");
        //}
        //[HttpPut]
        //[Route("add-feature/{id}")]
        //public IHttpActionResult Put(int id, int featId)
        //{
        //    var service = CreateInstanceService();
        //    if (service.AddCharacterFeature(id, featId)) return Ok();
        //    else return BadRequest("Feature not added to character");
        //}
        //[HttpPut]
        //[Route("add-skill/{id}")]
        //public IHttpActionResult PutSkill(int id, int skillId)
        //{
        //    var service = CreateInstanceService();
        //    if (service.AddCharacterSkill(id, skillId)) return Ok();
        //    else return BadRequest("Feature not added to character");
        //}
        //[HttpDelete]
        //[Route("remove-feature/{id}")]
        //public IHttpActionResult Delete(int id, int featId)
        //{
        //    var service = CreateInstanceService();
        //    if (service.RemoveCharacterFeature(id, featId)) return Ok();
        //    else return BadRequest("Feature not removed from character");
        //}
        //[HttpDelete]
        //[Route("remove-skill/{id}")]
        //public IHttpActionResult DeleteSkill(int id, int skillId)
        //{
        //    var service = CreateInstanceService();
        //    if (service.RemoveCharacterSkill(id, skillId)) return Ok();
        //    else return BadRequest("Skill not removed from character");
        //}
        ///// <summary>
        ///// Delete instanced character
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[Route("delete/{id}")]
        //public IHttpActionResult Delete(int id)
        //{
        //    var service = CreateInstanceService();
        //    if (service.DeleteInstancedCharacter(id)) return Ok();
        //    else return BadRequest("Character or other connected table not deleted");
        //}

        //private CharacterInstancedService CreateInstanceService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    return new CharacterInstancedService(userId);
        //}
    }
}
