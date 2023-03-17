import { observer } from 'mobx-react-lite'
import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import LoginForm from '../../App/common/Form/LoginForm'
import RegisterForm from '../../App/common/Form/RegisterForm'
import { UseStore } from '../../App/Stores/BaseStore'

export default observer(function Home() {
  const { UserStore, ModalStore } = UseStore()
  const navigate = useNavigate()

  const [showLoginForm, setShowLoginForm] = useState(true)

  // Handler saat tombol Login/Register di klik
  const handleToggleForm = () => {
    setShowLoginForm((prevState) => !prevState)
  }

  useEffect(() => {
    ModalStore.setOpen(showLoginForm ? <LoginForm /> : <RegisterForm />)
  }, [showLoginForm])

  useEffect(() => {
    document.body.style.backgroundImage = 'linear-gradient(to top right, #22d3ee, #8f3bff, #f02fc2)';
    document.body.style.minHeight = '100vh';
  
    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.backgroundImage = '';
      document.body.style.minHeight = '';
    };
  }, []);
  
  

  return (
    <>
      {/* Cek apakah user sudah login */}
      {!UserStore.IsLogging ? (
        <div>
          <div className="flex justify-center mb-4 gap-2 font-semibold text-slate-200"></div>
          {ModalStore.Modal.isOpen && (
            <div className="flex justify-center items-center w-full h-[97vh]"> {/*tambah style untuk bikin ditengah modalnya */}
              <div className="bg-white border shadow-lg rounded-md max-w-xs p-9 sm:max-w-md ">
                <h3 className="text-center m-4 text-3xl font-bold">
                  {showLoginForm ? 'Login' : 'Register'}
                </h3>
                {ModalStore.Modal.content}
                {/* Tombol Login/Register */}
                <p className='text-center text-sm font-medium text-[#5c5c5c]'>
                {showLoginForm
                  ? "Don't have an account yet? "
                  : 'Already have an account? '}
                  <button
                  className="underline underline-offset-2 hover:opacity-80"
                  onClick={handleToggleForm} // Panggil handler saat tombol di klik
                >
                  {showLoginForm ? 'Register' : 'Login'}{' '}
                  {/* Teks tombol disesuaikan dengan nilai state showLoginForm */}
                </button>
                </p>
                
                
              </div>
            </div>
          )}
        </div>
      ) : (
        <div className="w-full flex flex-col items-center gap-2 pt-56 text-center text-white drop-shadow-md">
          <h1 className="text-6xl   font-bold mb-2">Welcome to our world of movies</h1>
          <p className='text-xl text-slate-200 mb-4'>Experience the thrill of a lifetime with our movie - where every moment counts!</p>
          <button
            className="duration-300 bg-sky-400 hover:bg-sky-500 shadow-lg hover:shadow-none p-2 rounded-xl"
            onClick={() => navigate('/browse')}
          >
            Find Movies
          </button>
        </div>
      )}
    </>
  )
})
