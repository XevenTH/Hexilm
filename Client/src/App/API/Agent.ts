import axios, { AxiosResponse } from "axios";
import { Movie } from "../model/movie";
import { InitialEditProfile, Profile } from "../model/profile";
import { User, Credentials } from "../model/user";
import { Room } from "../model/userRoom";
import { storeContainer } from "../Stores/BaseStore";
import Photo from "../model/photo";

axios.defaults.baseURL = "http://localhost:5000/api/v2";

axios.interceptors.request.use((opt) => {
  const token = storeContainer.CommonStore.token;
  if (token) opt.headers!.Authorization = `Bearer ${token}`;

  return opt;
});

const responsePayload = <T>(res: AxiosResponse<T>) => res.data;

const request = {
  get: <T>(url: string) => axios.get<T>(url).then(responsePayload),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responsePayload),
  put: <T>(url: string, body: {}) =>
    axios.put<T>(url, body).then(responsePayload),
  delete: <T>(url: string) => axios.delete<T>(url).then(responsePayload),
};

const movieApi = {
  getMovieList: () => request.get<Movie[]>("/movies"),
  getMovie: (id: string) => request.get<Movie | null>(`/movies/${id}`),
  postMovie: (movie: Movie) => request.post<void>("/movies", movie),
  editMovie: (movie: Movie) => request.put<void>(`/movies/${movie.id}`, movie),
  deleteMovie: (id: string) => request.delete<void>(`/movies/${id}`),
};

const accountApi = {
  getUser: () => request.get<User>("/account"),
  login: (loginData: Pick<Credentials, "email" | "password">) =>
    request.post<User>("/account/login", loginData),
  register: (registerData: Credentials) =>
    request.post<User>("/account/register", registerData),
};

const userRoomAPi = {
  getRoomList: () => request.get<Room[]>("/rooms"),
  getRoom: (id: string) => request.get<Room | null>(`/rooms/${id}`),
};

const profileApi = {
  getProfile: (username: string) =>
    request.get<Profile>(`/profile/${username}`),
  editProfile: (editProfile: InitialEditProfile) =>
    request.put<void>("/profile", editProfile),
};

const photosApi = {
  changePhoto: (publicId: string) =>
    request.put<Photo>(`/photos/${publicId}/manage-main`, publicId),
  uploadPhoto: (File: Blob) => {
    let formData = new FormData();
    formData.append("File", File);
    return axios
      .post<Photo, any>("photos/upload?mode=square", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      })
      .then(async (response) => {
        return await response;
      })
  },
  savePhoto: (publicId: string, url: string, userId: string | undefined) => {
    axios.put(`photos/save?id=${userId}&to=user`, {
      PublicId: publicId,
      Url: url,
    });
  },
};

const ApiAgent = {
  movieApi,
  accountApi,
  userRoomAPi,
  profileApi,
  photosApi,
};

export default ApiAgent;
