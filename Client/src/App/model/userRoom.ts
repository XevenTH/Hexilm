import { Movie } from "./movie"
import { User } from "./user"
import InitialMovie from "./movie"

export interface Room {
  id: string
  title: string
  movie: Movie
  attendees: Pick<User, "displayName" | "username" | "photo">[]
}

export class InitialRoom implements Room {
  id = ""
  title = ""
  movie = new InitialMovie()
  attendees : Pick<User, "displayName" | "userName" | "photo">[] = []

  constructor(value?: Room) {
    if (value) {
      this.id = value.id
      this.title = value.title
      this.movie = value.movie
      this.attendees = value.attendees
    }
  }

}
