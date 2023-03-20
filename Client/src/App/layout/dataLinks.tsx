import Movies from '../../Components/Movies/Dashboard/Movies'
import RoomPage from '../../Components/UserRoom/RoomPage'

export default function pageLinks() {
  return [
    // Contoh di terapkan di array pertama
    {
      name: 'Movies', // <== name ini untuk judul yang ingin ditampilkan di list sidebar
      icon: 'bi-film', // <== icon ini untuk className/nama dari icon apa di svg yang ingin ditampilkan di list sidebar
      // >>> Untuk icon: dan path: ini refrensi dari bootstrap icon <<<
      path: (
        <path d="M0 1a1 1 0 0 1 1-1h14a1 1 0 0 1 1 1v14a1 1 0 0 1-1 1H1a1 1 0 0 1-1-1V1zm4 0v6h8V1H4zm8 8H4v6h8V9zM1 1v2h2V1H1zm2 3H1v2h2V4zM1 7v2h2V7H1zm2 3H1v2h2v-2zm-2 3v2h2v-2H1zM15 1h-2v2h2V1zm-2 3v2h2V4h-2zm2 3h-2v2h2V7zm-2 3v2h2v-2h-2zm2 3h-2v2h2v-2z" />
      ), // <== path ini untuk path icon di svg yang ingin ditampilkan di list sidebar
      page: <Movies />, // <== page ini untuk Element apa yg mau dirender bukan ditampilkan di list sidebar tepatnya di bawah navbar dan di samping sidebar, ini adalah untuk bagian main contentnya yg mau di render
    },
    {
      name: 'Room Page',
      icon: 'bi-camera-reels',
      path: (
        <>
          <path d="M6 3a3 3 0 1 1-6 0 3 3 0 0 1 6 0zM1 3a2 2 0 1 0 4 0 2 2 0 0 0-4 0z" />
          <path d="M9 6h.5a2 2 0 0 1 1.983 1.738l3.11-1.382A1 1 0 0 1 16 7.269v7.462a1 1 0 0 1-1.406.913l-3.111-1.382A2 2 0 0 1 9.5 16H2a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h7zm6 8.73V7.27l-3.5 1.555v4.35l3.5 1.556zM1 8v6a1 1 0 0 0 1 1h7.5a1 1 0 0 0 1-1V8a1 1 0 0 0-1-1H2a1 1 0 0 0-1 1z" />
          <path d="M9 6a3 3 0 1 0 0-6 3 3 0 0 0 0 6zM7 3a2 2 0 1 1 4 0 2 2 0 0 1-4 0z" />
        </>
      ),
      page: <RoomPage />,
    },
  ]
}
