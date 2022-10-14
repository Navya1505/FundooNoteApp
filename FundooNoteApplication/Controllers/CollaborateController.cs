using BusinessModel.Interface;
using BusinessModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using RepositoryModel.Entity;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborateController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollaborateController> _logger;

        public CollaborateController(ICollabBL collabBL, IDistributedCache distributedCache, ILogger<CollaborateController> _logger)
        {
            this.collabBL = collabBL;
            this.distributedCache = distributedCache;
            this._logger= _logger;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult Collab(long noteId, string receiver_email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = collabBL.AddCollaborate(userId, noteId, receiver_email);
                if (userdata != null)
                {
                    _logger.LogInformation("Collaborated Successfull from ADD API route");
                    return this.Ok(new { success = true, message = "Collaborated Successfull", data = userdata });
                }
                else
                {
                    _logger.LogInformation(" note CollaborateUnsucccessfull from ADD  API route");
                    return this.BadRequest(new { success = false, message = " collaborate UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("Get")]

        public async Task<IActionResult> GetCollabAsync()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var cachekey = Convert.ToString(userId);
                string serializeddata;
                List<CollaborateEntity> result;

                var distcacheresult = await distributedCache.GetAsync(cachekey);
                if (distcacheresult != null)
                {
                    serializeddata = Encoding.UTF8.GetString(distcacheresult);
                    result = JsonConvert.DeserializeObject<List<CollaborateEntity>>(serializeddata);
                    _logger.LogInformation("Fetch Successfull from GET  API route");
                    return this.Ok(new { success = true, message = "fetch Successfully", data = result });
                }
                else
                {
                    var userdata = collabBL.GetCollaborate(userId);
                    serializeddata = JsonConvert.SerializeObject(userdata);
                    distcacheresult = Encoding.UTF8.GetBytes(serializeddata);
                    var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(cachekey, distcacheresult, options);
                    if (userdata != null)
                    {
                        _logger.LogInformation("Data fetch Successfull from GET API route");
                        return this.Ok(new { success = true, message = " Data fetch Successfully", data = userdata });
                    }
                    else
                    {
                        _logger.LogInformation(" Fetch data UnSuccessfull from GET  API route");
                        return this.BadRequest(new { success = false, message = "Not able to fetch data" });
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
            /*
            if (!memoryCache.TryGetValue(cachekey, out List<CollabEntity> cacheresult))
            {
                var userdata = collabBL.GetCollab(userId);
                memoryCache.Set(cachekey, userdata);
                if (userdata != null)
                    return this.Ok(new { success = true, message = "Fetch Successfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Fetch operation failed" });
            }
            else
                return this.Ok(new { success = true, message = " Fetch Successfully", data = cacheresult });
            */
        }


        [Authorize]
        [HttpDelete("Remove")]

        public IActionResult RemoveCollaborate(long noteId, string emailId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var userdata = collabBL.RemoveCollaborate(noteId, userId, emailId);
                if (userdata != false)
                {
                    _logger.LogInformation("Remove  Successfull from Remove Collab API route");
                    return this.Ok(new { success = true, message = "Remove  Data Successfull" });
                }
                else
                {
                    _logger.LogInformation("Remove operation failed from Remove Collab API route");
                    return this.BadRequest(new { success = false, message = "Remove Data failed" });

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

    }
}
