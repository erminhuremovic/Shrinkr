using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shrinkr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShrinkController : ControllerBase
    {
        private readonly ILogger<ShrinkController> _logger;

        public ShrinkController(ILogger<ShrinkController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>();
        }
    }
}
