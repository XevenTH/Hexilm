import { UseStore } from '../../App/Stores/BaseStore'
import MovieList from '../Movies/Dashboard/MovieList'
import EditProfile from './EditProfile'

export default function dataLinks() {
  const {
    ProfileStore: { profile },
  } = UseStore()

  return [
    // Contoh di terapkan di array pertama
    {
      name: 'Fav Movie', // <== name ini untuk judul yang ingin ditampilkan di list sidebar
      page: (
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
      ), // <== page ini untuk Element apa yg mau dirender bukan ditampilkan di list sidebar tepatnya di bawah navbar dan di samping sidebar, ini adalah untuk bagian main contentnya yg mau di render
    },
    {
      name: 'Edit Profile',
      page: <EditProfile />,
    },
  ]
}
