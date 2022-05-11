<template>
  <b-container>
    <div>
      <b-button variant="outline-primary" v-b-modal="'movie-modal'">Add new movie</b-button>
      <MovieDetails />
    </div>
    <b-table striped hover :items="movies" :fields="fields" ></b-table>
  </b-container>
</template>
<script>
import {apiClient} from '/http_common.js';
import Movie from '@/models/Movie';
import MovieDetails from "@/components/MovieDetails";

export default {
  name: 'MovieList',
  components: {
    MovieDetails
  },
  data() {
    return {
      movies: [],
      errors: [], 
      fields: ['id', 'title', 'releaseDate'],
    }
  },

  methods: {
    getAllMovies() {
      apiClient.get().then(response => {
        response.data.forEach((movie) => {
          this.movies.push(new Movie(movie));
        });
      })
      .catch(e => {
        this.errors.push(e)
      });
    }
  },

  created() {
    this.getAllMovies();
  }
}
</script>