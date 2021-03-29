using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server;

namespace Server.Controllers
{
    [Route("api/current")]
    [ApiController]
    public class CurrentController : ControllerBase
    {
       // private readonly ICommanderRepo _repository;

        public CurrentController()
        {
            //_repository = repository;
        }

        //GET api/current
        //  gets current number of customers in shop
        [HttpGet]
        public ActionResult <int>GetCurrent()
        {
            var commandItem = Server.Program.current;
            return Ok(commandItem);
        }

        //PUT api/current
        //  updates allowed max number of customers
        [HttpGet]
        public ActionResult <int>UpdateMax(CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromServer=Server.Program.max;
            if(commandModelFromServer==null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto,commandModelFromServer);
        }

    }
}