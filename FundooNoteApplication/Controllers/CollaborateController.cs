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

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborateController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IDistributedCache distributedCache;

        public CollaborateController(ICollabBL collabBL, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("Add")]
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
        [HttpGet("Get")]

        public async Task<IActionResult> GetCollabAsync()
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
                    return this.Ok(new { success = true, message = " Data fetch Successfully", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not able to fetch data" });
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
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = collabBL.RemoveCollaborate(noteId, userId, emailId);
            if (userdata != false)
                return this.Ok(new { success = true, message = "Remove  Data Successfull" });
            else
                return this.BadRequest(new { success = false, message = "Remove Data failed" });
        }


    }
}
