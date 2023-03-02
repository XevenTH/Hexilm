import { observer } from "mobx-react-lite"
import { UseStore } from "../Stores/BaseStore"

export default observer(function NavBar() {
    const { UserStore: { User, logout } } = UseStore()

    return (
        <div className="flex justify-between items-center w-full bg-blue-400">
            <h1 className="font-bold text-2xl text-red-600 m-2">
                CoolMovie
            </h1>
            <h1 className="flex items-center m-2 text-xl text-red-600 gap-2 ">
                <button className="bg-red-600 text-white font-semibold p-2 rounded-xl" onClick={() => logout()}>
                    Logout
                </button>
                <div className="font-semibold ">
                    {User ?
                        User.displayname : "Name"}
                </div>
                <div className="bg-blue-800 rounded-full w-10 h-10">
                </div>
            </h1>
        </div>
    )
})