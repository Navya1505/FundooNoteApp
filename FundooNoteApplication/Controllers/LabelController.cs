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

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IDistributedCache distributedCache;

        public LabelController(ILabelBL labelBL, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("Create")]

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
        [HttpGet("Get")]
        public async Task<IActionResult> GetLabel()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var cachekey = Convert.ToString(userId);
            string serializeddata;
            List<LabelEntity> result;

            var distcacheresult = await distributedCache.GetAsync(cachekey);
            if (distcacheresult != null)
            {
                serializeddata = Encoding.UTF8.GetString(distcacheresult);
                result = JsonConvert.DeserializeObject<List<LabelEntity>>(serializeddata);

                return this.Ok(new { success = true, message = "Labels fetch Successfull", data = result });
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
                    return this.Ok(new { success = true, message = "Labels fetch Successfull", data = userdata });
                else
                    return this.BadRequest(new { success = false, message = "Not able to fetch Labels" });
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

        }


        [Authorize]
        [HttpPut("Update")]

        public IActionResult UpdateLabel(long NoteId, long LabelId, string LabelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var userdata = labelBL.UpdateLabel(userId, NoteId,  LabelName,LabelId);
            if (userdata != null)
                return this.Ok(new { success = true, message = "Label Updated Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Not Updated Successfull" });
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long NoteId,string LabelName)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            
            var userdata = labelBL.DeleteLabel(userId, NoteId, LabelName);
            if (userdata != false)
                return this.Ok(new { success = true, message = "Label Deleted Successfull", data = userdata });
            else
                return this.BadRequest(new { success = false, message = " Label Not Deleted Successfull" });
        }
    }
}
