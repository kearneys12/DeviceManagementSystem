<template>
  <div class="card" style="width: 25rem;">
    <div class="flex-card-header-container">
      <img class="card-img-top" src="../assets/images/Include_Health_logo.png" alt="Card image cap" />
      <div class="card-body">
        <h5 class="card-title">{{device.name}}</h5>
        <p class="card-text">Serial: {{device.serial}}</p>
        <p class="card-text">Device Type: {{device.machineModelId}}</p>
      </div>
    </div>

    <ul class="list-group list-group-flush">
      <li class="list-group-item" id="connection-bad" v-if="device.connectionLost">CONNECTION LOST</li>
      <li class="list-group-item" id="connection-good" v-else>DEVICE ONLINE & PAIRED</li>

      <li
        class="list-group-item maintenance"
        v-bind:class="{'list-group-item maintenance bad' : (device.pastMaintenance)}"
      >
        Cable Travel Maintenance Trip Meter
        <div id="maintenance-flex-container">
          <div>Left: {{device.leftDistanceSinceMaintenance}}</div>
          <div>Right: {{device.rightDistanceSinceMaintenance}}</div>
        </div>
      </li>

      <li class="list-group-item" id="battery-box">
        Battery Level
        <div class="progress">
          <div
            class="progress-bar progress-bar-striped"
            v-bind:class="{'progress-bar progress-bar-striped batteryLow':(device.batteryLow)}"
            role="progressbar"
            aria-valuenow
            aria-valuemin="0"
            aria-valuemax="90"
            v-bind:style="batteryPercentage"
          >
            <span id="battery-percentage-number">{{Math.floor(device.batteryLevel)}}%</span>
          </div>
        </div>
      </li>

      <li class="list-group-item in-use" v-if="device.inUse">Currently In Use</li>
      <li class="list-group-item not-in-use" v-else>Not In Use</li>
    </ul>

    <div class="card-body">
      <button class="btn maintenance-button" v-on:click="sendSerial()">Maintenance Reset</button>
    </div>
  </div>
</template>


<script>
import DeviceService from "../services/DeviceService.js";

export default {
  name: "device-card",
  props: {
    device: Object,
  },
  data() {
    return {};
  },
  created() {},
  methods: {
    sendSerial() {
      DeviceService.sendSerial(this.device.serial)
        .then(() => {
          console.log("Maintenace Checkpoint Updated");
          window.location.reload();
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },
  computed: {
    batteryPercentage() {
      return "width: " + Math.floor(this.device.batteryLevel) + "%";
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
.card {
  border: 5px solid #1c1c1c;
  border-radius: 25px;
  margin: 20px;
  text-align: center;
  background-color: #81888b;
}
.btn {
  background-color: #2dacb7;
  color: #ffffff;
  padding-top: 0.5rem;
  padding-right: 1rem;
  padding-bottom: 0.5rem;
  padding-left: 1rem;
}
.maintenance-button {
  width: 12rem;
}
img.card-img-top {
  width: 5rem;
}
div.progress {
  border: 2px solid #1c1c1c;
  border-radius: 0.5rem;
  height: 1.5rem;
}
div.progress-bar.progress-bar-striped {
  background-color: #a1cc3a;
}
div.progress-bar.progress-bar-striped.batteryLow {
  background-color: #e97a7a;
}
.flex-card-header-container {
  display: flex;
  align-items: center;
  margin-left: 2.5rem;
}
.list-group-item {
  margin: 11px;
  border-radius: 1rem !important;
  background-color: #f3f3f3;
  border: 2px solid #1c1c1c !important;
}
#connection-bad {
  background-color: #e97a7a;
}
#connection-good {
  background-color: #A1CC3A;
}
#maintenance-flex-container {
  display: flex;
  justify-content: space-between;
}
.list-group-item.maintenance {
  background-color: #A1CC3A;
}
.list-group-item.maintenance.bad {
  background-color: #e97a7a;
}
li.list-group-item div.progress {
  background-color: #f3f3f3;
}
.progress-bar {
  color: #1c1c1c;
  font-weight: bold;
}
#battery-box {
  background-color: #81888b;
}
.list-group-item.in-use {
  background-color: #a1cc3a;
}
.list-group-item.not-in-use{
  background-color: #81888B;
}





</style>