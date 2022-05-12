<template>
      <b-modal id="movie-modal" :title="title" hide-footer @shown="getMovie" @hidden="set_default">
         <b-form @submit="addMovie">
           <!-- @submit="onSubmit" @reset="onReset" -->
      <b-form-group
        id="title"
        label="Title:"
        label-for="title-input"
      >
        <b-form-input
          id="title-input"
       
          type="text"
          placeholder="Enter title"
          v-model="movie.title"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="release-date" label="Release date:" label-for="release-input">
        <b-form-input
          id="release-input"
          v-model="movie.releaseDate"
          placeholder="Enter release date"
        ></b-form-input>
      </b-form-group>

      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>  
  </b-modal>
</template>

<script>
import {ApiClient} from '/http_common.js';
import Movie from '@/models/Movie';

export default {
  name: 'MovieList',
  components: {
  },
  data() {
    return {
      movie: new Movie,
    }
  },
  props: {
    title:String,
    refresh_method: Function,
    movie_id: Number,
    set_default: Function,
    },

  methods: {
    addMovie() {
      delete this.movie.id;
      ApiClient.post(this.movie).then(response => {
        if (response.status == 200) {
          this.refresh_method();
          this.$bvModal.hide('movie-modal')
          this.movie = new Movie()
        }        
      })
    },

    getMovieById(id) {
      console.log(id);
      ApiClient.getById(id).then((response) => {
        this.movie = response.data;
        console.log(this.movie);
      });
    },

    getMovie() {
      if (this.movie_id != null) {
        this.getMovieById(this.movie_id);
      }
      else {
        this.movie = new Movie();
      }
    },
    getDefaultData() {
      this.movie_id = null;
    }

  },
 
}
</script>
