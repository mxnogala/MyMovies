<template>
  <b-container class="py-3">
    <b-alert
      :show="dismissCountDown"
      :variant="variant"
      @dismissed="dismissCountDown=0"
      @dismiss-count-down="countDownChanged"
      class="m-3 px-5 pt-4"
    >
      <p>{{info}}</p>
    </b-alert>
    <router-view @show-alert="showAlert"/>
  </b-container>
</template>

<script>
  export default {
    name: "App",
    data() {
      return {
        dismissSecs: 6,
        dismissCountDown: 0,
        showDismissibleAlert: false,
        variant: "success",
        info: "",
      }
    },
    methods: {
      countDownChanged(dismissCountDown) {
        this.dismissCountDown = dismissCountDown
      },
      showAlert(variant, info) {
        this.dismissCountDown = this.dismissSecs
        this.variant = variant;
        this.info = info;
      }
    }
  }
</script>