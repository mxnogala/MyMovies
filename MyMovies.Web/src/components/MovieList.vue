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
import { mapGetters } from 'vuex'
import Movie from '@/models/Movie';
import MovieDetails from "@/components/MovieDetails";
import MovieRequest from "@/service/MovieRequest.js";

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
        MovieRequest().getItems().then((response) => {
          this.movies = response.data;
        });
    },

    removeMovie(id) {
      if (confirm("Are you sure you want to delete this movie?")) {
        MovieRequest().removeItem(id).then((response) => {
          console.log(response);
          this.getAllMovies();
        })
      }
    },

    getMovieById(id) {
      MovieRequest().getItem(id).then((response) => {
        console.log(id);
        console.log(response.data);
        this.$store.state.movie = new Movie(response.data);
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
        this.disabled = false;
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
  },

}
</script>