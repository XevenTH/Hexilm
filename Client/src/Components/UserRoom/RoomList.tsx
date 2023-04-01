import './Room.css'

import { observer } from 'mobx-react-lite'
import { useEffect, useState } from 'react'
import { UserRoom } from '../../App/model/userRoom'
import { UseStore } from '../../App/Stores/BaseStore'

export default observer(function RoomList() {
  const {
    UserRoomStore: { userRooms },
  } = UseStore()

  const [isLoading, setIsLoading] = useState(true)
  const [isRoomClicked, setIsRoomClicked] = useState(false)

  useEffect(() => {
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'
    setIsLoading(false)
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  const handleRoomClick = (room: UserRoom) => {
    const filteredRooms = userRooms.filter((r) => r.id === room.id)
    const roomData = JSON.stringify(filteredRooms[0])
    localStorage.setItem('dataRoom', roomData)
    setIsRoomClicked(true)
    window.location.href = '/room'
  }

  return (
    <>
      {isRoomClicked ? (
        <></>
      ) : (
        <>
          {isLoading ? (
            <div className="min-h-screen flex justify-center items-center">
              <div className="loader">
                <div className="dot1"></div>
                <div className="dot2"></div>
                <div className="dot3"></div>
              </div>
            </div>
          ) : (
            <div className="grid md:grid-cols-3">
              {userRooms.map((room) => (
                <div
                  className="duration-200 max-w-lg bg-gray-800 max-h-96 m-4 rounded-lg drop-shadow-lg cursor-pointer"
                  key={room.id}
                  onClick={() => {
                    handleRoomClick(room)
                  }}
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
                        key={attendee.userName}
                        className="flex flex-col gap-1 items-center"
                      >
                        <div className="bg-blue-300 rounded-full w-10 h-10" />
                        <div className="text-black">{attendee.displayName}</div>
                      </div>
                    ))}
                    {room.attendees.length > 3 && (
                      <div className="text-black">
                        +{room.attendees.length - 3}
                      </div>
                    )}
                  </div>
                </div>
              ))}
            </div>
          )}
        </>
      )}
    </>
  )
})
