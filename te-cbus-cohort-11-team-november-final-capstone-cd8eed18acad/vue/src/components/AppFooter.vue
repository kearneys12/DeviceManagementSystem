<template>
  <footer>
    <img
      class="logo-img, d-inline-block align-top"
      src="../assets/images/Include_Health_logo_with_company_name.png"
      width="300"
      height="50"
      alt="Logo"
    />
    <h5 class="title">All Rights Reserved 2020</h5>
  </footer>
</template>

<script>
export default {
  name: "AppFooter",
  data() {
    return {};
  },
};
</script>

<style scoped>
footer {
  height: 86px;
  background-color: #f3f3f3;
  color:#444444 ;
  grid-area: navBar;
  text-align: center;
    -webkit-box-shadow: 0 -4px 5px rgba(0,0,0,.3);
    -moz-box-shadow: 0 -4px 5px rgba(0,0,0,.3);
    box-shadow: 0 -4px 5px rgba(0,0,0,.3);
    position : relative;
}
img {
  padding-top: 5px;
}
</style>