using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class MachineSqlDAO : IMachineDAO
    {
        private readonly string connectionString;

        public MachineSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        private string sqlGetMachineCheckIns = "SELECT * FROM MachineCheckIns";

        public List<CheckIn> GetMachineCheckIns()
        {
            List<CheckIn> checkIns = new List<CheckIn>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlGetMachineCheckIns, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CheckIn chkin = new CheckIn()
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
                            
                                checkIns.Add(chkin);
                            
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return checkIns;
        }
    }
}
