import RegisterForm from "../../App/common/Form/RegisterForm"
import { Link } from "react-router-dom"

function Register() {
  return (
    <div className="flex justify-center items-center w-full h-[97vh]">
      {/*tambah style untuk bikin ditengah modalnya */}
      <div className="bg-white border shadow-lg rounded-md max-w-xs p-9 sm:max-w-md ">
        <h3 className="text-center m-4 text-3xl font-bold">Register</h3>
        <RegisterForm />
        {/* Tombol Login/Register */}
        <p className="text-center text-sm font-medium text-[#5c5c5c]">
          Already have an account ?
          <Link
            to="/login"
            className="underline underline-offset-2 hover:opacity-80"
          >
            Login
            {/* Teks tombol disesuaikan dengan nilai state showLoginForm */}
          </Link>
        </p>
      </div>
    </div>
  )
}

export default Register
