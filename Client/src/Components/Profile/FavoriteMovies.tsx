import { UseStore } from "../../App/Stores/BaseStore";
import MovieList from "../Movies/Dashboard/MovieList";


export default function FavoriteMovies() {
    const { ProfileStore: { profile } } = UseStore()

    return (
        <>
            <h3 className="text-white text-center text-4xl">
                Your Favorite Movie
            </h3>
            <ul className="grid md:grid-cols-3 gap-3 ml-3">
                {profile?.favoriteMovies.map((x) => (
                    <li key={x.id}>
                        <MovieList movie={x} />
                    </li>
                ))}
            </ul>
        </>
    )
}