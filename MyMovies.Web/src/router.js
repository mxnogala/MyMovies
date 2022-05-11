import Vue from "vue";
import Router from "vue-router";
import MoviePage from "@/views/MoviePage.vue";

Vue.use(Router);

const router = new Router({
  routes: [
    {
      path: "/",
      component: MoviePage,
    },
  ],
});

export default router;
