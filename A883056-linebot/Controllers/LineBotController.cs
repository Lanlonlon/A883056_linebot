using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A883056_linebot.Models;
using Line.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A883056_linebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly LineBotConfig _lineBotConfig;

        public LineBotController(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _lineBotConfig = new LineBotConfig();
            _lineBotConfig.channelSecret = "175a36eb0d9cc08b5f1649a5ff7b1076";
            _lineBotConfig.accessToken = "HPOfFSpAHqf+AFlNQUN5+0Qu7yDuJX3eyo5g6ghJI8Pk1WMjtAS6JFqpTBoLHm0sWFYvvlzxjXR0Pti2AGEWa/XoxcFj2j8HEVDV7OMx43a+oW5XrIK3w5fePaAFOEBPBXm4fMXGACXwizDU8esYywdB04t89/1O/w1cDnyilFU=";
        }

        
        //完整的路由網址就是 https://xxx/api/linebot/run
        [HttpPost("run")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
                var lineMessagingClient = new LineMessagingClient(_lineBotConfig.accessToken);
                var lineBotApp = new LineBotApp(lineMessagingClient);
                await lineBotApp.RunAsync(events);
            }
            catch (Exception ex)
            {
                //需要 Log 可自行加入
                //_logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
    }
}
