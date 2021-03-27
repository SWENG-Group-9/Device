using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server;

namespace Server.Conrollers
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
        [HttpGet]
        public ActionResult <int>GetCurrent()
        {
            var commandItem = Server.Program.current;
            return Ok(commandItem);
        }

    }
}