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
    public class AlertsController : ControllerBase
    {
        //private IAlertDAO alertDAO;

        //public AlertsController(IAlertDAO _alertDAO)
        //{
        //    alertDAO = _alertDAO;
        //}

        //[Authorize]
        //[HttpGet]
        //public List<CheckIn> GetMachineAlerts()
        //{
        //    List<CheckIn> machinesAlerting = alertDAO.GetMachineData();

        //    return machinesAlerting;
        //}
    }
}