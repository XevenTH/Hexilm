import { Movie } from "../../../App/model/movie"
import MovieList from "../Browse/MovieList"
import EditCompo from "./edit"

interface props {
    movie: Movie[],
    selectedMovie: Movie | undefined,
    deleteMovie: (id: string) => void,
    selectedMovieHandler: (id: string) => void,
    EditCreateHandler: (movie: Movie) => void,
    setOpenForm: (state: boolean) => void,
    openForm: boolean
}


export default function Dashboard({ movie, deleteMovie, selectedMovieHandler, selectedMovie, EditCreateHandler, openForm, setOpenForm }: props) {
    return (
        <div>
            <ul>
                {movie.map(x => (
                    <li key={x.id}>
                        <MovieList movie={x} deleteMovie={deleteMovie} selectedMovieHandler={selectedMovieHandler} />
                    </li>
                ))}
            </ul>
            {selectedMovie && openForm &&
                <EditCompo Movie={selectedMovie} EditCreateHandler={EditCreateHandler} selectedMovieHandler={selectedMovieHandler} setOpenForm={setOpenForm} />
            }
        </div>

    )
}