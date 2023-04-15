import EditProfile from './EditProfile'
import FavoriteMovies from './FavoriteMovies'

export default function dataLinks() {
  return [
    // Contoh di terapkan di array pertama
    {
      name: 'Fav Movie', // <== name ini untuk judul yang ingin ditampilkan di list sidebar
      page: <FavoriteMovies />, // <== page ini untuk Element apa yg mau dirender bukan ditampilkan di list sidebar tepatnya di bawah navbar dan di samping sidebar, ini adalah untuk bagian main contentnya yg mau di render
    },
    {
      name: 'Edit Profile',
      page: <EditProfile />,
    },
  ]
}
