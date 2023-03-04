import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { UseStore } from "../../App/Stores/BaseStore"

export default observer(function RoomList() {
  const { UserRoomStore: { userRooms } } = UseStore();

  return (
    <>
      {userRooms.map(room => (
        <div className="max-w-lg bg-slate-300 max-h-96 p-2 m-4 rounded-md" key={room.id}>
          {room.title}
          <div className="bg-green-400 h-1 my-1"></div>
          <div>Judul Film : {room.movie.title}</div>
          <div className="flex gap-4 my-2 py-2">
            {room.attendees.map(attendee => (
              <div key={attendee.id} className="flex flex-col gap-1 items-center">
                <div className="bg-blue-300 rounded-full w-10 h-10" />
                <div className="text-black">{attendee.displayname}</div>
              </div>
            ))}
          </div>
        </div>
      ))}
    </>
  )
})
