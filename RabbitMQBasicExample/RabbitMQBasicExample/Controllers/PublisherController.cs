using Microsoft.AspNetCore.Mvc;
using RabbitMQBasicExample.DTO;
using RabbitMQBasicExample.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RabbitMQBasicExample.Controllers
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
        public async Task<ActionResult<bool>> SentToQueue([FromBody] List<QueueType> dto)
        {
            bool result = this.publisher.SendToQueue(dto);
            return Ok(result);
        }
    }
}

