import { ApiClient } from "@/service/ApiClient";

export default {
  get() {
    return ApiClient.request(ApiClient.get);
  },
  getById(id) {
    return ApiClient.request(ApiClient.getById, id);
  },
  add(data) {
    return ApiClient.request(ApiClient.post, data);
  },
  delete(id) {
    return ApiClient.request(ApiClient.delete, id);
  },
  update(data) {
    return ApiClient.request(ApiClient.put, data);
  },
};
