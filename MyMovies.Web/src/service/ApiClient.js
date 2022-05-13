import axios from "axios";
var baseURL = `http://localhost:5000/api/movie/`;
axios.defaults.baseURL = baseURL;
axios.defaults.withCredentials = false;
axios.defaults.headers.common["Accept"] = "application/json";
axios.defaults.headers.common["ContentType"] = "application/json";

export const ApiClient = {
  post(data) {
    return axios.post(baseURL, data);
  },
  get() {
    return axios.get();
  },
  getById(id) {
    return axios.get(baseURL + id);
  },
  delete(id) {
    return axios.delete(baseURL + id);
  },
  put(data) {
    return axios.put(baseURL, data);
  },
  request(method, data) {
    return new Promise((resolve, reject) => {
      method(data)
        .then((response) => {
          resolve(response);
        })
        .catch((e) => {
          reject(e);
        });
    });
  },
};
