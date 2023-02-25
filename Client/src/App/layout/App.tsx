import axios from 'axios'
import { useEffect, useState } from 'react'
import { Movie } from '../model/movie';
import './App.css'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard';
import Navbar from './Navbar';
import Browse from '../../Components/Movies/Browse/Browse';
import uuid from 'react-uuid';
import { observer } from 'mobx-react-lite';

function App() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [selectedMovie, setSelectedMovie] = useState<Movie | undefined>(undefined);
  const [openForm, setOpenForm] = useState(false);
  const [openDetails, setOpenDetails] = useState(false);

  useEffect(() => {
    axios.get('http://localhost:5000/api/movie').then(res => {
      setMovies(res.data);
    })
  }, [])

  function deleteMovie(id: string) {
    setMovies([...movies.filter(x => x.id !== id)]);
  }

  function selectedMovieHandler(id: string) {
    let movie = movies.find(x => x.id === id);
    // console.log(movie);
    setSelectedMovie(movie);
    setOpenDetails(true);
    setOpenForm(false);
  }

  const EditCreateHandler = (movie: Movie) => {
    if (movie.id) {
      setMovies([...movies.filter(x => x.id !== movie.id), movie]);
    }
    else {
      movie.id = uuid();
      setMovies([...movies, movie]);
    }
  }

  const CloseDetailsResetMovie = (state: boolean) => {
    setSelectedMovie(undefined);
    setOpenFormHandler(state);
  }

  const setOpenFormHandler = (state: boolean) => {
    setOpenForm(state);
    setOpenDetails(false);
  }

  return (
    <>
      <Navbar />
      <Browse />
      <Dashboard
        movie={movies}
        deleteMovie={deleteMovie}
        selectedMovie={selectedMovie}
        selectedMovieHandler={selectedMovieHandler}
        EditCreateHandler={EditCreateHandler}
        openForm={openForm}
        setOpenFormHandler={setOpenFormHandler}
        CloseDetailsResetMovie={CloseDetailsResetMovie}
        openDetails={openDetails}
      />
    </>
  )
}

export default observer(App)
