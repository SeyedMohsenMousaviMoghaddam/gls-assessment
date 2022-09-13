import httpToken from "../http-common-token";

class RoleDataService {
  getAll(data) {
    return httpToken.post("/Role/GetAll",data);
  }

  get(id) {
    return httpToken.get(`/Role/GetById/${id}`);
  }

  create(data) {
    return httpToken.post("/Role/Save", data);
  }

  update(id, data) {
    return httpToken.put(`/Role/Save/${id}`, data);
  }

  delete(id) {
    return httpToken.delete(`/Role/Delete/${id}`);
  }

  findByTitle(title) {
    return httpToken.get(`/Role/GetById?title=${title}`);
  }

}

export default new RoleDataService();