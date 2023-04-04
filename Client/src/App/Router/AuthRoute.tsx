import { observer } from "mobx-react-lite"
import { Navigate, Outlet, useLocation } from "react-router-dom"
import { UseStore } from "../Stores/BaseStore"
import Navbar from "../layout/Navbar"

export default observer(function AuthRoute() {
  const { UserStore } = UseStore()
  const location = useLocation()

  if (!UserStore.IsLogging) {
    return <Navigate to={"/login"} state={{ from: location }} />
  }

  return (
    <>
      <Navbar />
      <Outlet />
    </>
  )
})
