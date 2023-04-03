export interface Movie {
  id: string
  title: string
  picture: string
}

export default class InitialMovie {
  id: string = ""
  title: string = ""
  picture: string = ""

  constructor(value?: InitialMovie) {
    if (value) {
      this.id = value.id
      this.title = value.title
      this.picture = value.picture
    }
  }
}
