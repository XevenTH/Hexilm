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
