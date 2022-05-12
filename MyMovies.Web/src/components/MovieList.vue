<template>
  <b-container>
    <div>
      <b-button variant="secondary" v-b-modal="'movie-modal'">Add new movie</b-button>
      <!-- <b-modal id="movie-modal" :title="title" hide-footer @shown="getMovie(movie_id)"> -->
      <MovieDetails :title="title" :refresh_method="getAllMovies" :movie_id="movie_id" :set_default="getDefaultData"/>
      <!-- </b-modal> -->

    </div>
    <b-table striped hover :items="movies" :fields="fields" >
      <template v-slot:cell(edit) >
          <b-button variant="outline-success"><b-icon-pencil></b-icon-pencil></b-button>
      </template>
      <template v-slot:cell(remove)="row">
        <b-button variant="outline-danger" @click="removeMovie(row.item.id)">
        <b-icon-trash></b-icon-trash>
        </b-button>
      </template>
      <template v-slot:cell(show)="row">
        <b-button variant="outline-secondary" @click="showMovieDetails(row.item.id)">
        <b-icon-eye></b-icon-eye>
        </b-button>
      </template>
    </b-table>
  </b-container>
</template>
 <script>
import {ApiClient} from '/http_common.js';
// import Movie from '@/models/Movie';
import MovieDetails from "@/components/MovieDetails";
// import axios from "axios";

export default {
  name: 'MovieList',
  components: {
    MovieDetails
  },
  data() {
    return {
      movies: [],
      errors: [], 
      fields: ['id', 'title', 'releaseDate', 'show', 'edit', 'remove'],
      title: "Add new movie",
      movie_id: null,
    }
  },

  methods: {
    getAllMovies() {
      ApiClient.get().then(response => {
        this.movies = response.data;
      })
      .catch(e => {
        this.errors.push(e)
      });
    },

    removeMovie(id) {
      console.log(id);
      ApiClient.delete(id).then((response) => {
        console.log(response);
        this.getAllMovies();
      });
    },

    showMovieDetails(id) {
      this.title = "Movie details";
      this.movie_id = id;
      this.$bvModal.show('movie-modal');
    },
        getMovieById(id) {
      console.log(id);
      ApiClient.getById(this.movie_id).then((response) => {
        this.movie = response.data;
        console.log(this.movie);
      })
    },

    getMovie() {
       console.log(this.movie_id);
      if (this.movie_id != null) {
        this.getMovieById();
      }
    },
    getDefaultData() {
      this.movie_id = null;
      this.title="Add new movie";
    }

  },

  created() {
    this.getAllMovies();
  }
}
</script>