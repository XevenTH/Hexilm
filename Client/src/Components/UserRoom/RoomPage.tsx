import { useEffect } from 'react'
import { UseStore } from '../../App/Stores/BaseStore'
import RoomList from './RoomList'

export default function RoomPage() {
  const {
    UserRoomStore: { getRoomList },
  } = UseStore()

  useEffect(() => {
    getRoomList()
  }, [])

  return (
    <div className="grid md:grid-cols-3">
      <RoomList />
    </div>
  )
}
