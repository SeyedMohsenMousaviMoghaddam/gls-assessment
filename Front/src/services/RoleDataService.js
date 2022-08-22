import http from "../http-common";

class RoleDataService {
  getAll(data) {
    return http.post("/Role/GetAll",data);
  }

  get(id) {
    return http.get(`/Role/GetById/${id}`);
  }

  create(data) {
    return http.post("/Role/Save", data);
  }

  update(id, data) {
    return http.put(`/Role/Save/${id}`, data);
  }

  delete(id) {
    return http.delete(`/Role/Delete/${id}`);
  }

  findByTitle(title) {
    return http.get(`/Role/GetById?title=${title}`);
  }

}

export default new RoleDataService();