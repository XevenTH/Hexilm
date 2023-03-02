import { makeAutoObservable, runInAction } from "mobx";
import uuid from "react-uuid";
import ApiAgent from "../API/Agent";
import NewMovie, { Movie } from "../model/movie";

export default class MovieStore {
    movieList: Movie[] = [];
    selectedMovie: Movie | undefined = undefined;
    isOpenDetails: boolean = false;
    isOpenForm: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    getMovie = async () => {
        try {
            let result = await ApiAgent.movieApi.getMovieList();
            runInAction(() => {
                this.movieList = result
            })
        } catch (error) {
            runInAction(() => {
                console.log(error);
            })
        }
    }

    deleteMovie = async (id: string) => {
        try {
            await ApiAgent.movieApi.deleteMovie(id);

            runInAction(() => {
                this.movieList = [...this.movieList.filter(x => x.id !== id)];

                if (id === this.selectedMovie?.id) {
                    this.isOpenDetails = false;
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    CreateNewMovie = async (movie: Movie) => {
        try {
            let newMovie = new NewMovie(movie);
            newMovie.id = uuid();
            await ApiAgent.movieApi.postMovie(newMovie);

            runInAction(() => {
                this.movieList = [...this.movieList, movie];
            })
        } catch (error) {
            console.log(error);
        }
    }

    EditMovie = async (movie: Movie) => {
        try {
            await ApiAgent.movieApi.editMovie(movie);

            runInAction(() => {
                this.movieList = [...this.movieList.filter(x => x.id !== movie.id), movie];
            })
        } catch (error) {
            console.log(error)
        }
    }

    selectedMovieHandler = (id: string) => {
        let movie = this.movieList.find(x => x.id === id);
        // console.log(movie);
        this.selectedMovie = movie;
        this.isOpenDetails = true;
        this.isOpenForm = false;;
    }

    CloseDetailsResetMovie = (state: boolean) => {
        this.OpenFormCloseDetailsHandler(state);
        this.selectedMovie = undefined;
    }

    OpenFormCloseDetailsHandler = (state: boolean) => {
        this.isOpenForm = state;
        this.isOpenDetails = false;
    }
}