import '../Room.css'
import './RoomStream.css'

import { observer } from 'mobx-react-lite'
import { useEffect, useRef, useState } from 'react'
import Recommend from './Recommend'
import LiveChat from './LiveChat'
import Stream from './Stream'

export default observer(function RoomStream() {
  interface Room {
    id: string
    title: string
    movie: {
      id: string
      title: string
      description: string
      picture: string
    }
    attendees: any[]
  }

  const [isLoading, setIsLoading] = useState(true)
  const [filterRoom, setFilterRoom] = useState<Room>({
    id: '',
    title: '',
    movie: { id: '', title: '', description: '', picture: '' },
    attendees: [],
  })
  const [commentsState, setCommentsState] = useState([
    { user: '', comment: '' },
  ])

  const commentRef = useRef<HTMLDivElement>(null)
  const [closeRecommend, setCloseRecommend] = useState(false)
  const [closeLiveChat, setCloseLiveChat] = useState(false)

  useEffect(() => {
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'

    const dataRoom = JSON.parse(localStorage.getItem('dataRoom') || '[]')

    setFilterRoom(dataRoom)

    setIsLoading(false)

    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  useEffect(() => {
    setIsLoading(false)
  }, [filterRoom])

  useEffect(() => {
    if (commentRef.current instanceof HTMLElement) {
      commentRef.current.scrollTop = commentRef.current.scrollHeight
    }
  }, [commentsState])

  return (
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
        <>
          <div className="flex overflow-y-hidden" style={{ height: '37rem' }}>
            <Recommend
              closeRecommend={closeRecommend}
              setCloseRecommend={setCloseRecommend}
            />
            <Stream
              closeLiveChat={closeLiveChat}
              filterRoom={filterRoom}
              closeRecommend={closeRecommend}
            />

            <LiveChat
              closeLiveChat={closeLiveChat}
              setCloseLiveChat={setCloseLiveChat}
              filterRoom={filterRoom}
              commentsState={commentsState}
              setCommentsState={setCommentsState}
            />
          </div>
        </>
      )}
    </>
  )
})
