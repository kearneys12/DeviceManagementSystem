using Capstone.DAO;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace CapstoneTest
{
    [TestClass]
    public class MachineSqlDAOTest
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True;";

        private TransactionScope transaction;

        [TestInitialize]
        public void Initalize()
        {
            transaction = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_machineCheckInsInsert = "INSERT INTO MachineCheckIns (AuditLogId, PropertyName, LastCheckInTimeUtc, Serial, Name, MachineModelId, Organization, ArmAssistLeft, ArmAssistRight, ArmCartLeft, ArmCartRight, Total_PulleyDataLeftDistance, Total_PulleyDataRightDistance, BatteryLevel) " +
                "VALUES ('952593', 'LastCheckInTimeUtc', '8/27/2019 23:15', 'IHM-S001-4G7S-LJL6', 'June', '1', 'Org 1', '30740', '25750', '7320', '8670', '317500', '324939', '10')";
                SqlCommand machineCheckInsInsertCmd = new SqlCommand(sql_machineCheckInsInsert, conn);
                machineCheckInsInsertCmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void TestGetMachineCheckIns()
        {
            MachineSqlDAO getMachineCheckIns = new MachineSqlDAO(connectionString);

            List<CheckIn> testResult = getMachineCheckIns.GetMachineCheckIns();

            Assert.AreEqual(952593, testResult[testResult.Count - 1].AuditLogId);
            Assert.AreEqual("LastCheckInTimeUtc", testResult[testResult.Count - 1].PropertyName);
            Assert.AreEqual(DateTime.Parse("8 / 27 / 2019 23:15"), testResult[testResult.Count - 1].LastCheckInTimeUtc);
            Assert.AreEqual("IHM-S001-4G7S-LJL6", testResult[testResult.Count - 1].Serial);
            Assert.AreEqual("June", testResult[testResult.Count - 1].Name);
            Assert.AreEqual(1, testResult[testResult.Count - 1].MachineModelId);
            Assert.AreEqual(30740, testResult[testResult.Count - 1].ArmAssistLeft);
            Assert.AreEqual(25750, testResult[testResult.Count - 1].ArmAssistRight);
            Assert.AreEqual(7320, testResult[testResult.Count - 1].ArmCartLeft);
            Assert.AreEqual(8670, testResult[testResult.Count - 1].ArmCartRight);
            Assert.AreEqual(317500, testResult[testResult.Count - 1].TotalPulleyDataLeftDistance);
            Assert.AreEqual(324939, testResult[testResult.Count - 1].TotalPulleyDataRightDistance);
            Assert.AreEqual(10, testResult[testResult.Count - 1].BatteryLevel);
        }
    }
}
