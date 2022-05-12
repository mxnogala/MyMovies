import axios from "axios";
var baseURL = `http://localhost:5000/api/movie/`;
axios.defaults.baseURL = baseURL;
axios.defaults.withCredentials = false;
axios.defaults.headers.common["Accept"] = "application/json";
axios.defaults.headers.common["ContentType"] = "application/json";

export const ApiClient = {
  configure(method, data) {
    if (method == "post") {
      var configData = {
        method: method,
        data: data,
      };
      return configData;
    } else {
      var config = {
        method: "get",
        params: {
          id: data,
        },
      };
      return config;
    }
  },

  post(data) {
    // var config = ApiClient.configure("post", data);
    // return axios(config);
    return axios.post(baseURL, data);
  },
  get() {
    var config = ApiClient.configure("get", null);
    return axios(config);
  },
  getById(id) {
    // var config = ApiClient.configure("get", id);
    console.log(baseURL + id);
    return axios.get(baseURL + id);
    // return axios(config);
  },
  delete(id) {
    return axios.delete(baseURL + id);
  },
};

// var config = {
//   method: 'post',
//   url: `http://localhost:5000/api/movie/`,
//   headers: {
//     "Content-type": "application/json",

//   },
//   data: this.movie
// };

// const ApiClient = {
//   connection: axios.create({
//     baseURL: `http://localhost:5000/api/movie/`,
//     headers: {
//       "Content-type": "application/json",
//     },
//   }),
//   post(data) {
//     return axios.post(this.connection, data);
//   },
// };

// export default ApiClient;
