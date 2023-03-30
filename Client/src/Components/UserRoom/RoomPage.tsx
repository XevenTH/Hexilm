import { useEffect, useState } from 'react'
import { UseStore } from '../../App/Stores/BaseStore'
import RoomList from './RoomList'

export default function RoomPage() {
  const {
    UserRoomStore: { getRoomList },
  } = UseStore()

  useEffect(() => {
    getRoomList()
  }, [])

  const [isOpen, setIsOpen] = useState(false)

  const toggleSidebar = () => {
    setIsOpen(!isOpen)
  }

  return (
    <div className="grid md:grid-cols-3">
      <RoomList />
    </div>
  )
}
