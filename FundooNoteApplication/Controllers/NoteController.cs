using BusinessModel.Interface;
using CommonModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;

        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

        [Authorize]
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(Notes createnote)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var userdata = noteBL.CreatNoteUser(email, createnote);
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

    }
}

