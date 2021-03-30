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

        //manually change door status
        // PUT api/commands/2

        [HttpPut("2")]
        public ActionResult manualLockDoor(bool manualDoorStatus)
        {
            if (manualDoorStatus == true)
            {
                //not sure what method to set door status is but needs to go here and below
                return NoContent();
            }
            else

                return NoContent();
        }

        //get stats
        // GET api/commands/3

        [HttpGet("3")]
        public ActionResult <int[]> getDoorStats(double period,string date,string time)
        {
            var commandItem;
            double i=0;
            int j=0;
            while(i<period){
                commandItem[j]=findDoorStat(date,time);             //need to write method to get data from data.json file which takes date and start time as parameters
                i=i+0.5;                                            //function then returns data in array
                double newTime=time.toDouble()+30;                     //need to increment time by half an hour for every period increment, not sure if time will actually be a string
                j++;
            }
            return Ok(commandItem);
        }

        //put stats
        //PUT api/commands/3
        [HttpPut("3")]
        public ActionResult sendDoorStats()
        {

            return NoContent();
        }

        //get user inputted device name
        //get api/commands/4
        [HttpGet("4")]
        public ActionResult <string>getDeviceName(string deviceName)
        {
                return Ok(deviceName);                          //dont think it would be parameter, need extra variable in program.cs for user input i think
        }

        //display new device with name from user
        //put api/commands/4
        [HttpPut("4")]
        public ActionResult putNewDevice(string deviceName){
            return NoContent();
        }
    }
    /*
         get max value      done
         change max value   done
         get current value  done
         manual lock door   partial
         manual unlock door partial
         pull stats         partial
         send stats         partial
         create new device  partial
         manage device  
         individual device stats
    */
}
