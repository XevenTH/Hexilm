import { createContext, useContext } from "react";
import MovieStore from "./MovieStore";

interface Store {
    MovieStore: MovieStore
}

export const storeContainer: Store = {
    MovieStore: new MovieStore(),
}

export const BaseStoreContext = createContext(storeContainer);

export function UseStore() {
    return useContext(BaseStoreContext);
}