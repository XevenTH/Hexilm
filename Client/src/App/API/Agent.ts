import axios, { AxiosResponse } from "axios";
import { Movie } from "../model/movie";
import { User, UserLogin, UserRegister } from "../model/user";
import { storeContainer } from "../Stores/BaseStore";

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.request.use(opt => {
    const token = storeContainer.CommonStore.token;
    if(token) opt.headers!.Authorization = `Bearer ${token}`;

    return opt;
})

const responsePayload = <T>(res: AxiosResponse<T>) => res.data

const request = {
    get: <T>(url: string) => axios.get<T>(url).then(responsePayload),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responsePayload),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responsePayload),
    delete: <T>(url: string) => axios.delete<T>(url).then(responsePayload)
};

const movieApi = {
    getMovieList: () => request.get<Movie[]>("/movie"),
    postMovie: (movie: Movie) => request.post<void>("/movie", movie),
    editMovie: (movie: Movie) => request.put<void>(`/movie/${movie.id}`, movie),
    deleteMovie: (id: string) => request.delete<void>(`/movie/${id}`)
};

const accountApi = {
    getUser: () => request.get<User>("/account"),
    login: (loginData: UserLogin) => request.post<User>("/account/login", loginData),
    register: (registerData: UserRegister) => request.post<User>("/account/register", registerData)
}

const ApiAgent = {
    movieApi,
    accountApi,
};

export default ApiAgent;