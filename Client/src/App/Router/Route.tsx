import { createBrowserRouter, RouteObject } from "react-router-dom"
import Browse from "../../Components/Movies/Browse/Browse"
import Dashboard from "../../Components/Movies/Dashboard/Dashboard"
import MovieDetail from "../../Components/Movies/MovieDetail/MovieDetail"
import RoomPage from "../../Components/UserRoom/RoomPage"
import App from "../layout/App"
import AuthRoute from "./AuthRoute"

const router: RouteObject[] = [
  {
    path: "/",
    element: <App />,
    children: [
      {
        element: <AuthRoute />,
        children: [
          { path: "/browse", element: <Browse /> },
          { path: "/movies", element: <Dashboard /> },
          { path: "/movieRoom", element: <RoomPage /> },
          { path: "/movie/:id", element: <MovieDetail /> },
        ],
      },
    ],
  },
]

export const browserRoute = createBrowserRouter(router)
