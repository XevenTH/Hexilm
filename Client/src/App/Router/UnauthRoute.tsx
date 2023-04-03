import { observer } from "mobx-react-lite"
import { Navigate, Outlet, useLocation } from "react-router-dom"
import { UseStore } from "../Stores/BaseStore"

export default observer(function AuthRoute() {
  const { UserStore } = UseStore()
  const location = useLocation()

  if (UserStore.IsLogging) {
    return <Navigate to={"/"} state={{ from: location }} />
  }

  return (
    <div className="bg-gradient-to-tr from-[#22d3ee] to-[#f02fc2] via-[#8f3bff] min-h-screen">
      <Outlet />
    </div>
  )
})
