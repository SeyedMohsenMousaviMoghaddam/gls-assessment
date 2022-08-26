import axios from 'axios';

/*const config = {
  baseURL: process.env.VUE_APP_BASE_URLL,
  headers: {
    "Content-type": "application/json"
  }
}*/

const client = axios.create({
  baseURL: process.env.VUE_APP_BASE_URL
  /*headers: {
    "Content-type": "application/json"
  }*/
});

const authInterceptor = config => {
  config.headers.Authorization = `Bearer ${localStorage.getItem('access_token')}`
  config.headers.common.Accept = 'Application/json'
  config.headers['Access-Control-Allow-Origin'] = '*'
  return config
}


const loggerInterceptor = config =>

  config

client.interceptors.request.use(authInterceptor)
client.interceptors.request.use(loggerInterceptor)

client.interceptors.response.use(
  response => Promise.resolve(response),
  error => {
    /*if (error.response.status === 400) {
      throw error.response;
    }*/
    //Event.$emit('error', 500, error.response.data.message)
    
    if (error.response.status === 401) AuthService.logout();
    if(error.response.status == 403){
      refreshToken()
  }
    const errorMessage = error.response.data.message
    /*error.response.data.message = errorMessage.length > 200
      ? JSON.parse(errorMessage.split('code :').pop()).error.message.split(':')[0]
      : errorMessage*/
    throw error
    // Promise.reject(error)
  }
)

export default client
/*
export default axios.create({
  baseURL: process.env.VUE_APP_BASE_URL,
  headers: {
    "Content-type": "application/json"
  }
});
axios.interceptors.request.use(
  config => {
    config.headers['Authorization'] = `Bearer ${localStorage.getItem('access_token')}`;
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);
axios.interceptors.response.use(
  response => {
      return response;
  },
  error => {
      if(error.response.status == 403){
          refreshToken()
      }
      if(error.response.status == 400){
        console.log('gggg');
    }
  }
);*/