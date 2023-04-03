import { Movie } from "./movie"
import { User } from "./user"

export interface UserRoom {
  id: string
  title: string
  movie: Movie
  attendees: Pick<User, "displayName" | "username" | "photo">[]
}
