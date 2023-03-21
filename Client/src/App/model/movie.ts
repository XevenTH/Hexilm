export interface Movie {
  id: string
  title: string
  picture: string
}

export default class NewMovie {
  id: string = ""
  title: string = ""
  picture: string = ""

  constructor(value?: NewMovie) {
    if (value) {
      this.id = value.id
      this.title = value.title
      this.picture = value.picture
    }
  }
}
