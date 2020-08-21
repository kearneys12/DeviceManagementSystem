<template>
<div class="machine-check-ins">
<h1>Device List with All Device Data</h1>
<check-in-log />
</div>
</template>

<script>
import CheckInLog from "../component/CheckInLog.vue";

export default {
    name: "machine-check-ins",
    components: {
        CheckInLog,
    },
};
</script>

<style>
</style>

