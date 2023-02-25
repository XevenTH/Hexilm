import ReuseableForm from "../../../App/common/ReuseableForm"
import { Movie } from "../../../App/model/movie"
import MovieList from "../Browse/MovieList"
import EditCompo from "./edit"
import MovieDetails from "./MovieDetails"

interface props {
    movie: Movie[],
    selectedMovie: Movie | undefined,
    openForm: boolean
    openDetails: boolean
    deleteMovie: (id: string) => void,
    selectedMovieHandler: (id: string) => void,
    EditCreateHandler: (movie: Movie) => void,
    CloseDetailsResetMovie: (state: boolean) => void,
    setOpenFormHandler: (state: boolean) => void
}


export default function Dashboard({ movie, deleteMovie, selectedMovieHandler, selectedMovie, EditCreateHandler, openForm, CloseDetailsResetMovie, setOpenFormHandler, openDetails }: props) {
    return (
        <div>
            <ul>
                {movie.map(x => (
                    <li key={x.id}>
                        <MovieList movie={x} deleteMovie={deleteMovie} selectedMovieHandler={selectedMovieHandler} />
                    </li>
                ))}
            </ul>
            <button className="bg-red-200 p-2 rounded-xl shadow-md font-semibold capitalize w-fit ml-4" onClick={() => CloseDetailsResetMovie(true)}>
                New Movie
            </button>
            {openDetails &&
                <MovieDetails selectedMovie={selectedMovie} setOpenFormHandler={setOpenFormHandler} />
            }
            {openForm && 
                <EditCompo selectedMovie={selectedMovie} EditCreateHandler={EditCreateHandler} setOpenFormHandler={setOpenFormHandler} />
            }
        </div>
    )
}