import React, { useState, useEffect, useRef } from 'react'
import ImageCrop from '../../App/layout/ReactImgCrop/App'
import { UseStore } from '../../App/Stores/BaseStore'
import './css/EditProfile.css'
import Profile from './Profile'
import { observer } from 'mobx-react-lite'
import { InitialEditProfile } from '../../App/model/profile'

export default observer(function EditProfile() {
  const {
    UserStore: { User },
    ProfileStore: { editProfile, getProfile, profile },
  } = UseStore()
  
  const [selectedFile, setSelectedFile] = useState<File | null>(null)
  const [edit, setEdit] = useState({ displayName: '', userName: '', bio: '' })

  const onChangeHandler = (event: any) => {
    const { name, value } = event.target
    setEdit((prevEdit) => ({ ...prevEdit, [name]: value }))
    if (name === 'displayName') {
      edit.displayName = value
    } else if (name === 'userName') {
      edit.userName = value
    } else if (name === 'bio') {
      edit.bio = value
    }
  }

  function handleSaveChanges() {
    const updatedEdit = {
      displayName: edit.displayName || profile?.displayName || '',
      userName: edit.userName || profile?.userName || '',
      bio: edit.bio || profile?.bio || '',
    }
    editProfile(updatedEdit)
  }

  useEffect(() => {
    try {
      if (User?.username) {
        getProfile(User?.username)
      }
    } catch (error) {
      console.log(error)
    }
  }, [])

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
            Name
          </label>
          <div>
            <input
              name="displayName"
              id="displayName"
              type="text"
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              onChange={onChangeHandler}
            />
          </div>
        </div>
        <div className="mb-5">
          <label htmlFor="username" className="text-white/70">
            Username
          </label>
          <div>
            <input
              name="userName"
              id="username"
              type="text"
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              onChange={onChangeHandler}
            />
          </div>
        </div>
        <div className="mb-5">
          <label htmlFor="bio" className="text-white/70">
            Bio
          </label>
          <div>
            <input
              name="bio"
              id="bio"
              type="text"
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              onChange={onChangeHandler}
            />
          </div>
        </div>

        <button
          type="submit"
          className="duration-200 bg-gray-700 hover:bg-gray-800 hover:text-white text-center rounded-lg p-1 w-32"
          onClick={handleSaveChanges}
        >
          Edit Profile
        </button>
      </div>
    </>
  )

  const [changeToPhoto, setChangeToPhoto] = useState(formText)

  const handleFileInputChange = (
    event: React.ChangeEvent<HTMLInputElement>,
  ) => {
    setSelectedFile(event.target.files ? event.target.files[0] : null)
  }

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    // Lakukan sesuatu dengan file yang dipilih
    console.log(selectedFile)
  }

  const backFromChangeToPhoto = (
    <>
      <div className="px-2">
        <button
          className="bg-red-600 text-gray-900 p-1 px-2 rounded-lg cursor-pointer"
          onClick={() => setChangeToPhoto(changeToPhoto)}
        >
          Cancel
        </button>
      </div>
      <form onSubmit={handleSubmit} className="form px-5">
        <ImageCrop />
      </form>
    </>
  )

  return <>{changeToPhoto}</>
})
