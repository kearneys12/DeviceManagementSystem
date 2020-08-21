<template>
  <div class="checkIns">
    <div class="check-in-log-header">
        <div class="header">Machine Check-In Log</div>
    </div>
    <table id="table-checkins">
      <thead>
        <tr>
          <th>Audit Log Id</th>
          <th>Last Check In</th>
          <th>Serial #</th>
          <th>Name</th>
          <th>Model ID</th>
          <th>Organization</th>
          <th>Battery Level</th>
          <th>Left Pulley Cable Travel</th>
          <th>Right Pulley Cable Travel</th>
          <th>Arm Assist Left</th>
          <th>Arm Assist Right</th>
          <th>Arm Cart Left</th>
          <th>Arm Cart Right</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td class="input-cell">
            <input
              type="text"
              id="auditLogFilter"
              placeholder="Serach Text Here"
              v-model="filter.auditLogId"
            />
          </td>
          <td class="input-cell">
            <input
              type="text"
              id="lastCheckInFilter"
              placeholder="Serach Text Here"
              v-model="filter.lastCheckInTimeUtc"
            />
          </td>
          <td class="input-cell">
            <input
              type="text"
              id="serialFilter"
              placeholder="Serach Text Here"
              v-model="filter.serial"
            />
          </td>
          <td class="input-cell">
            <input type="text" id="nameFilter" placeholder="Serach Text Here" v-model="filter.name" />
          </td>
          <td class="input-cell">
            <input
              type="text"
              id="modelIDFilter"
              placeholder="Serach Text Here"
              v-model="filter.machineModelId"
            />
          </td>
          <td class="input-cell">
            <input
              type="text"
              id="organizationFilter"
              placeholder="Serach Text Here"
              v-model="filter.organization"
            />
          </td>
          <td class="input-cell">
            <input
              type="text"
              id="batteryLevelFilter"
              placeholder="Serach Text Here"
              v-model="filter.batteryLevel"
            />
          </td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
          <td></td>
        </tr>
        <tr v-for="checkIn in filteredList" v-bind:key="checkIn.auditLogId">
          <td>{{ checkIn.auditLogId }}</td>
          <td>{{ checkIn.lastCheckInTimeUtc }}</td>
          <td>{{ checkIn.serial }}</td>
          <td>{{ checkIn.name }}</td>
          <td>{{ checkIn.machineModelId }}</td>
          <td>{{ checkIn.organization }}</td>
          <td>{{ checkIn.batteryLevel }}</td>
          <td>{{ checkIn.totalPulleyDataLeftDistance }}</td>
          <td>{{ checkIn.totalPulleyDataRightDistance }}</td>
          <td>{{ checkIn.armAssistLeft }}</td>
          <td>{{ checkIn.armAssistRight }}</td>
          <td>{{ checkIn.armCartLeft }}</td>
          <td>{{ checkIn.armCartRight }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import DeviceService from "../services/DeviceService.js";

export default {
  name: "machine-check-ins",
  props: {
    checkIn: Object,
  },
  data() {
    return {
      checkIns: [],
      filter: {
        auditLogId: "",
        propertyName: "",
        lastCheckInTimeUtc: "",
        serial: "",
        name: "",
        machineModelId: "",
        organization: "",
        batteryLevel: "",
      },
    };
  },
  created() {
    DeviceService.getMachineCheckIns()
      .then((response) => {
        this.checkIns = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  computed: {
    filteredList() {
      let filteredCheckIns = this.checkIns;

      if (this.filter.auditLogId != "") {
        filteredCheckIns = filteredCheckIns.filter(
          (chkin) => chkin.auditLogId == this.filter.auditLogId
        );
      }

      if (this.filter.propertyName != "") {
        filteredCheckIns = filteredCheckIns.filter((chkin) =>
          chkin.propertyName
            .toLowerCase()
            .includes(this.filter.propertyName.toLowerCase())
        );
      }
      if (this.filter.lastCheckInTimeUtc != "") {
        filteredCheckIns = filteredCheckIns.filter((chkin) =>
          chkin.lastCheckInTimeUtc
            .toLowerCase()
            .includes(this.filter.lastCheckInTimeUtc.toLowerCase())
        );
      }

      if (this.filter.serial != "") {
        filteredCheckIns = filteredCheckIns.filter((chkin) =>
          chkin.serial.toLowerCase().includes(this.filter.serial.toLowerCase())
        );
      }
      if (this.filter.name != "") {
        filteredCheckIns = filteredCheckIns.filter((chkin) =>
          chkin.name.toLowerCase().includes(this.filter.name.toLowerCase())
        );
      }
      if (this.filter.machineModelId != "") {
        filteredCheckIns = filteredCheckIns.filter(
          (chkin) => chkin.machineModelId == this.filter.machineModelId
        );
      }
      if (this.filter.organization != "") {
        filteredCheckIns = filteredCheckIns.filter((chkin) =>
          chkin.organization
            .toLowerCase()
            .includes(this.filter.organization.toLowerCase())
        );
      }

      if (this.filter.batteryLevel != "") {
        filteredCheckIns = filteredCheckIns.filter(
          (chkin) => chkin.batteryLevel <= this.filter.batteryLevel
        );
      }

      return filteredCheckIns;
    },
  },
};
</script>

<!-- 
INCLUDE HEALTH APPROVED COLORS

#E97A7A - Error
#41C0CB - Brand Teal
#2DACB7 - Web Teal
#F3F3F3 - Light Gray
#81888B - Gray
#444444 - Dark Gray
#1C1C1C - Almost Black

FROM LOGO
#A1CC3A - Light Green
-->

<style>
#table-checkins {
  display: inline-block;
  margin-left: 77px;
  margin-top: 20px;
}
table {
  border-collapse: collapse;
  border: 1px solid #1c1c1c;
}
table th {
  border: 1px solid #1c1c1c;
  text-align: center;
  padding: 8px;
}
table td {
  border: 1px solid #1c1c1c;
  padding: 8px;
}
.input-cell {
  padding: 1px;
}
table tr:nth-child(odd) {
  background: #f3f3f3;
}
table tr:nth-child(even) {
  background: #d1d9dc;
}
.check-in-log-header{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding-left: 20px;
  padding-right: 20px;
  padding-top: 10px;
  padding-bottom: 8px;
  border-bottom: 5px solid #2dacb7;
}
.header {
  text-align: center;
  font-size: 2rem;
  color: #444444;
  margin-left: 57px;
}
</style>