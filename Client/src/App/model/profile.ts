import { Movie } from "./movie";
import Photo from "./photo";

export interface Profile {
  displayName: string;
  userName: string;
  bio: string;
  photos: Photo[]
  favoriteMovies: Movie[]
}

export class Profile implements Profile {
  constructor(init?: InitialEditProfile) {
    Object.assign(this, init);
  }
}

export class InitialEditProfile implements Pick<Profile, "displayName" | "userName" | "bio" > {
  displayName: string = '';
  userName: string = '';
  bio: string = '';

  constructor(request?: InitialEditProfile) {
    if (request) {
      this.displayName = request.displayName;
      this.userName = request.userName;
      this.bio = request.bio;
    }
  }
}