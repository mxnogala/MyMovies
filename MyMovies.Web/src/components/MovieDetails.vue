<template>
  <b-modal id="movie-modal" :title="title" hide-footer>
    <b-form @submit="onSubmit">
      <div class="errorsTitle">
        <div class="error" v-if="!$v.movie.title.required">Title is required</div>
        <div class="error" v-if="!$v.movie.title.maxLength">Title can only have 200 characters.</div>
      </div>
      <b-form-group id="title" label="Title:" label-for="title-input" class="pb-3">
        <b-form-input
          id="title-input"
          type="text"
          placeholder="Enter title"
          v-model.trim="$v.movie.title.$model"
          :disabled="disabled"
          @input="$v.movie.title.$touch()"
          @blur="$v.movie.title.$touch()"
        ></b-form-input>
      </b-form-group>
      <div class="errorsDate">
        <div class="error" v-if="!$v.movie.releaseDate.numeric">Release date must be numeric.</div>
        <div class="error" v-if="!$v.movie.releaseDate.minValue || !$v.movie.releaseDate.maxValue">Release date must be between 1900 and 2100</div>
      </div>
      <b-form-group id="release-date" label="Release date:" label-for="release-input" class="pb-5">
        <b-form-input
          id="release-input"
          v-model="movie.releaseDate"
          placeholder="Enter release date"
          :disabled="disabled"
        ></b-form-input>
      </b-form-group>
      <b-button 
        class="mt-3" 
        type="submit" 
        variant="outline-success" 
        v-if="action != 'show'" 
        :disabled="$v.movie.$invalid">Submit</b-button>
    </b-form>  
  </b-modal>
</template>

<script>
import { mapGetters } from 'vuex'
import MovieRequest from "@/service/MovieRequest.js";
import {
  required,
  minValue,
  maxValue,
  maxLength,
  numeric,
} from "vuelidate/lib/validators";

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
    disabled: Boolean,
    action: String,
    },
    
  validations: {
    movie: {
      title: {
        required,
        maxLength: maxLength(200),
      },
      releaseDate: {
        numeric,
        minValue: minValue(1900),
        maxValue: maxValue(2100),
      },
    },
  },

  methods: {
    addMovie() {
      delete this.movie.id;
      MovieRequest().addItem(this.$store.state.movie).then((response) => {
        if (response.status == 200) {
          this.$emit('show-alert', 'success', 'Movie added.');
          this.refresh_method();
          this.$bvModal.hide('movie-modal');
        }
        else {
          this.$emit('show-alert', 'error', 'Movie cannot be added. Please try later.');
          this.$bvModal.hide('movie-modal');
        }
      },
      (error) => {
        this.$emit('show-alert', 'danger', `Movie cannot be added. Please try later. ${error}`);
        this.$bvModal.hide('movie-modal');
      })
    },

    editMovie() {
      console.log(this.$store.state.movie);
      MovieRequest().updateItem(this.$store.state.movie).then((response) => {
        if (response.status == 200)
        {
          this.refresh_method();
          this.$emit('show-alert', 'success', 'Movie edited.');
          this.$bvModal.hide('movie-modal');
        }
        else {
          this.$emit('show-alert', 'error', `Movie cannot be edited. Please try later. ${error}`);
          this.$bvModal.hide('movie-modal');
        }
      },
      (error) => {
        this.$emit('show-alert', 'danger', `Movie cannot be edited. Please try later. ${error}`);
        this.$bvModal.hide('movie-modal');
      })
    },

    onSubmit() {
      if (this.action == "add") {
        this.addMovie();
      }
      else if (this.action == "edit") {
        this.editMovie();
      }
    },

  },

  computed: {
    ...mapGetters({
      movie: "movie",
    }),
    
  },
}
</script>

<style scoped>
.error {
  color: red;
}
.errorsTitle {
  height: 25px;
}
.errorsDate {
  height: 50px;
}
</style>
