import { createContext, useContext } from "react"
import CommonStore from "./CommonStore"
import { ModalStore } from "./ModalStore"
import MovieStore from "./MovieStore"
import { UserRoomStore } from "./UserRoomStore"
import UserStore from "./UserStore"
import ProfileStore from "./ProfileStore"
import PhotoStore from "./PhotoStore"

interface Store {
  MovieStore: MovieStore
  CommonStore: CommonStore
  UserStore: UserStore
  ModalStore: ModalStore
  UserRoomStore: UserRoomStore
  ProfileStore: ProfileStore,
  PhotoStore: PhotoStore
}

export const storeContainer: Store = {
  MovieStore: new MovieStore(),
  CommonStore: new CommonStore(),
  UserStore: new UserStore(),
  ModalStore: new ModalStore(),
  UserRoomStore: new UserRoomStore(),
  ProfileStore: new ProfileStore(),
  PhotoStore: new PhotoStore(),
}

export const BaseStoreContext = createContext(storeContainer)

export function UseStore() {
  return useContext(BaseStoreContext)
}
