using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessModel.Interface;
using RepositoryModel.Services;
using BusinessModel.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryModel.Entity;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBL labelBL, IDistributedCache distributedCache, ILogger<LabelController> _logger)
        {
            this.labelBL = labelBL;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }

        [Authorize]
        [HttpPost("Create")]

        public IActionResult CreateLabel(long noteId, string LabelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = labelBL.CreateLabel(userId, noteId, LabelName);
                if (userdata != null)
                {
                    _logger.LogInformation("Label created Successfull from Create Api route");
                    return this.Ok(new { success = true, message = "Label created Successfull", data = userdata });

                }
                else
                {
                    _logger.LogInformation("Label created UnSuccessfull from Create Api route");
                    return this.BadRequest(new { success = false, message = "Label Not created Successfull" });
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        [Authorize]
        [HttpGet("Get")]
        public async Task<IActionResult> GetLabel()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var cachekey = Convert.ToString(userId);
                string serializeddata;
                List<LabelEntity> result;

                var distcacheresult = await distributedCache.GetAsync(cachekey);
                if (distcacheresult != null)
                {
                    serializeddata = Encoding.UTF8.GetString(distcacheresult);
                    result = JsonConvert.DeserializeObject<List<LabelEntity>>(serializeddata);
                    _logger.LogInformation("Labels Fetch Successfull from GET route");
                    return this.Ok(new { success = true, message = "Labels Fetch Successfull", data = result });
                }
                else
                {
                    var userdata = labelBL.GetLabel(userId);
                    serializeddata = JsonConvert.SerializeObject(userdata);
                    distcacheresult = Encoding.UTF8.GetBytes(serializeddata);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(cachekey, distcacheresult, options);
                    if (userdata != null)
                    {
                        _logger.LogInformation("Labels Fetch Successfull from GET route");
                        return this.Ok(new { success = true, message = "Labels fetch Successfull", data = userdata });
                    }
                    else
                    {
                        _logger.LogInformation("Labels Fetch UnSuccessfull from GET route");
                        return this.BadRequest(new { success = false, message = "Not able to fetch Labels" });
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
            }

            /*
            if (!memoryCache.TryGetValue(cachekey, out List<LabelEntity> cacheresult))
            {
                var userdata = labeBL.GetLabel(userId);
                memoryCache.Set(cachekey, userdata);
                if (userdata != null)
                    return this.Ok(new { success = true, message = "Label Fetch Successfully", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not able to Fetch Label" });
            }
            else
                return this.Ok(new { success = true, message = "Label Fetch Successfully", data = cacheresult });
            */

        


        [Authorize]
        [HttpPut("Update")]

        public IActionResult UpdateLabel(long NoteId, long LabelId, string LabelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = labelBL.UpdateLabel(userId, NoteId, LabelName, LabelId);
                if (userdata != null)
                {

                    _logger.LogInformation("Labels Updated Successfull from Update route");
                    return this.Ok(new { success = true, message = "Label Updated Successfull", data = userdata });
                }
                else
                {

                    _logger.LogInformation("Labels Update UnSuccessfull from Update route");
                    return this.BadRequest(new { success = false, message = " Label Not Updated Successfull" });
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long NoteId,string LabelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var userdata = labelBL.DeleteLabel(userId, NoteId, LabelName);
                if (userdata != false)
                {

                    _logger.LogInformation("Labels Deleted Successfull from Delete route");
                    return this.Ok(new { success = true, message = "Label Deleted Successfull", data = userdata });
                }
                else
                {

                    _logger.LogInformation("Labels Not Deleted Successfull from Delete route");
                    return this.BadRequest(new { success = false, message = " Label Not Deleted Successfull" });
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }
    }
}
