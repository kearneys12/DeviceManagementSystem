using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Capstone.Models;
using Capstone.DAO;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class DevicesController : ControllerBase
    {
        private IDeviceDAO deviceDAO;
        
        public DevicesController(IDeviceDAO _deviceDAO)
        {
            deviceDAO = _deviceDAO;
        }
        
        [Authorize]
        [HttpGet]
        public List<CheckIn> GetAllDevicesAndRelavantAlerts()
        {
            List<CheckIn> allDeviceWithRelavantAlerts = deviceDAO.GetAllDevicesAndRelavantAlerts();

            return allDeviceWithRelavantAlerts;
        }

        [Authorize]
        [HttpPut("{inputSerial}")]
        public ActionResult UpdateMaintenanceCheckPoint(string inputSerial)
        {
            deviceDAO.UpdateMaintenanceCheckPoint(inputSerial);

            return Ok();

        }

    }
}