<template>
  <header>
    <nav class="navbar navbar-light fixed-top">
      <router-link v-bind:to="{name: 'home'}" class="navbar-brand" href="#">
        <img
          class="logo-img, d-inline-block align-top"
          src="../assets/images/Include_Health_logo_with_company_name.png"
          width="300"
          height="50"
          alt="Logo"
        />
      </router-link>
      <div class="nav-buttons">
        <router-link v-bind:to="{name: 'home'}" exact>Devices</router-link>
        <router-link v-bind:to="{name: 'machineCheckins'}" exact>&nbsp;|&nbsp;Check-In Log</router-link>
        <router-link v-bind:to="{name: 'register'}" v-if="!$store.state.token">&nbsp;|&nbsp;Register</router-link>
        <router-link v-bind:to="{name: 'login'}" v-if="!$store.state.token">&nbsp;|&nbsp;Login</router-link>
        <router-link v-bind:to="{name: 'logout'}" v-if="$store.state.token">&nbsp;|&nbsp;Logout</router-link>
      </div>
    </nav>
  </header>
</template>

<script>
export default {
  name: "AppHeader",
  data() {
    return {};
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

<style scoped>
header {
  height: 86px;
  color: #81888b;
  grid-area: navBar;
  text-align: center;
}
nav {
  background-color: #F3F3F3;
  box-shadow: 0 1px 5px 0 rgba(0,0,0,.3);
}
.nav-buttons {
  display: flex;
  justify-content: flex-end;
  flex-direction: row;
  align-items: center;
}
.nav-buttons a {
  color:#444444 ;
  font-size: 18px;
}
</style>