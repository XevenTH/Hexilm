import { createBrowserRouter, RouteObject } from 'react-router-dom'
import Browse from '../../Components/Movies/Browse/Browse'
import Dashboard from '../../Components/Movies/Dashboard/Dashboard'
import MovieDetail from '../../Components/Movies/MovieDetail/MovieDetail'
import AddMovie from '../../Components/Movies/AddMovie/AddMovie'
import Profile from '../../Components/Profile/Profile'
import RoomStream from '../../Components/UserRoom/RoomStream/RoomStream'
import App from '../layout/App'
import AuthRoute from './AuthRoute'
import AdminRoute from './AdminRoute'
import UnauthRoute from './UnauthRoute'
import Login from '../../Components/Auth/Login'
import Register from '../../Components/Auth/Register'
import ErrorBoundary from '../../Components/ErrorBoundary'

const router: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    errorElement: <ErrorBoundary />,
    children: [
      {
        element: <AuthRoute />,
        children: [
          { path: '/', element: <Dashboard />, index: true },
          { path: 'browse', element: <Browse /> },
          // {/movieRoom} sudah dijadikan component di file sidebar.tsx
          { path: 'room/:id', element: <RoomStream /> },
          {
            path: 'movie/:id',
            element: <MovieDetail />,
          },
          { path: 'profile/:username', element: <Profile /> },
          {
            path: 'admin',
            element: <AdminRoute />,
            children: [
              {
                path: 'add-movie',
                element: <AddMovie />,
              },
            ],
          },
        ],
      },
      {
        element: <UnauthRoute />,
        children: [
          { path: 'login', element: <Login /> },
          { path: 'register', element: <Register /> },
        ],
      },
    ],
  },
]

export const browserRoute = createBrowserRouter(router)
