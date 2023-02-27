import { observer } from "mobx-react-lite"
import ReuseableForm from "../../../App/common/ReuseableForm"
import { Movie } from "../../../App/model/movie"
import { UseStore } from "../../../App/Stores/BaseStore"
import MovieList from "../Browse/MovieList"
import EditCompo from "./edit"
import MovieDetails from "./MovieDetails"

export default observer(function Dashboard() {
    const { MovieStore } = UseStore();
    const { movieList: movie, isOpenDetails, isOpenForm, CloseDetailsResetMovie } = MovieStore

    return (
        <div>
            <ul>
                {movie.map(x => (
                    <li key={x.id}>
                        <MovieList movie={x} />
                    </li>
                ))}
            </ul>
            <button className="bg-red-200 p-2 rounded-xl shadow-md font-semibold capitalize w-fit ml-4" onClick={() => CloseDetailsResetMovie(true)}>
                New Movie
            </button>
            {isOpenDetails &&
                <MovieDetails />
            }
            {isOpenForm &&
                <EditCompo />
            }
        </div>
    )
})