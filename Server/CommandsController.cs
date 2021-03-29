using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server;

namespace Server.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CurrentController : ControllerBase
    {
        //get current customers
        // GET api/commands/1
        [HttpGet("1")]
        public ActionResult<int> GetCurrent()
        {
            var commandItem = Server.Program.current;
            return Ok(commandItem);
        }
        //get maximum  number of customers
        // GET api/commands/0
        [HttpGet("0")]
        public ActionResult<int> GetMax()
        {
            var commandItem = Server.Program.max;
            return Ok(commandItem);
        }

        //change max value
        // PUT api/commands/0
        [HttpPut("0")]
        public ActionResult PutMax(int newMax)
        {
            //newMax = 15;          //test value for updating max
            Server.Program.max = newMax;
            return NoContent();
        }


    }
    /*
         get max value      done
         change max value   done
         get current value  done
         manual lock door
         manual unlock door
         pull stats
         send stats
    */
}
