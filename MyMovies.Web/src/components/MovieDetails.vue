<template>
      <b-modal id="movie-modal" :title="title" hide-footer>
         <b-form @submit="onSubmit">
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
          :disabled="disabled"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="release-date" label="Release date:" label-for="release-input">
        <b-form-input
          id="release-input"
          v-model="movie.releaseDate"
          placeholder="Enter release date"
          :disabled="disabled"
        ></b-form-input>
      </b-form-group>

      <b-button type="submit" variant="primary" v-if="action != 'show'">Submit</b-button>
    </b-form>  
  </b-modal>
</template>

<script>
import {ApiClient} from '@/service/ApiClient.js';
import Movie from '@/models/Movie';
import { mapGetters } from 'vuex'
import MovieRequest from "@/service/MovieRequest.js";

export default {
  name: 'MovieList',
  components: {
  },
  data() {
    return {
    }
  },
  props: {
    title:String,
    refresh_method: Function,
    movie_id: Number,
    set_default: Function,
    disabled: Boolean,
    action: String,
    },

  methods: {
    addMovie() {
      delete this.movie.id;
      MovieRequest().addItem(this.$store.state.movie).then((response) => {
        if (response.status == 200) {
          this.refresh_method();
          this.$bvModal.hide('movie-modal');
        }
      })
    },

    editMovie() {
      MovieRequest.updateItem(this.$store.state.movie).then((response) => {
        if (response.status == 200)
        {
          this.refresh_method();
          this.$bvModal.hide('movie-modal');
        }
      })
    },
    // editMovie() {
    //   ApiClient.put(this.$store.state.movie).then(response => {
    //     if (response.status == 200) {
    //       this.refresh_method();
    //       this.$bvModal.hide('movie-modal');
    //     }
    //   })
    // },

    onSubmit() {
      if (this.action == "add") {
        this.addMovie();
      }
      else if (this.action == "edit") {
        this.editMovie();
      }
    }

  },
      computed: {
    ...mapGetters({
      movie: "movie",
    }),
    
    },
 
}
</script>
