import { makeAutoObservable, runInAction } from "mobx"
import ApiAgent from "../API/Agent"
import { InitialEditProfile, Profile } from "../model/profile"
import { storeContainer } from "./BaseStore";

export default class ProfileStore {
  profile: Profile | undefined = undefined;

  constructor() {
    makeAutoObservable(this)
  }

  getProfile = async (username: string) => {
    if (this.profile) {
      return this.profile;
    }

    try {
      const data = await ApiAgent.profileApi.getProfile(username);
      runInAction(() => {
        this.profile = data;
      });
      return this.profile;
    } catch (error) {
      console.log(error)
    }
  }

  editProfile = async (editData: InitialEditProfile) => {
    try {
      await ApiAgent.profileApi.editProfile(editData)

      runInAction(() => {
        if (!this.profile) return;
        if (!storeContainer.UserStore.User) return;

        // Check if username has changed
        if (this.profile.userName !== editData.userName) {
          localStorage.removeItem('jwt')
        }

        // Update profile with new data
        Object.assign(this.profile, editData)

        // Update user with new data
        Object.assign(storeContainer.UserStore.User, editData)

        // Update edited profile in localStorage
        localStorage.setItem('profile', JSON.stringify(this.profile))
      })
    } catch (error) {
      console.log(error)
    }
  }

}
