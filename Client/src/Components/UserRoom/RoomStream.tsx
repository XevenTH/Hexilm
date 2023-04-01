import './Room.css'

import { observer } from 'mobx-react-lite'
import { useEffect, useRef, useState } from 'react'

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
  const [user, setUser] = useState('MY NAME')
  const [comment, setComment] = useState('')
  const [smallComment, setSmallComment] = useState(false)
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

  function addCommentHandler(event: any) {
    event.preventDefault()
    setCommentsState([...commentsState, { user: 'MY NAME', comment: comment }])
    setComment('')
  }

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
          {smallComment && (
            <div className="fixed bottom-5 right-5 w-80  bg-white/20 rounded-lg p-2">
              <div className="h-56 overflow-y-scroll mb-1">
                <div className="bg-green-700 text-white ml-auto text-right p-2 rounded-lg w-3/4 mb-5">
                  <h2>MY NAME</h2>
                  <p>Hello! it's a nice movie right?</p>
                </div>
                <div className="bg-gray-900 text-white text-left p-4 rounded-lg w-3/4 mb-5">
                  <h2>FRIEND</h2>
                  <p>yeah you're right, i like it!</p>
                </div>
                {commentsState.slice(1).map((comment, index) => (
                  <div
                    key={index}
                    className="bg-green-700 text-white ml-auto text-right p-2 rounded-lg w-3/4 mb-5"
                  >
                    <h2>{comment.user}</h2>
                    <p>{comment.comment}</p>
                  </div>
                ))}
              </div>
              <form onSubmit={addCommentHandler}>
                <div className={`flex`}>
                  <input
                    className="w-full h-10 rounded-l-full px-4 focus:outline-none"
                    type="text"
                    value={comment}
                    onChange={(event) => setComment(event.target.value)}
                  />
                  <button
                    className="bi bi-send w-10 py-1 pr-2 rounded-r-full bg-white text-gray-900 text-xl"
                    type="submit"
                  ></button>
                </div>
              </form>
            </div>
          )}
          <div className="flex overflow-y-hidden" style={{ height: '37rem' }}>
            <div
              className={`duration-200 bg-gray-800 text-white h-screen ${
                closeRecommend ? 'closeRecommend overflow-y-scroll' : 'hover:overflow-y-scroll'
              }  ${closeRecommend ? 'w-20' : 'w-96'}`}
            >
              <div className="flex items-center p-2">
                <h2 className={`${closeRecommend && 'hidden'}`}>
                  RECOMMEND MOVIES
                </h2>
                <i
                  className={`bi ${
                    closeRecommend
                      ? 'bi-arrow-bar-right text-center'
                      : 'bi-arrow-bar-left ml-auto'
                  } px-1 rounded-lg hover:bg-white/20 text-xl`}
                  onClick={() => setCloseRecommend(!closeRecommend)}
                ></i>
              </div>
              <ul>
                {Array.from({ length: 20 }).map((_, index) => {
                  return (
                    <li className="p-2 hover:bg-gray-900 flex items-center">
                      <div className="w-10">
                        <img
                          src="https://media.tenor.com/NICoVNbKVGYAAAAM/profile-picture.gif"
                          alt=""
                          className="rounded-full bg-cover"
                        />
                      </div>
                      {!closeRecommend && (
                        <>
                          <div className='ml-2'>
                            <h3 className="text-sm font-semibold">
                              DisplayName
                            </h3>
                            <p className="text-sm text-gray-300">Movie Name</p>
                          </div>
                          <div className="flex items-center ml-auto">
                            <p className="bg-red-700 rounded-full w-2 h-2 mr-1"></p>
                            {index + 1}.4k
                          </div>
                        </>
                      )}
                    </li>
                  )
                })}
              </ul>
            </div>
            <div className="w-full h-screen">
              <iframe
                className={`w-full h-${
                  closeLiveChat && closeRecommend ? '3/4' : '2/3'
                } border border-white/20`}
                src="https://www.youtube.com/embed/dQw4w9WgXcQ"
                title="YouTube video player"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                allowFullScreen
              ></iframe>
              <div className="w-full p-5">
                <div className="flex items-center text-white">
                  <div>
                <img src="https://i.pinimg.com/550x/0e/51/7e/0e517eb57cb5a992ef3230b0e0d792af.jpg" alt="" className='rounded-full border-2 border-sky-500' width={80} />
                <div className="text-center"><p className='-mt-4'><span className='bg-red-600 px-1 rounded-md text-sm font-medium'>LIVE</span></p></div>
                </div>
                <h2 className='px-4 text-2xl font-semibold'>TenZ</h2>
                </div>
              </div>
            </div>
            <div
              className={`duration-200 text-white ${
                closeLiveChat ? 'marginCustome' : 'bg-gray-700'
              }`}
              style={{ width: '603px' }}
            >
              <i
                className={`bi ${
                  closeLiveChat
                    ? 'bi-arrow-bar-left absolute -ml-8'
                    : 'bi-arrow-bar-right'
                } px-1 rounded-lg hover:bg-white/20 text-xl ml-auto`}
                onClick={() => setCloseLiveChat(!closeLiveChat)}
              ></i>
              <div className={`text-center p-5 ${closeLiveChat && 'hidden'}`}>
                <h1 className="text-white text-6xl mb-5">{filterRoom.title}</h1>
                <p>Movie Attendees:</p>
                <div className="flex justify-center mb-5">
                  {filterRoom.attendees.slice(0, 3).map((attendee: any) => (
                    <div
                      key={attendee.username}
                      className="flex flex-col mr-2 items-center"
                    >
                      <div className="bg-blue-300 rounded-full w-10 h-10" />
                      <div className="text-black">{attendee.displayName}</div>
                    </div>
                  ))}
                </div>
                <div
                  className={`bg-gray-900 rounded-lg ${smallComment && 'py-5'}`}
                >
                  <i
                    className={`bi ${
                      smallComment
                        ? 'bi-arrow-down-square'
                        : 'bi-arrow-up-square mt-1'
                    } text-4xl text-gray-300 hover:text-gray-400 absolute`}
                    onClick={() => setSmallComment(!smallComment)}
                  ></i>
                  <div className="p-5">
                    <div
                      className={`${
                        smallComment ? 'hidden' : 'heightCustome'
                      } overflow-y-scroll p-2`}
                    >
                      <div className="bg-green-700 ml-auto text-white text-right p-4 rounded-lg w-2/3 mb-5">
                        <h2 className="font-medium">MY NAME</h2>
                        <p>Hello! it's a nice movie right?</p>
                      </div>
                      <div className="bg-gray-600 text-white text-left p-4 rounded-lg w-2/3 mb-5">
                        <h2 className="font-medium">FRIEND</h2>
                        <p>yeah you're right, i like it!</p>
                      </div>
                      {commentsState.slice(1).map((comment, index) => (
                        <div
                          key={index}
                          className="bg-green-700 ml-auto text-white text-right p-4 rounded-lg w-2/3 mb-5"
                        >
                          <h2 className="font-medium">{comment.user}</h2>
                          <p>{comment.comment}</p>
                        </div>
                      ))}
                    </div>
                    <form
                      onSubmit={addCommentHandler}
                      className={`${smallComment && 'hidden'}`}
                    >
                      <div className={`flex ${smallComment && 'mt-10'}`}>
                        <input
                          className="bg-gray-600 w-full h-16 rounded-l-full px-10 text-white focus:outline-none text-lg"
                          type="text"
                          value={comment}
                          onChange={(event) => setComment(event.target.value)}
                        />
                        <button
                          className="bi bi-send w-20 py-4 bg-gray-600 rounded-r-full text-white text-2xl"
                          type="submit"
                        ></button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  )
})
