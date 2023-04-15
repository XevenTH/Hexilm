import './RoomStream.css'
import '../Room.css'
import { useEffect, useState } from 'react'

export default function LiveChat(props: any) {
  const {
    closeLiveChat,
    setCloseLiveChat,
    filterRoom,
    commentsState,
    setCommentsState,
  } = props
  const [comment, setComment] = useState('')
  const [seeAttendees, setSeeAttendees] = useState(false)
  const handleClick = () => {
    // Mengubah nilai closeRecommend
    setCloseLiveChat(!closeLiveChat)
  }

  function addCommentHandler(event: any) {
    event.preventDefault()
    setCommentsState([...commentsState, { user: 'MY NAME', comment: comment }])
    setComment('')
  }

  function addHorizontalScrollListener(element: HTMLElement) {
    element.addEventListener('wheel', (e) => {
      e.preventDefault()
      element.scrollLeft += e.deltaY
    })
  }
  useEffect(() => {
    const AttendeesXScroll = document.querySelector(
      '.xScroll',
    ) as HTMLDivElement
    addHorizontalScrollListener(AttendeesXScroll)
  }, [])
  return (
    <div
      className={`duration-200 text-white ${
        closeLiveChat ? 'marginCustome' : 'bg-gray-800'
      } overflow-y-scroll`}
      style={{ width: '603px' }}
    >
      <div className="flex p-2 border-b">
        <i
          className={`bi ${
            closeLiveChat
              ? 'bi-arrow-bar-left absolute -ml-12'
              : 'bi-arrow-bar-right'
          } px-1 rounded-lg hover:bg-white/20 text-xl`}
          onClick={handleClick}
        ></i>
        <div className="w-full text-center">
          <h1 className="text-white text-2xl text-center">Live Chat</h1>
        </div>
      </div>
      <div className={`p-2 ${closeLiveChat && 'hidden'}`}>
        <div className={`bg-gray-900 rounded-lg`}>
          <div className="p-2">
            <div
              className={`heightCustome overflow-y-scroll p-2 text-white text-sm`}
            >
              <p className="mb-2">
                <b>tenz</b>: Hello! it's a nice movie right?
              </p>

              <p className="mb-2">
                <b>Alex</b>: yeah you're right, i like it!
              </p>

              {commentsState.slice(1).map((comment: any, index: any) => (
                <p className="mb-2" key={index}>
                  <b>{comment.user}</b>: {comment.comment}
                </p>
              ))}
            </div>
            <form onSubmit={addCommentHandler}>
              <div className={`flex`}>
                <input
                  className="bg-inherit border-y border-l w-full h-10 rounded-l-lg px-10 text-white focus:outline-none text-lg"
                  type="text"
                  value={comment}
                  onChange={(event) => setComment(event.target.value)}
                />
                <button
                  className="bi bi-send w-20 rounded-r-lg text-white text-xl border-y border-r"
                  type="submit"
                ></button>
              </div>
            </form>
          </div>
        </div>
        <i
          className="bi bi-eye mx-2 cursor-pointer"
          onClick={() => setSeeAttendees(!seeAttendees)}
        >
          {filterRoom.attendees.length}
        </i>
        <div
          className={`duration-300 bg-black rounded-lg flex px-2 p-1 overflow-x-scroll xScroll ${
            !seeAttendees && 'w-0 h-0 absolute top-0 left-0'
          } w-full static`}
        >
          {filterRoom.attendees.slice(0, 3).map((attendee: any) => (
            <div
              key={attendee.userName}
              className="flex flex-col mr-4 items-center text-sm"
            >
              <img className="rounded-full w-8 h-8 bg-cover" src={attendee.photo} alt="" />
              <div className="text-white">{attendee.displayName}</div>
            </div>
          ))}
        </div>
      </div>
    </div>
  )
}
