using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server;
using register;

namespace Server.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        [HttpGet("{id}")]
      public ActionResult addDevice(string id)
        {
            string connstring = manageDevices.addDeviceEntrance(id);
            return Ok(connstring);
        }
    }
}