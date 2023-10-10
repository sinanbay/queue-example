using Microsoft.AspNetCore.Mvc;
using RebusBasicExample.DTO;
using RebusBasicExample.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RebusBasicExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisher;

        public PublisherController(IPublisherService publisher)
        {
            this.publisher = publisher;
        }
        [HttpPost("send-to-queue")]
        public async Task<ActionResult<bool>> SentToQueue([FromBody] QueueType dto)
        {
            bool result = await this.publisher.SendToQueue(dto);
            return Ok(result);
        }
    }
}

