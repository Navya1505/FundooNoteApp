using BusinessModel.Interface;
using BusinessModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborateController : ControllerBase
    {
        private readonly ICollabBL collabBL;

        public CollaborateController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("AddCollaborate")]
        public IActionResult Collab(long noteId, string receiver_email)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = collabBL.AddCollaborate(userId, noteId, receiver_email);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Collaborated Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " collaborate UnSuccessfull" });
        }

        [Authorize]
        [HttpGet("GetCollaborate")]

        public IActionResult GetCollab()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata=collabBL.GetCollaborate(userId);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Data Fetched Sucessfull", data = userdata });
            else
            {
                return this.BadRequest(new {sucess=false,message="Data Fetched Unsucessfull"});
            }

        }

        [Authorize]
        [HttpDelete("RemoveCollaborate")]

        public IActionResult RemoveCollaborate(long noteId, string emailId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = collabBL.RemoveCollaborate(noteId, userId, emailId);
            if (userdata != false)
                return this.Ok(new { success = true, message = "Remove  Data Successfull" });
            else
                return this.BadRequest(new { success = false, message = "Remove Data failed" });
        }


    }
}
