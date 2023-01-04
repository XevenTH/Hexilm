import axios from 'axios'
import { useEffect, useState } from 'react'
import { Movie } from '../model/movie';
import './App.css'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard';

function App() {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [selectedMovie, setSelectedMovies] = useState<Movie | undefined>(undefined);

  useEffect(() => {
    axios.get('http://localhost:5000/api/movie').then(res => {
      setMovies(res.data);
    })
  }, [])

  function deleteMovie(id: string) {
    setMovies([...movies.filter(x => x.id !== id )]);
  }

  function selectedMovieHandler(id: string) {
    setSelectedMovies(movies.find(x => x.id === id));
  }

  return (
    <Dashboard 
    movie={movies} 
    deleteMovie={deleteMovie}
    selectedMovie={selectedMovie}
    selectedMovieHandler={selectedMovieHandler}
    />
  )
}

export default App
