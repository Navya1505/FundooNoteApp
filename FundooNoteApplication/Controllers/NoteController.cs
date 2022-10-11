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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using RepositoryModel.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        [HttpPut("PinNotes")]
        public IActionResult PinNotes(long NoteId)
        {
            try
            {
                long userId=Convert.ToInt32(User.Claims.FirstOrDefault(e=>e.Type == "UserId").Value);
                var userData = noteBL.PinNotes(NoteId);
                if (userData == true)
                    return this.Ok(new { sucess = true, message = "IsPinned ", data = userData });
                else if (userData== false)
                    return this.Ok(new { success = true, message = "UnPinned successfully", data = userData });
                else
                    return this.BadRequest(new { sucess = false, meassage = " Pinned Failed" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPut("Archieve")]
        public IActionResult Isarcheive(long NoteId)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userData = noteBL.Isarcheive(userId, NoteId);
                if (userData == true)
                    return this.Ok(new { sucess = true, message = "ArcheiveNotes ", data = userData });
                else if(userData==false)
                    return this.Ok(new {success=true,message="UnArcheivedNotes", data=userData});
                    return this.BadRequest(new { sucess = false, meassage = "Archeived Failed" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPut("TrashBin")]
        public IActionResult IsTrash(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userData = noteBL.IsTrash(userId, NoteId);
                if (userData== true)
                    return this.Ok(new { sucess = true, message = "Trash ", data = userData });

                else if(userData==false)
                    return this.Ok(new {sucess=true,message="Not Trashed", data=userData});

                    return this.BadRequest(new { sucess = false, meassage = "TrashFailed" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("color")]
        public IActionResult UpdateColor(long NoteId,string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userData = noteBL.UpdateNoteColor(userId,NoteId,color);
                if (userData!= null)
                    return this.Ok(new { sucess = true, message = "UpdateNoteColour successfull", data = userData });

                else

                return this.BadRequest(new { sucess = false, meassage = "UpdateNoteColour Unsuccessfull" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPost("ImageUpload")]

        public IActionResult Image(long noteId, IFormFile file)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var userdata = noteBL.Image(userId, noteId, file);
                if (userdata.Trash == true)
                    return this.Ok(new { success = true, message = "Image uploaded successfully", data = userdata });
                else if (userdata.Trash == false)
                    return this.Ok(new { success = true, message = "Image not uploaded", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Image upload failed" });


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
