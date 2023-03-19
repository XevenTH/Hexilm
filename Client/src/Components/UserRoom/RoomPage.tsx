import { useEffect, useState } from 'react'
import { UseStore } from '../../App/Stores/BaseStore'
import RoomList from './RoomList'
import Sidebar from '../../App/layout/Sidebar'

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
    <>
      <Sidebar isOpen={isOpen} toggleSidebar={toggleSidebar}>
        <div className="grid grid-cols-3 gap-4">
          <RoomList />
        </div>
      </Sidebar>
    </>
  )
}
