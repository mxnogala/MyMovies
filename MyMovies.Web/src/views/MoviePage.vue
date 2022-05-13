<template>
  <MovieList v-on="$listeners"/>
</template>
<script>
import MovieList from '@/components/MovieList'

export default {
  name: "MoviePage",
  components: {
    MovieList,
  },
};
</script>
