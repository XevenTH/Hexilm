import { makeAutoObservable, runInAction } from 'mobx'
import uuid from 'react-uuid'
import ApiAgent from '../API/Agent'
import InitialMovie, { Movie } from '../model/movie'

export default class MovieStore {
  movieList: Movie[] = []
  selectedMovie: Movie | undefined = undefined
  isOpenDetails: boolean = false
  isOpenForm: boolean = false
  isLoadingMovie: boolean = true

  constructor() {
    makeAutoObservable(this)
  }

  getMovie = async () => {
    this.isLoadingMovie = true
    try {
      let result = await ApiAgent.movieApi.getMovieList()
      runInAction(() => {
        this.movieList = result
      })
    } catch (error) {
      runInAction(() => {
        console.log(error)
      })
    } finally {
      runInAction(() => {
        this.isLoadingMovie = false
      })
    }
  }

  deleteMovie = async (id: string) => {
    try {
      await ApiAgent.movieApi.deleteMovie(id)

      runInAction(() => {
        this.movieList = [...this.movieList.filter((x) => x.id !== id)]

        if (id === this.selectedMovie?.id) {
          this.isOpenDetails = false
        }
      })
    } catch (error) {
      console.log(error)
    }
  }

  CreateNewMovie = async (movie: Movie) => {
    try {
      let newMovie = new InitialMovie(movie)
      newMovie.id = uuid()
      await ApiAgent.movieApi.postMovie(newMovie)

      runInAction(() => {
        this.movieList = [...this.movieList, movie]
      })
    } catch (error) {
      console.log(error)
    }
  }

  EditMovie = async (movie: Movie) => {
    try {
      await ApiAgent.movieApi.editMovie(movie)

      runInAction(() => {
        this.movieList = [
          ...this.movieList.filter((x) => x.id !== movie.id),
          movie,
        ]
      })
    } catch (error) {
      console.log(error)
    }
  }

  selectedMovieHandler = async (id: string) => {
    this.isLoadingMovie = true

    if (!this.movieList.length) {
      await this.getMovie()
    }
    let movie = this.movieList.find((x) => x.id === id)

    if (!movie) throw new Error('not found')

    this.selectedMovie = movie
    this.isOpenDetails = true
    this.isOpenForm = false
    this.isLoadingMovie = false
  }

  CloseDetailsResetMovie = (state: boolean) => {
    this.OpenFormCloseDetailsHandler(state)
    this.selectedMovie = undefined
  }

  OpenFormCloseDetailsHandler = (state: boolean) => {
    this.isOpenForm = state
    this.isOpenDetails = false
  }
}
