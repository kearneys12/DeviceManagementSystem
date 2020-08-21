using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Device
    {
        public string Serial { get; set; }
        public string LastMaintenanceDateTime { get; set; }
        public decimal TimeOfMaintenancePulleyDataLeftDistance { get; set; }
        public decimal TimeOfMaintenancePulleyDataRightDistance { get; set; }
    }
}
