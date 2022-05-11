export default class Movie {
  id = "";
  title = "";
  releaseDate = null;

  constructor(data) {
    if (data) {
      this.id = data.id ? data.id : "";
      this.title = data.title ? data.title : "";
      this.releaseDate = data.releaseDate ? data.releaseDate : "";
    }
  }
}
