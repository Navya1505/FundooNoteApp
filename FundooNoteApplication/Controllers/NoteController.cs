using BusinessModel.Interface;
using CommonModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Linq;
using BusinessModel.Services;
using RepositoryModel.Services;
using System.Runtime.CompilerServices;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;

        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }


        [HttpPost("CreateNote")]
        public IActionResult CreateNote(Notes createnote)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var userdata = noteBL.CreateNoteUser(userId, createnote);
                if (userdata != null)
                    return this.Ok(new { success = true, message = "Note created Successfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not able to create note" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost("Getnote")]
        public IActionResult GetNote()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var userdata = noteBL.GetNotes(userId);
                if (userdata != null)
                    return this.Ok(new { success = true, message = "Note created Successfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not able to create note" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(long NoteId,Notes updateNote )
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = noteBL.UpdateNotes(userId, NoteId, updateNote);
                if (userdata == true)
                    return this.Ok(new { success = true, message = "Note updated Successfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not not updated" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = noteBL.DeleteNotes(userId, NoteId);
                if (userdata == true)
                    return this.Ok(new { success = true, message = "deleted sucessfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not deleted" });

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpPost("PinNotes")]
        public IActionResult PinNotes(long NoteId)
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type == "UserId").Value);
                var userData = noteBL.PinNotes(userId, NoteId);
                if (userData == true)
                    return this.Ok(new { sucess = true, message = "IsPinned ", data = userData });
                else
                    return this.BadRequest(new { sucess = false, meassage = "Not Pinned" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
    


