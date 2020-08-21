<!-- Main Application Template. 
     Note that you have classes from bootstrap available to you.
     See https://getbootstrap.com/docs/4.0/components/alerts/ for reference on bootstrap -->

<template>
  <div id="app">
    <app-header />
    <div class="pageRoot">
      <router-view />
    </div>
    <app-footer />
  </div>
</template>

<script>
import AppHeader from "./components/AppHeader.vue";
import AppFooter from "./components/AppFooter.vue";

export default {
  name: "App",

  components: {
    AppHeader,
    AppFooter
  },
};
</script>

<!-- Application-Wide Styles go here 

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
body {
  background-color: #F3F3F3  ;
  font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; 
  color: #1C1C1C;
}
img.logo-img {
  max-width: 100%;
  height: auto;
}

</style>