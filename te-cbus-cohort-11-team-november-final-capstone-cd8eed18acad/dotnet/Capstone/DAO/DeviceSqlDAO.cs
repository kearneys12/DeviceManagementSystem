using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;
using Capstone.Controllers;

namespace Capstone.DAO
{
    public class DeviceSqlDAO : IDeviceDAO
    {
        private readonly string connectionString;

        //private string sqlGetDevices = "SELECT Serial, name FROM MachineCheckIns GROUP BY Serial, name;";

        private string sqlGetOrderedMachineCheckIns = "SELECT * FROM MachineCheckIns ORDER BY Serial, LastCheckInTimeUtc";
        private string sqlGetDeviceData = "SELECT * FROM DeviceData";

        public DeviceSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;

        }

        public List<CheckIn> GetDevices()
        {
            List<CheckIn> orderedCheckIns = new List<CheckIn>();
            List<CheckIn> recentCheckIns = new List<CheckIn>();
            List<Device> deviceData = new List<Device>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetOrderedMachineCheckIns, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CheckIn checkIn = new CheckIn()
                            {
                                AuditLogId = Convert.ToInt32(reader["AuditLogId"]),
                                PropertyName = Convert.ToString(reader["PropertyName"]),
                                LastCheckInTimeUtc = Convert.ToDateTime(reader["LastCheckInTimeUtc"]),
                                Serial = Convert.ToString(reader["Serial"]),
                                Name = Convert.ToString(reader["Name"]),
                                MachineModelId = Convert.ToInt32(reader["MachineModelId"]),
                                Organization = Convert.ToString(reader["Organization"]),
                                ArmAssistLeft = Convert.ToInt32(reader["ArmAssistLeft"]),
                                ArmAssistRight = Convert.ToInt32(reader["ArmAssistRight"]),
                                ArmCartLeft = Convert.ToInt32(reader["ArmCartLeft"]),
                                ArmCartRight = Convert.ToInt32(reader["ArmCartRight"]),
                                TotalPulleyDataLeftDistance = Convert.ToDecimal(reader["Total_PulleyDataLeftDistance"]),
                                TotalPulleyDataRightDistance = Convert.ToDecimal(reader["Total_PulleyDataRightDistance"]),
                                BatteryLevel = Convert.ToDecimal(reader["BatteryLevel"])
                            };

                            orderedCheckIns.Add(checkIn);
                        }
                    }


                }
            }
            catch (SqlException)
            {
                throw;
            }
            try
            {
                using (SqlConnection conn2 = new SqlConnection(connectionString))
                {
                    conn2.Open();


                    SqlCommand cmd2 = new SqlCommand(sqlGetDeviceData, conn2);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            Device device = new Device()
                            {
                                Serial = Convert.ToString(reader2["Serial"]),
                                LastMaintenanceDateTime = Convert.ToString(reader2["LastMaintenanceDateTime"]),
                                TimeOfMaintenancePulleyDataLeftDistance = Convert.ToDecimal(reader2["Time_of_Maintenance_PulleyDataLeftDistance"]),
                                TimeOfMaintenancePulleyDataRightDistance = Convert.ToDecimal(reader2["Time_of_Maintenance_PulleyDataRightDistance"])
                            };
                            deviceData.Add(device);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            //breaksdown our total list to contain the recent updates from each machine
            for (int i = 2; i < orderedCheckIns.Count; i++)
            {
                if (orderedCheckIns[i].Serial != orderedCheckIns[i - 1].Serial)
                {
                    recentCheckIns.Add(orderedCheckIns[i - 1]);
                }
                if (i == orderedCheckIns.Count - 1)
                {
                    recentCheckIns.Add(orderedCheckIns[orderedCheckIns.Count - 1]);
                }
            }

            for (int i = 0; i < recentCheckIns.Count; i++)
            {
                recentCheckIns[i].LeftDistanceSinceMaintenance = recentCheckIns[i].TotalPulleyDataLeftDistance - deviceData[i].TimeOfMaintenancePulleyDataLeftDistance;
                recentCheckIns[i].RightDistanceSinceMaintenance = recentCheckIns[i].TotalPulleyDataRightDistance - deviceData[i].TimeOfMaintenancePulleyDataRightDistance;

                if( (recentCheckIns[i].LeftDistanceSinceMaintenance > 20000M) || (recentCheckIns[i].RightDistanceSinceMaintenance> 20000M))
                {
                    recentCheckIns[i].PastMaintenance = true;
                }
            }

            return recentCheckIns;
        }

        List<CheckIn> orderedCheckIns = new List<CheckIn>();
        List<CheckIn> recentCheckIns = new List<CheckIn>();
        List<CheckIn> secondMostRecentCheckIns = new List<CheckIn>();
        List<CheckIn> machinesAlerting = new List<CheckIn>();

        public List<CheckIn> GetMachineData()
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetOrderedMachineCheckIns, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CheckIn checkIn = new CheckIn()
                            {
                                AuditLogId = Convert.ToInt32(reader["AuditLogId"]),
                                PropertyName = Convert.ToString(reader["PropertyName"]),
                                LastCheckInTimeUtc = Convert.ToDateTime(reader["LastCheckInTimeUtc"]),
                                Serial = Convert.ToString(reader["Serial"]),
                                Name = Convert.ToString(reader["Name"]),
                                MachineModelId = Convert.ToInt32(reader["MachineModelId"]),
                                ArmAssistLeft = Convert.ToInt32(reader["ArmAssistLeft"]),
                                ArmAssistRight = Convert.ToInt32(reader["ArmAssistRight"]),
                                ArmCartLeft = Convert.ToInt32(reader["ArmCartLeft"]),
                                ArmCartRight = Convert.ToInt32(reader["ArmCartRight"]),
                                TotalPulleyDataLeftDistance = Convert.ToDecimal(reader["Total_PulleyDataLeftDistance"]),
                                TotalPulleyDataRightDistance = Convert.ToDecimal(reader["Total_PulleyDataRightDistance"]),
                                BatteryLevel = Convert.ToDecimal(reader["BatteryLevel"])

                            };

                            orderedCheckIns.Add(checkIn);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            //breaksdown our total list to contain the recent updates from each machine
            for (int i = 2; i < orderedCheckIns.Count; i++)
            {
                if (orderedCheckIns[i].Serial != orderedCheckIns[i - 1].Serial)
                {
                    recentCheckIns.Add(orderedCheckIns[i - 1]);
                    secondMostRecentCheckIns.Add(orderedCheckIns[i - 2]);
                }
                if (i == orderedCheckIns.Count - 1)
                {
                    recentCheckIns.Add(orderedCheckIns[orderedCheckIns.Count - 1]);
                    secondMostRecentCheckIns.Add(orderedCheckIns[i - 2]);
                }
            }
            //write loops that check our last two updates for issues
            for (int i = 0; i < recentCheckIns.Count; i++)
            {
                if (recentCheckIns[i].BatteryLevel < 25.00M)
                {
                    recentCheckIns[i].BatteryLow = true;
                    machinesAlerting.Add(recentCheckIns[i]);
                }

                if (recentCheckIns[i].ArmAssistLeft != secondMostRecentCheckIns[i].ArmAssistLeft ||
                    recentCheckIns[i].ArmAssistRight != secondMostRecentCheckIns[i].ArmAssistRight ||
                    recentCheckIns[i].ArmCartLeft != secondMostRecentCheckIns[i].ArmCartLeft ||
                    recentCheckIns[i].ArmCartRight != secondMostRecentCheckIns[i].ArmCartRight ||
                    recentCheckIns[i].TotalPulleyDataLeftDistance != secondMostRecentCheckIns[i].TotalPulleyDataLeftDistance ||
                    recentCheckIns[i].TotalPulleyDataRightDistance != secondMostRecentCheckIns[i].TotalPulleyDataRightDistance
                    )
                {
                    recentCheckIns[i].InUse = true;
                    machinesAlerting.Add(recentCheckIns[i]);

                }

                TimeSpan diff = DateTime.Now - recentCheckIns[i].LastCheckInTimeUtc;
                if (diff.TotalMinutes >= 15)
                {
                    recentCheckIns[i].ConnectionLost = true;
                    machinesAlerting.Add(recentCheckIns[i]);
                }
            }

            return machinesAlerting;
        }

        public List<CheckIn> GetAllDevicesAndRelavantAlerts()
        {
            List<CheckIn> allDevices = GetDevices();

            List<CheckIn> checkInAlerts = GetMachineData();


            for (int i = 0; i < allDevices.Count; i++)
            {
                for (int j = 0; j < checkInAlerts.Count; j++)
                {
                    if (allDevices[i].Serial == checkInAlerts[j].Serial)
                    {
                        allDevices[i].BatteryLow = checkInAlerts[j].BatteryLow;
                        allDevices[i].InUse = checkInAlerts[j].InUse;
                        allDevices[i].ConnectionLost = checkInAlerts[j].ConnectionLost;
                    }
                }
            }

            return allDevices;

        }


        private string sqlUpdateMaintenaceData = "UPDATE dbo.DeviceData SET LastMaintenanceDateTime = CURRENT_TIMESTAMP, Time_of_Maintenance_PulleyDataLeftDistance = @leftPulleyDistance, Time_of_Maintenance_PulleyDataRightDistance = @rightPulleyDistance WHERE Serial = @serialToUpdate;";

        public void UpdateMaintenanceCheckPoint(string inputSerial)
        {
            decimal leftPulleyDistance = 0M;
            decimal rightPulleyDistance = 0M;
            recentCheckIns = GetDevices();

            for (int i = 0; i < recentCheckIns.Count; i++)
            {
                if (inputSerial == recentCheckIns[i].Serial)
                {
                    leftPulleyDistance = recentCheckIns[i].TotalPulleyDataLeftDistance;
                    rightPulleyDistance = recentCheckIns[i].TotalPulleyDataRightDistance;
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateMaintenaceData, conn);
                    cmd.Parameters.AddWithValue("@leftPulleyDistance", leftPulleyDistance);
                    cmd.Parameters.AddWithValue("@rightPulleyDistance", rightPulleyDistance);
                    cmd.Parameters.AddWithValue("@serialToUpdate", inputSerial);

                    int count = cmd.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {
            }

            ;
        }
    }

}
