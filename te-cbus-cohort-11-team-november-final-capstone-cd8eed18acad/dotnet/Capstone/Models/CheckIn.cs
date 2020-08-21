using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class CheckIn
    {
        public int AuditLogId { get; set; }
        public string PropertyName { get; set; }
        public DateTime LastCheckInTimeUtc { get; set; }
        public string Serial { get; set; }
        public string Name { get; set; }
        public int MachineModelId { get; set; }
        public string Organization { get; set; }
        public int ArmAssistLeft { get; set; }
        public int ArmAssistRight { get; set; }
        public int ArmCartLeft { get; set; }
        public int ArmCartRight { get; set; }
        public decimal TotalPulleyDataLeftDistance { get; set; }
        public decimal TotalPulleyDataRightDistance { get; set; }
        public decimal BatteryLevel { get; set; }
        public bool BatteryLow { get; set; }
        public bool InUse { get; set; }
        public bool ConnectionLost { get; set; }
        public bool PastMaintenance { get; set; }
        public decimal LeftDistanceSinceMaintenance { get; set; }
        public decimal RightDistanceSinceMaintenance { get; set; }
        public DateTime MaintenanceDateTime { get; set; }
        
    }
}
