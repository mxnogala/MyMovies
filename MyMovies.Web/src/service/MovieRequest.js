import MovieClient from "@/service/MovieClient.js";

export default function movieRequest() {
  const getItems = async () => {
    return await MovieClient.get();
  };

  const getItem = async (id) => {
    if (id) {
      return await MovieClient.getById(id);
    }
  };

  const addItem = async (data) => {
    return await MovieClient.add(data);
  };

  const updateItem = async (data) => {
    return await MovieClient.update(data);
  };

  const removeItem = async (id) => {
    if (id) {
      return await MovieClient.delete(id);
    }
  };

  return {
    getItems,
    getItem,
    addItem,
    updateItem,
    removeItem,
  };
}
