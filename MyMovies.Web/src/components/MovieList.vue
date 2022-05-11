<template>
  <h1>{{movies}}</h1>
</template>
<script>
import {apiClient} from '/http_common.js';
import Movie from '@/models/Movie';

export default {
  name: 'MovieList',
  data() {
    return {
      movies: [],
      errors: []
    }
  },

  created() {
    apiClient.get()
    .then(response => {
      response.data.forEach((movie) => {
        this.movies.push(new Movie(movie));
      })
    })
    .catch(e => {
      this.errors.push(e)
    })
  }
}
</script>