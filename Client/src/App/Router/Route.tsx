import { createBrowserRouter, RouteObject } from "react-router-dom";
import Dashboard from "../../Components/Movies/Browse/Dashboard";
import App from "../layout/App";
import AuthRoute from "./AuthRoute";

const router: RouteObject[] = [{
    path: "/",
    element: <App />,
    children: [
        {
            element: <AuthRoute />, children: [
                { path: "/movies", element: <Dashboard /> },
            ]
        }
    ]
}];

export const browserRoute = createBrowserRouter(router);