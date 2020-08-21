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
    public class MachineController : ControllerBase
    {
        private IMachineDAO machineDAO;
        
        public MachineController(IMachineDAO _machineDAO)
        {
            machineDAO = _machineDAO;
        }
        
        [Authorize]
        [HttpGet]
        public List<CheckIn> GetMachineCheckins()
        {
            List<CheckIn> checkins = machineDAO.GetMachineCheckIns();

            return checkins;
        }
    }
}