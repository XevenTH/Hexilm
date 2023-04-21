import { observer } from 'mobx-react-lite'
import { Navigate, Outlet, useLocation } from 'react-router-dom'
import { UseStore } from '../Stores/BaseStore'

export default observer(function AuthRoute() {
  const { UserStore } = UseStore()
  const location = useLocation()

  const isAdmin = UserStore?.User?.role === 'admin'

  if (!isAdmin) {
    return <Navigate to={'/'} state={{ from: location }} />
  }

  return (
    <>
      <Outlet />
    </>
  )
})
