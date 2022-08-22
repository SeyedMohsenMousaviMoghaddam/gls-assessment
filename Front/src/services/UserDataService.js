import http from "../http-common";

class UserDataService {
  getAll(data) {
    return http.post("/User/GetAll",data);
  }
  getAllLog(data) {
    return http.post("/User/GetAll",data);
  }
  get(id) {
    return http.get(`/User/GetById/${id}`);
  }

  create(data) {
    return http.post("/User/Save", data);
  }

  update(id, data) {
    return http.put(`/User/Save/${id}`, data);
  }

  delete(id) {
    return http.delete(`/User/Delete/${id}`);
  }

  findByTitle(title) {
    return http.get(`/User/GetById?title=${title}`);
  }

  loginUser(data) {
    return http.post(`/Accounts/LoginUser`, data);
  }  
}

export default new UserDataService();