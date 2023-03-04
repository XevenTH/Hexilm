import { createBrowserRouter, RouteObject } from "react-router-dom";
import Dashboard from "../../Components/Movies/Dashboard/Dashboard";
import RoomPage from "../../Components/UserRoom/RoomPage";
import App from "../layout/App";
import AuthRoute from "./AuthRoute";

const router: RouteObject[] = [{
    path: "/",
    element: <App />,
    children: [
        {
            element: <AuthRoute />, children: [
                { path: "/movies", element: <Dashboard /> },
                { path: "/movieRoom", element: <RoomPage /> },
            ]
        }
    ]
}];

export const browserRoute = createBrowserRouter(router);