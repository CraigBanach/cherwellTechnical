using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using cherwellTechnical.Factories.Interfaces;
using cherwellTechnical.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cherwellTechnical.Controllers
{
    [Route("api/[controller]")]
    public class TriangleController : Controller
    {
        private readonly ITriangleFactory triangleFactory;

        public TriangleController(ITriangleFactory factory)
        {
            this.triangleFactory = factory;
        }

        // GET api/Triangle/:designation
        [HttpGet("{designation}")]
        public IActionResult Get(string designation)
        {
            Triangle triangle;

            try
            {
                triangle = this.triangleFactory.ToTriangle( designation );
            }
            catch ( Exception e )
            {
                if (e is ArgumentException)
                {
                    return BadRequest();
                } else
                {
                    return StatusCode( 500 );
                }
            }
            
            return Ok(JsonConvert.SerializeObject( triangle ));
        }

        [HttpPost]
        public IActionResult Post( [FromBody] Collection<Coordinate> coordinates )
        {
            Triangle triangle;

            try
            {
                triangle = this.triangleFactory.ToTriangle( coordinates );
            }
            catch ( Exception e )
            {
                if ( e is ArgumentException )
                {
                    return BadRequest();
                }
                else
                {
                    return StatusCode( 500 );
                }
            }

            return Ok( triangle );
        }
    }
}
