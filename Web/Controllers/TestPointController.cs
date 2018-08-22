using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestPointController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3", "banana" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Your get number is: " + id;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Dictionary<string, string>> Post(string value, string book)
        {
            return new Dictionary<string, string>
            {
                ["Something"] = "AnotherThing",
                ["YourValue"] = value,
                ["YourOtherValue"] = "This is your value, but different: " + book,
                ["Number"] = "42"
            };
        }
    }
}
