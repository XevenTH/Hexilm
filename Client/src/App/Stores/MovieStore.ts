import axios from "axios";
import { makeAutoObservable, runInAction } from "mobx";
import uuid from "react-uuid";
import { Movie } from "../model/movie";

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
            await axios.get('http://localhost:5000/api/movie').then(res => this.movieList = res.data);
        } catch (error) {
            runInAction(() => {
                console.log(error);
            })
        }
    }

    deleteMovie = (id: string) => {
        this.movieList = [...this.movieList.filter(x => x.id !== id)];

        if (id === this.selectedMovie?.id) {
            this.isOpenDetails = false;
        }
    }

    selectedMovieHandler = (id: string) => {
        let movie = this.movieList.find(x => x.id === id);
        // console.log(movie);
        this.selectedMovie = movie;
        this.isOpenDetails = true;
        this.isOpenForm = false;;
    }

    EditCreateHandler = (movie: Movie) => {
        if (movie.id) {
            this.movieList = [...this.movieList.filter(x => x.id !== movie.id), movie];
        }
        else {
            movie.id = uuid();
            this.movieList = [...this.movieList, movie];
        }
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