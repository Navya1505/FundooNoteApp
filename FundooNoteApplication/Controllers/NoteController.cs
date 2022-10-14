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
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryModel.Entity;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteBL noteBL, IDistributedCache distributedCache, ILogger<NoteController> _logger)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }


        [HttpPost("Create")]
        public IActionResult CreateNote(Notes createnote)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var userdata = noteBL.CreateNoteUser(userId, createnote);
                if (userdata != null)
                {
                    _logger.LogInformation("Note created Successfull from CreateNote API route");
                    return this.Ok(new { success = true, message = "Note created Successfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation("Note created UnSuccessfull from CreateNote API route");

                    return this.BadRequest(new { success = false, message = " create note UnSuccessfull" });
                }
            }
            catch (Exception e)

            {
                _logger.LogError(e.Message);
                throw e;
            }
        }
        [HttpPost("Get")]
        public async Task<IActionResult> GetNote()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var cachekey = Convert.ToString(userId);
                string serializeddata;
                List<NoteEntity> result;

                var distcacheresult = await distributedCache.GetAsync(cachekey);

                if (distcacheresult != null)
                {
                    serializeddata = Encoding.UTF8.GetString(distcacheresult);
                    result = JsonConvert.DeserializeObject<List<NoteEntity>>(serializeddata);

                    //return this.Ok(distcacheresult);
                    _logger.LogInformation("Note Data fetch Successfull from GET route");
                    return this.Ok(new { success = true, message = "Data Note Fetch Successfull", data = result });
                }
                else
                {
                    var userdata = noteBL.GetNotes(userId);
                    serializeddata = JsonConvert.SerializeObject(userdata);
                    distcacheresult = Encoding.UTF8.GetBytes(serializeddata);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    await distributedCache.SetAsync(cachekey, distcacheresult, options);
                    if (userdata != null)
                    {
                        _logger.LogInformation("Note Data fetch Successfull from GET route");
                        return this.Ok(new { success = true, message = "Note Data fetch Successfull", data = userdata });
                    }
                    else
                    {
                        _logger.LogInformation("Note Data fetch UnSuccessfull from GET route");
                        return this.BadRequest(new { success = false, message = "Note Data fetch UnSuccessfull" });
                    }
                    /*
                    if (!memoryCache.TryGetValue(cachekey, out List<NoteEntity> cacheresult))
                    {
                        var userdata = noteBL.GetNoteUser(userId);
                        memoryCache.Set(cachekey,userdata);
                        if (userdata != null)
                            return this.Ok(new { success = true, message = "Note Data fetch Successfully", data = userdata });
                        else
                            return this.BadRequest(new { success = false, message = "Not able to fetch notes" });
                    }
                    else
                        return this.Ok(new { success = true, message = "Note Data fetch Successfully", data = cacheresult });
                    */
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }

       
        [HttpPut("Update")]
        public IActionResult UpdateNotes(long NoteId,Notes updateNote )
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = noteBL.UpdateNotes(userId, NoteId, updateNote);
                if (userdata == true)
                {
                    _logger.LogInformation("Note UpdateNotesSuccessfull from Update route");
                    return this.Ok(new { success = true, message = "Note updated Successfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation("Note Update UnSuccessfull from Update route");
                    return this.BadRequest(new { success = false, message = " Note updated UnSuccessfull" });

                }
            }
            catch (Exception e)
            {

                _logger.LogError(e.Message);
                throw e;
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = noteBL.DeleteNotes(userId, NoteId);
                if (userdata == true)
                {
                    _logger.LogInformation("Note Delete Successfull from Delete route");
                    return this.Ok(new { success = true, message = "deleted sucessfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation("Note Delete UnSuccessfull from Delete route");
                    return this.BadRequest(new { success = false, message = "Note deleted UnSuccessfull" });
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
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
                {
                    _logger.LogInformation("Note Pinned Successfull from PinNotes route");
                    return this.Ok(new { sucess = true, message = "IsPinned ", data = userData });
                }
                else if (userData == false)
                {

                    _logger.LogInformation("Note UnPinned Successfull from PinNotes route");
                    return this.Ok(new { success = true, message = "UnPinned successfull", data = userData });
                }
                else
                {
                    _logger.LogInformation("Note Pinned Failed from PinNotes route");
                    return this.BadRequest(new { sucess = false, meassage = " Pinned Failed" });
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
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
                {
                    _logger.LogInformation("NoteArcheive  Successfull from Archieve route");
                    return this.Ok(new { sucess = true, message = "ArcheiveNotes ", data = userData });
                }
                else if (userData == false)
                {
                    _logger.LogInformation("Note UnArcheiveNotes  from Archieve route");
                    return this.Ok(new { success = true, message = "UnArcheivedNotes", data = userData });
                }
                else
                {
                    _logger.LogInformation("Note Archeive Failed from Archieve route");
                    return this.BadRequest(new { sucess = false, meassage = "Archeived Failed" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            
            }
        }
        [HttpPut("Trash")]
        public IActionResult IsTrash(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userData = noteBL.IsTrash(userId, NoteId);
                if (userData == true)
                {
                    _logger.LogInformation("Note Trash  from Trash route");
                    return this.Ok(new { sucess = true, message = "Trash ", data = userData });
                }

                else if (userData == false)
                {
                    _logger.LogInformation("Note Not Trashed from Trash route");

                    return this.Ok(new { sucess = true, message = "Not Trashed", data = userData });
                }
                else
                {
                    _logger.LogInformation("Note Trash UnSucessfull from Trash route");
                    return this.BadRequest(new { sucess = false, meassage = "TrashUnSuccessfull" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
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
                if (userData != null)
                {
                    _logger.LogInformation("UpdateNoteColor successfull from Color route");
                    return this.Ok(new { sucess = true, message = "UpdateNoteColor successfull", data = userData });
                }
                else
                {
                    _logger.LogInformation(" UpdateNoteColour Unsuccessfull from Color route");
                    return this.BadRequest(new { sucess = false, meassage = "UpdateNoteColour Unsuccessfull" });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }


        [Authorize]
        [HttpPost("ImageUpload")]

        public IActionResult Image(long noteId, IFormFile file)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = noteBL.Image(userId, noteId, file);
                if (userdata.Trash == true)
                {
                    _logger.LogInformation(" Image uploaded successfull from Image route");
                    return this.Ok(new { success = true, message = "Image uploaded successfull", data = userdata });
                }
                else if (userdata.Trash == false)
                {
                    _logger.LogInformation(" Image uploaded Unsuccessfull from Image route");
                    return this.Ok(new { success = true, message = "Image not uploaded", data = userdata });
                }
                else
                {

                    _logger.LogInformation(" Image uploaded failed from Image route");
                    return this.BadRequest(new { success = false, message = "Image upload failed" });
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e ;
            }
        }


    }
}
