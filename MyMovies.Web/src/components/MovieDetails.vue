   
   <template>
      <b-modal id="movie-modal">Hello From My Modal!</b-modal>
  </template>

<script>

export default ({
 name: "MovieDetails",
 data() {
    return {
    };
  },
  props: {
    modalShow: Boolean,
  },
})
</script>
