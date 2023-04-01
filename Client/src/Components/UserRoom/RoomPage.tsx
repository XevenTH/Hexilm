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

  return (
    <div>
      <RoomList />
    </div>
  )
}
