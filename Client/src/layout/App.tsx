import axios from 'axios'
import { useEffect, useState } from 'react'
import { Movie } from '../App/model/movie';
import reactLogo from '../assets/react.svg'
import './App.css'

function App() {
  const [movies, setMovies] = useState<Movie[]>([]);

  useEffect(() => {
    axios.get('http://localhost:5000/api/movie').then(res => {
      setMovies(res.data);
    })
  }, [])

  return (
    <>
      <div className="App">
        <div>
          <a href="https://vitejs.dev" target="_blank">
            <img src="/vite.svg" className="logo" alt="Vite logo" />
          </a>
          <a href="https://reactjs.org" target="_blank">
            <img src={reactLogo} className="logo react" alt="React logo" />
          </a>
        </div>
      </div>
      <div>
        <ul>
          {movies.map((movie) => (
            <li key={movie.id}>
              {movie.title}
            </li>
          ))}
        </ul>
      </div>
    </>
  )
}

export default App
