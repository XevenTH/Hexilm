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

        Object.assign(this.profile, editData)
        Object.assign(storeContainer.UserStore.User, editData)
      })
    } catch (error) {
      console.log(error)
    }
  }
}
