import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { UseStore } from '../../../App/Stores/BaseStore'
import Edit from './edit'
import MovieDetails from './MovieDetails'
import MovieList from './MovieList'
import { Link } from 'react-router-dom'

export default observer(function Movies() {
  const { MovieStore, UserStore } = UseStore()
  const {
    movieList: movie,
    isOpenDetails,
    isOpenForm,
    CloseDetailsResetMovie,
  } = MovieStore

  const { User } = UserStore

  const isAdmin = User!.role === 'admin'

  useEffect(() => {
    MovieStore.getMovie()
  }, [])

  useEffect(() => {
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'

    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  return (
    <>
      <div>
        <ul className="grid md:grid-cols-3 gap-3 ml-3">
          {movie.map((x) => (
            <li key={x.id}>
              <MovieList movie={x} />
            </li>
          ))}
        </ul>
        {isAdmin && (
          <Link
            to="/admin/add-movie"
            className="bg-red-200 p-2 rounded-xl shadow-md font-semibold capitalize w-fit ml-4 inline-block"
          >
            New Movie
          </Link>
        )}

        {isOpenDetails && <MovieDetails />}
        {isOpenForm && <Edit />}
      </div>
    </>
  )
})
