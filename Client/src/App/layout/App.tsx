import axios from 'axios'
import { useEffect, useState } from 'react'
import { Movie } from '../model/movie';
import './App.css'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard';
import Navbar from './Navbar';
import Browse from '../../Components/Movies/Browse/Browse';

function App() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [selectedMovie, setSelectedMovies] = useState<Movie | undefined>(undefined);
  const [openForm, setOpenForm] = useState(false);

  useEffect(() => {
    axios.get('http://localhost:5000/api/movie').then(res => {
      setMovies(res.data);
    })
  }, [])

  function deleteMovie(id: string) {
    setMovies([...movies.filter(x => x.id !== id)]);
  }

  function selectedMovieHandler(id: string) {
    setSelectedMovies(movies.find(x => x.id === id));
    setOpenForm(true);
  }

  const EditCreateHandler = (movie: Movie) => {
    movie.id ? 
    setMovies([...movies.filter(x => x.id !== movie.id), movie]) 
    : setMovies([...movies, movie]);
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
        setOpenForm={setOpenForm}
        openForm={openForm}
      />
    </>
  )
}

export default App
