<template>
  <div class="home">
   
    <device-list />
  </div>
</template>

<script>
import DeviceList from "../components/DeviceList.vue";

export default {
  name: "home",
  components: {
    DeviceList,
  },
  
};
</script>

<style>

</style>