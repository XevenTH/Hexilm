import { observer } from 'mobx-react-lite'
import { UseStore } from '../../../App/Stores/BaseStore'
import { useParams } from 'react-router-dom'
import { useEffect, useState } from 'react'

export default observer(function MovieDetail() {
  const { id } = useParams()
  const [error, setError] = useState(false)

  const { MovieStore } = UseStore()
  const {
    selectedMovie: movie,
    isLoadingMovie,
    selectedMovieHandler,
  } = MovieStore

  useEffect(() => {
    selectedMovieHandler(id!).catch(() => {
      setError(true)
    })
    window.scrollTo({
      top: 0,
    })
  }, [id])

  if (isLoadingMovie) return <p>Loading...</p>
  if (error) {
    throw new Response('Not found', {
      status: 404,
      statusText: 'Movie not found',
    })
  }

  return (
    <>
      {movie && (
        <div>
          <img
            src="https://images.unsplash.com/photo-1494972688394-4cc796f9e4c5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8d2FyfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
            alt="cover"
            className="h-[240px] lg:h-[400px] w-full object-cover relative -z-[3] filter brightness-75"
          />

          <div className="flex md:p-8 rounded-tl-xl rounded-tr-xl bg-[#181823] -mt-5 min-h-screen shadow-xl">
            <img
              src="https://images.unsplash.com/photo-1616530940355-351fabd9524b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTR8fG1vdmllfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
              alt="cover"
              className="object-cover aspect-[6/8] hidden md:block w-[max(30%,240px)] rounded-xl self-start"
            />
            <div className="px-5 text-neutral-200">
              <h1 className="text-3xl text-neutral-100 my-5 font-semibold">
                {movie.title}
              </h1>

              <h3 className="text-md text-gray-500 font-bold">SYNOPSIS</h3>
              <p className="text-md text-gray-200">{movie.description}</p>
            </div>
          </div>
        </div>
      )}
    </>
  )
})
