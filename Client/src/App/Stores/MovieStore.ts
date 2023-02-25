import { makeAutoObservable } from "mobx";

export default class MovieStore {
    movieList = [];

    constructor() {
        makeAutoObservable(this);
    }
}