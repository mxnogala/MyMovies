<template>
  <b-container>
    <div>
      <b-button variant="secondary" @click="getAction('add', null)">Add new movie</b-button>
      <MovieDetails :title="title" :refresh_method="getAllMovies" :disabled="disabled" :action="action"/>
    </div>
    <b-table striped hover :items="movies" :fields="fields" >
      <template v-slot:cell(edit)="row">
          <b-button variant="outline-success"  @click="getAction('edit', row.item.id)"><b-icon-pencil></b-icon-pencil></b-button>
      </template>
      <template v-slot:cell(remove)="row">
        <b-button variant="outline-danger" @click="removeMovie(row.item.id)">
        <b-icon-trash></b-icon-trash>
        </b-button>
      </template>
      <template v-slot:cell(show)="row">
        <b-button variant="outline-secondary" @click="getAction('show', row.item.id)">
        <b-icon-eye></b-icon-eye>
        </b-button>
      </template>
    </b-table>
  </b-container>
</template>

 <script>
import {ApiClient} from '@/service/ApiClient.js';
import { mapGetters } from 'vuex'
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
      fields: ['id', 'title', 'releaseDate', 'show', 'edit', 'remove'],
      title: "",
      addTitle: "Add new movie",
      editTitle: "Edit movie",
      showTitle: "Movie details",
      movie_id: null,
      disabled: false,
      action: "add",
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

      getMovieById(id) {
      console.log(id);
      ApiClient.getById(id).then((response) => {
        this.$store.state.movie = response.data;
        console.log(this.movie);
      })
    },

    getAction(action, id) {
      if (action == 'show' && id != null) {
        this.disabled = true;
        this.title = this.showTitle;
        this.action = "show";
        this.getMovieById(id);
      }
      if (action == 'edit' && id != null) {
        this.disabled = false;
        this.title = this.editTitle;
        this.action = "edit";
        this.getMovieById(id);
      }
      else if (action == 'add' && id == null) {
        this.diabled = false;
        this.title = this.addTitle;
        this.action = "add";
        this.$store.state.movie = new Movie();
      }

      this.$bvModal.show('movie-modal');
    },

  },
 computed: {
    ...mapGetters({
      movie: "movie",
    }),
    },

  created() {
    this.getAllMovies();
  }
}
</script>