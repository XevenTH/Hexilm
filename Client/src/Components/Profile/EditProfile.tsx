import React, { useState, useEffect } from "react"
import ImageCrop from "../../App/layout/ReactImgCrop/App"
import { UseStore } from "../../App/Stores/BaseStore"
import "./css/EditProfile.css"

export default function EditProfile() {
  const {
    UserStore: { User },
  } = UseStore()
  const [selectedFile, setSelectedFile] = useState<File | null>(null)
  const [name, setName] = useState(User?.displayName || "")
  const [username, setUsername] = useState(User?.userName || "")

  //Disini state udh berubah tapi di inputnya belum
  console.log(name)

  const handleNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    console.log(e.target.value)
    setName(e.target.value)
  }

  const handleUsernameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(e.target.value)
  }

  const formText = (
    <>
      <div className="grid grid-cols-1 place-items-center text-white/80">
        <img
          src="https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dXNlciUyMHByb2ZpbGV8ZW58MHx8MHx8&w=1000&q=80"
          alt=""
          width={80}
          className="rounded-lg cursor-pointer hover:opacity-90 mb-2"
          onClick={() => setChangeToPhoto(backFromChangeToPhoto)}
        />
        <div
          className="duration-100 text-blue-600 hover:text-white cursor-pointer font-medium mb-4"
          onClick={() => setChangeToPhoto(backFromChangeToPhoto)}
        >
          Edit Photo
        </div>

        <div className="mb-5">
          <label htmlFor="displayName" className="text-white/70">
            Namee
          </label>
          <div>
            <input
              id="displayName"
              type="text"
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              value={name}
              onChange={handleNameChange}
            />
          </div>
        </div>
        <div className="mb-5">
          <label htmlFor="username" className="text-white/70">
            Username
          </label>
          <div>
            <input
              id="username"
              type="text"
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              value={username}
              onChange={handleUsernameChange}
            />
          </div>
        </div>

        <button
          type="submit"
          className="duration-200 bg-gray-700 hover:bg-gray-800 hover:text-white text-center rounded-lg p-1 w-32"
        >
          Edit Profile
        </button>
      </div>
    </>
  )

  const backFromChangeToPhoto = (
    <>
      <div>
        <button
          className="bg-red-600 text-gray-900 p-1 px-2 rounded-lg cursor-pointer"
          onClick={() => setChangeToPhoto(changeToPhoto)}
        >
          Cancel
        </button>
      </div>
      <ImageCrop />
    </>
  )

  const [changeToPhoto, setChangeToPhoto] = useState(formText)

  const handleFileInputChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSelectedFile(event.target.files ? event.target.files[0] : null)
  }

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    // Lakukan sesuatu dengan file yang dipilih
    console.log(selectedFile)
    alert(name)
    alert(username)
  }

  return (
    <form onSubmit={handleSubmit} className="form px-5">
      {changeToPhoto}
    </form>
  )
}
