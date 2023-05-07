import { makeAutoObservable, runInAction } from "mobx";
import ApiAgent from "../API/Agent";
import { Profile } from "../model/profile";
import { storeContainer } from "./BaseStore";
import Photo from "../model/photo";

export default class PhotoStore {
  profile: Profile | undefined = undefined;
  photoList: Photo[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  changePhoto = async (publicId: string) => {
    try {
      await ApiAgent.photosApi.changePhoto(publicId);

      runInAction(() => {
        if (!this.profile) return;
        if (!storeContainer.UserStore.User) return;

        Object.assign(this.profile, publicId);
        Object.assign(storeContainer.UserStore.User, publicId);
      });
    } catch (error) {
      console.log(error);
    }
  };
  uploadPhoto = async (uploadPhoto: Blob) => {
    try {
      const dataPhoto = await ApiAgent.photosApi.uploadPhoto(
        uploadPhoto
      );
      return dataPhoto;
    } catch (error) {
      console.log(error);
    }
  };
  savePhoto = async (publicId: string, url: string, userId: string | undefined) => {
    try {
       ApiAgent.photosApi.savePhoto(
        publicId,
        url,
        userId
      );
    } catch (error) {
      console.log(error);
    }
  };
}
