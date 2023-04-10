export interface Profile {
  displayName: string;
  userName: string;
  bio: string;
  favoriteMovies: [
    {
      id: string;
      title: string;
      description: string;
      picture: string;
    }
  ];
}

export class InitialEditProfile {
  displayName= "";
  userName= "";
  bio= "";
  
  constructor(request?: InitialEditProfile) {
    if (request) {
      this.displayName = request.displayName;
    this.userName = request.userName;
    this.bio = request.bio;
    }    
  }
}