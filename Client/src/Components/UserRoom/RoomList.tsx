import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { UseStore } from '../../App/Stores/BaseStore'

export default observer(function RoomList() {
  const {
    UserRoomStore: { userRooms },
  } = UseStore()

  useEffect(() => {
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'

    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  return (
    <>
      {userRooms.map((room) => (
        <div
          className="max-w-lg bg-gray-800 max-h-96 m-4 rounded-lg drop-shadow-lg"
          key={room.id}
        >
          <div className="relative px-2 flex items-center bg-white/25 z-10 rounded-t-lg">
            {room.title}{' '}
            <div className="bg-red-700/95 rounded-full w-5 h-5 ml-1"></div>
          </div>

          <img
            src="https://static.vecteezy.com/system/resources/previews/005/502/524/original/cinema-background-concept-movie-theater-object-on-red-curtain-background-and-movie-time-with-electric-bulbs-frame-illustration-free-vector.jpg"
            alt=""
            className="-mt-6 rounded-t-lg"
          />

          <div className="-mt-9 text-center text-white">
            Judul Film : {room.movie.title}
          </div>
          <div className="flex justify-center gap-4 my-2 p-4 items-center">
            {room.attendees.slice(0, 3).map((attendee) => (
              <div
                key={attendee.username}
                className="flex flex-col gap-1 items-center"
              >
                <div className="bg-blue-300 rounded-full w-10 h-10" />
                <div className="text-black">{attendee.displayName}</div>
              </div>
            ))}
            {room.attendees.length > 3 && (
              <div className="text-black">+{room.attendees.length - 3}</div>
            )}
          </div>
        </div>
      ))}
    </>
  )
})
