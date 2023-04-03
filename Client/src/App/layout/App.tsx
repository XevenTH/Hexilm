import { observer } from "mobx-react-lite"
import { useState, useEffect } from "react"
import { Outlet } from "react-router-dom"
import Loading from "../common/UI/Loading"
import { UseStore } from "../Stores/BaseStore"
import "./css/App.css"

function App() {
  const { UserStore, CommonStore } = UseStore()
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    const checkAuth = async () => {
      if (CommonStore.token) {
        await UserStore.getUser()
      }
      setTimeout(() => {
        setLoading(false)
      }, 2000)
    }
    checkAuth()
  }, [UserStore])

  return loading ? <Loading /> : <Outlet />
}

export default observer(App)
