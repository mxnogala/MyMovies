import axios from "axios";

export const apiClient = axios.create({
  baseURL: `http://localhost:5000/api/movie/`,
  headers: {
    "Content-type": "application/json",
  },
});
