import { Movie } from "../../../App/model/movie"

interface props {
    movie: Movie[],
    selectedMovie: Movie | undefined,
    deleteMovie: (id: string) => void,
    selectedMovieHandler: (id: string) => void,
}


export default function Dashboard({ movie, deleteMovie, selectedMovieHandler }: props) {
    return (
        <div>
            <ul>
                {movie.map(x => (
                    <li key={x.id}>
                        {x.title}
                        <button
                            onClick={() => deleteMovie(x.id)}>
                            Click to Delete </button>
                        <button
                            onClick={() => selectedMovieHandler(x.id)}>
                            Select Movie </button>
                    </li>
                ))}
            </ul>
        </div>
    )
}