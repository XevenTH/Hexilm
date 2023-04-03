import LoginForm from "../../App/common/Form/LoginForm"
import { Link } from "react-router-dom"

function Login() {
  return (
    <div className="flex justify-center items-center w-full h-[97vh]">
      {/*tambah style untuk bikin ditengah modalnya */}
      <div className="bg-white border shadow-lg rounded-md max-w-xs p-9 sm:max-w-md ">
        <h3 className="text-center m-4 text-3xl font-bold">Login</h3>
        <LoginForm />
        {/* Tombol Login/Register */}
        <p className="text-center text-sm font-medium text-[#5c5c5c]">
          Don't have account yet ?
          <Link
            to={"/register"}
            className="underline underline-offset-2 hover:opacity-80"
          >
            Register
            {/* Teks tombol disesuaikan dengan nilai state showLoginForm */}
          </Link>
        </p>
      </div>
    </div>
  )
}

export default Login
