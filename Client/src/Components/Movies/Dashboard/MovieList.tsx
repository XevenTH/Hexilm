import { observer } from "mobx-react-lite";
import { Movie } from "../../../App/model/movie";
import { UseStore } from "../../../App/Stores/BaseStore";

interface Props {
    movie: Movie;
}

export default observer(function MovieList({ movie }: Props) {
    const { MovieStore: { deleteMovie, selectedMovieHandler } } = UseStore()

    return (
        <div className="max-w-lg bg-slate-300 max-h-96 p-2 m-4 rounded-md">
            {movie.title}
            <div className="bg-green-400 h-1 my-1"></div>
            <div>
                <button className="bg-red-500 m-1 p-1 rounded-xl shadow-md font-semibold text-white"
                    onClick={() => deleteMovie(movie.id)}>
                    Click to Delete </button>
                <button className="bg-blue-300 p-1 m-1 rounded-xl shadow-md font-semibold text-blue-800"
                    onClick={() => selectedMovieHandler(movie.id)}>
                    Select Movie 
                </button>
            </div>
        </div>
    )
})
