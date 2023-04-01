import axios, { AxiosResponse } from "axios"
import { Movie } from "../model/movie"
import { User, Credentials } from "../model/user"
import { Room } from "../model/userRoom"
import { storeContainer } from "../Stores/BaseStore"

axios.defaults.baseURL = "http://localhost:5000/api/v2"

axios.interceptors.request.use((opt) => {
  const token = storeContainer.CommonStore.token
  if (token) opt.headers!.Authorization = `Bearer ${token}`

  return opt
})

const responsePayload = <T>(res: AxiosResponse<T>) => res.data

const request = {
  get: <T>(url: string) => axios.get<T>(url).then(responsePayload),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responsePayload),
  put: <T>(url: string, body: {}) =>
    axios.put<T>(url, body).then(responsePayload),
  delete: <T>(url: string) => axios.delete<T>(url).then(responsePayload),
}

const movieApi = {
  getMovieList: () => request.get<Movie[]>("/movies"),
  getMovie: (id: string) => request.get<Movie | null>(`/movies/${id}`),
  postMovie: (movie: Movie) => request.post<void>("/movies", movie),
  editMovie: (movie: Movie) => request.put<void>(`/movies/${movie.id}`, movie),
  deleteMovie: (id: string) => request.delete<void>(`/movies/${id}`),
}

const accountApi = {
  getUser: () => request.get<User>("/account"),
  login: (loginData: Pick<Credentials, "email" | "password">) =>
    request.post<User>("/account/login", loginData),
  register: (registerData: Credentials) =>
    request.post<User>("/account/register", registerData),
}

const userRoomAPi = {
  getRoomList: () => request.get<Room[]>("/rooms"),
}

const ApiAgent = {
  movieApi,
  accountApi,
  userRoomAPi,
}

export default ApiAgent
