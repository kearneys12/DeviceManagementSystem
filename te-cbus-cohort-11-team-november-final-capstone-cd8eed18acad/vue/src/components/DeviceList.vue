<template>
  <div>
    <div class="btn-container">
      <div class="header">Device Dashboard</div>
      <div class="dropdown-container">
        <select id="orgFilter" v-model="filter.status">
          <option value>Organization - Show All</option>
          <option value="Organization 1">Organization 1</option>
          <option value="Organization 2">Organization 2</option>
          <option value="Organization 3">Organization 3</option>
          <option value="Organization 4">Organization 4</option>
          <option value="Organization 5">Organization 5</option>
        </select>
        <p class="space"></p>
        <select id="connectionFilter" v-model="filter.connectionStatus">
          <option value>Connection Status - Show All</option>
          <option v-bind:value="true">Connection Lost</option>
        </select>
        <button type="button" class="btn" @click="reloadPage()">Refresh</button>
      </div>
    </div>
    <div class="card-container">
      <p v-for="device in filteredDevices" v-bind:key="device.id" id="card-container">
        <device-card v-bind:device="device" />
      </p>
    </div>
  </div>
</template>

<script>
import DeviceService from "../services/DeviceService.js";
import DeviceCard from "../components/DeviceCard.vue";

export default {
  name: "device-list",
  components: {
    DeviceCard,
  },
  methods: {
    reloadPage() {
      window.location.reload();
    },
  },
  data() {
    return {
      devices: [],
      filter: {
        status: "",
        connectionStatus: "",
      },
    };
  },
  created() {
    DeviceService.getDevices()
      .then((response) => {
        this.devices = response.data;
      })
      .catch((error) => {
        console.error(error);
      });
  },
  computed: {
    orderedDevices() {
      // eslint-disable-next-line vue/no-side-effects-in-computed-properties
      return this.devices.sort((device1, device2) => {
        console.log(device1, device2);
        //if (device1.connectionLost && !device2.connectionLost) return 1; // Use device 1
        //if (device2.connectionLost && !device2.connectionLost) return -1; // Use device 2

        if (device1.batteryLow && !device2.batteryLow) return 1;
        // Use device 1
        else if (device2.batteryLow && !device2.batteryLow) return -1; // Use device 2

        return 1;
      });
    },
    filteredDevices() {
      let filteredDevices = this.devices;

      if (this.filter.status != "") {
        filteredDevices = filteredDevices.filter(
          (device) => device.organization === this.filter.status
        );
      }
      if (this.filter.connectionStatus == true) {
        console.log(this.device, this.filter);

        filteredDevices = filteredDevices.filter(
          (device) => device.connectionLost == this.filter.connectionStatus
        );
      }
      return filteredDevices;
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
#card-container {
  display: inline-block;
}
.header {
  text-align: center;
  font-size: 2rem;
  color: #444444;
  margin-left: 57px;
}
select {
  background-color: F3F3F3;
  color: 444444;
  padding-top: 0.5rem;
  padding-right: 1.5rem;
  padding-bottom: 0.5rem;
  padding-left: 1.5rem;
}
.btn {
  background-color: #2dacb7;
  color: #ffffff;
  padding-top: 0.5rem;
  padding-right: 1.5rem;
  padding-bottom: 0.5rem;
  padding-left: 1.5rem;
  border-radius: 1rem;
}
.btn-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding-left: 20px;
  padding-right: 20px;
  padding-top: 5px;
  padding-bottom: 10px;
  border-bottom: 5px solid #2dacb7;
}
#orgFilter {
  padding-right: 10px;
  border-radius: 1rem;
  color: #444444;
}
#connectionFilter {
  border-radius: 1rem;
  color: #444444;
}
.dropdown-container {
  display: flex;
}
.dropdown-container button {
  margin-left: 10px;
}
.space {
  width: 10px;
}
.card-container {
  margin-left: 57px;
}
</style>