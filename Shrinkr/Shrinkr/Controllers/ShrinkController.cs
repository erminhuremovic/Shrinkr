using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shrinkr.Dal;

namespace Shrinkr.Controllers
{
    [ApiController]
    public class ShrinkController : ControllerBase
    {
        private readonly ILogger<ShrinkController> logger;
        private readonly IDatabase database;

        public ShrinkController(ILogger<ShrinkController> logger, IDatabase database)
        {
            this.logger = logger;
            this.database = database;
        }

        [HttpPost]
        [Route("Generate")]
        public IActionResult Generate([FromBody]string longUrl)
        {
            var shortUrlToken = Guid.NewGuid().ToString().Substring(0, 8);
            var shortUrl = $"{Request.Headers["Origin"]}/{shortUrlToken}";
            this.database.Add(shortUrlToken, shortUrl, longUrl);
            return new OkObjectResult(shortUrl);
        }

        [HttpGet]
        [Route("/{token}")]
        public IActionResult ShrinkRedirect([FromRoute]string token)
        {
            var urlMapping = this.database.UrlMappings.FirstOrDefault(x => x.Token == token);
            return new RedirectResult(urlMapping.LongUrl);
        }

        [HttpGet]
        [Route("/GetDatabase")]
        public IActionResult GetDatabase()
        {
            return new OkObjectResult(this.database.UrlMappings);
        }

    }
}
