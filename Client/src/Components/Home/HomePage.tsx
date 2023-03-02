import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import LoginForm from "../../App/common/Form/LoginForm";
import RegisterForm from "../../App/common/Form/RegisterForm";
import { UseStore } from "../../App/Stores/BaseStore";

export default observer(function Home() {
    const { UserStore, ModalStore } = UseStore();
    const navigate = useNavigate();

    return (
        <>
            {!UserStore.IsLogging ?
                <div>
                    <h3 className="text-center m-4 text-3xl font-bold">
                        Home Page
                    </h3>
                    <div className="flex justify-center mb-4 gap-2 font-semibold text-slate-200">
                        <button className="bg-blue-400 p-2 rounded-xl" onClick={() => ModalStore.setOpen(<LoginForm />)}>
                            Login
                        </button>
                        <button className="bg-blue-400 p-2 rounded-xl" onClick={() => ModalStore.setOpen(<RegisterForm />)}>
                            Register
                        </button>
                    </div>
                    {ModalStore.Modal.isOpen &&
                        <div className="flex justify-center w-full">
                            <div className="bg-blue-300 rounded-md max-w-md p-4">
                                {ModalStore.Modal.content}
                            </div>
                        </div>
                    }
                </div>
                :
                <div className="w-full flex flex-col items-center gap-2">
                    <h1 className="text-4xl font-bold">
                        WelCome
                    </h1>
                    <button className="bg-blue-400 p-2 rounded-xl" onClick={() => navigate("/movies")}>
                        Find Movies
                    </button>
                </div>
            }
        </>
    )
})