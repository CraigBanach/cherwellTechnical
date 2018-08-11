using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cherwellTechnical.Controllers
{
    [Route("api/[controller]")]
    public class TriangleController : Controller
    {
        // GET api/values/5
        [HttpGet("{designation}")]
        public string Get(string designation)
        {
            return "value";
        }
    }
}
