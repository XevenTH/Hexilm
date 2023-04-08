import { makeAutoObservable, runInAction } from "mobx"
import ApiAgent from "../API/Agent"
import { InitialEditProfile, Profile } from "../model/profile"

export default class ProfileStore {
  profile: Profile | null = null;

  constructor() {
    makeAutoObservable(this)
  }

  getProfile = async (username:string) => {
    try {
      const data = await ApiAgent.profileApi.getProfile(username);
      runInAction(() => {
        this.profile = data;
      });
    } catch (error) {
      console.log(error)
    }
  }
  editProfile = async (profile: InitialEditProfile) => {
    if (!profile) {
      console.log("Please provide a valid profile object.");
      return;
    }
    await ApiAgent.profileApi.editProfile(profile);
    try {
      await runInAction(async() => {
        
      });
    } catch (error) {
      console.log(error);
    }
  }
}