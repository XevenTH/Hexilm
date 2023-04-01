import { createBrowserRouter, RouteObject } from 'react-router-dom'
import Browse from '../../Components/Movies/Browse/Browse'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard'
import MovieDetail from '../../Components/Movies/MovieDetail/MovieDetail'
import Profile from '../../Components/Profile/Profile'
import RoomStream from '../../Components/UserRoom/RoomStream/RoomStream'
import App from '../layout/App'
import AuthRoute from './AuthRoute'

const router: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <AuthRoute />,
        children: [
          { path: '/browse', element: <Browse /> },
          { path: '/movies', element: <Dashboard /> },
          // {/movieRoom} sudah dijadikan component di file sidebar.tsx
          { path: '/room', element: <RoomStream /> },
          { path: '/movie/:id', element: <MovieDetail /> },
          { path: '/profile/:username', element: <Profile /> },
        ],
      },
    ],
  },
]

export const browserRoute = createBrowserRouter(router)
