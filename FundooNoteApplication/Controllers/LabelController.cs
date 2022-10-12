using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessModel.Interface;
using RepositoryModel.Services;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        [Authorize]
        [HttpPost("CreateLabel")]

        public IActionResult CreateLabel(long noteId, string LabelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = labelBL.CreateLabel(userId, noteId, LabelName);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Label created Successfully", data = userdata });
            else
                return this.BadRequest(new { success = false, message = "Label Not created Successfull" });
        }

        [Authorize]
        [HttpGet("GetLabel")]
        public IActionResult GetLabel()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = labelBL.GetLabel(userId);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Label Fetch Successfully", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Fetch UnSuccessfull" });
        }
    }
}
