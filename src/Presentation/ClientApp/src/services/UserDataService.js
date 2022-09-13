import httpToken from "../http-common-token";
import http from "../http-common";


class UserDataService {
  getAll(data) {
    return httpToken.post("/User/GetAll",data);
  }
  getAllLog(id) {
    return httpToken.get(`/User/GetAllLog/${id}`);
  }
  get(id) {
    return httpToken.get(`/User/GetById/${id}`);
  }

  create(data) {
    return httpToken.post("/User/Save", data);
  }
  changePassword(data) {
    return httpToken.post("/User/ChangePassword", data);
  }
  changePasswordAllUser(data) {
    return httpToken.post("/User/ChangePasswordAllUser", data);
  }  
  update(id, data) {
    return httpToken.put(`/User/Save/${id}`, data);
  }

  delete(id) {
    return httpToken.delete(`/User/Delete/${id}`);
  }

  findByTitle(title) {
    return httpToken.get(`/User/GetById?title=${title}`);
  }

  loginUser(data) {
    return http.post(`/Accounts/Login`, data);
  }  
}

export default new UserDataService();