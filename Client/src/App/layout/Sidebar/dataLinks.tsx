import Movies from '../../../Components/Movies/Dashboard/Movies'
import RoomPage from '../../../Components/UserRoom/RoomPage'

export default function pageLinks() {
  return [
    // Contoh di terapkan di array pertama
    {
      name: 'Movies', // <== name ini untuk judul yang ingin ditampilkan di list sidebar
      icon: 'bi-film', // <== icon ini untuk className/nama dari icon apa di svg yang ingin ditampilkan di list sidebar
      // >>> Untuk icon: dan path: ini refrensi dari bootstrap icon <<<
      page: <Movies />, // <== page ini untuk Element apa yg mau dirender bukan ditampilkan di list sidebar tepatnya di bawah navbar dan di samping sidebar, ini adalah untuk bagian main contentnya yg mau di render
    },
    {
      name: 'Room Page',
      icon: 'bi-camera-reels',
      page: <RoomPage />,
    },
  ]
}
