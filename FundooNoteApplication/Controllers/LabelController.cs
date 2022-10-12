﻿using Microsoft.AspNetCore.Authorization;
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
                return this.Ok(new { success = true, message = "Label created Successfull", data = userdata });
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
                return this.Ok(new { success = true, message = "Label Fetch Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Fetch UnSuccessfull" });
        }
        [Authorize]
        [HttpPut("UpdateLabel")]

        public IActionResult UpdateLabel(long NoteId, long LabelId, string LabelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var userdata = labelBL.UpdateLabel(userId, NoteId,  LabelName,LabelId);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Label Updated Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Not Updated Successfull" });
        }
        [Authorize]
        [HttpDelete("Deletelabel")]
        public IActionResult DeleteLabel(long NoteId,string LabelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            
            var userdata = labelBL.DeleteLabel(userId, NoteId, LabelName);
            if (userdata != false)
                return this.Ok(new { success = true, message = "Label Deleted Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Not Deleted Successfull" });
        }
    }
}
