import { observer } from 'mobx-react-lite'
import React, { useEffect, useState } from 'react'
import ImageCrop from '../../App/layout/ReactImgCrop/App'
import { InitialEditProfile, Profile } from '../../App/model/profile'
import { UseStore } from '../../App/Stores/BaseStore'
import './css/EditProfile.css'

export default observer(function EditProfile() {
  const {
    ProfileStore: { editProfile, profile },
  } = UseStore()
  

  const [activePage, setActivePage] = useState(
    Number(localStorage.getItem('fastPageEditPhoto')) || 0
  )

  useEffect(()=>localStorage.removeItem('fastPageEditPhoto'))

  useEffect(() => {
    const handleBeforeUnload = () => {
      localStorage.setItem('fastPageEditPhoto', activePage.toString())
    }

    window.addEventListener('beforeunload', handleBeforeUnload)

    return () => {
      window.removeEventListener('beforeunload', handleBeforeUnload)
    }
  }, [activePage])

  const [selectedFile, setSelectedFile] = useState<File | null>(null)
  const [editData, setEditData] = useState<
    Pick<Profile, 'displayName' | 'userName' | 'bio'>
  >(new InitialEditProfile(profile))

  const onChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target
    setEditData((prevEdit) => {
      return { ...prevEdit, [name]: value }
    })
  }

  const handleSaveChanges = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    editProfile(editData)
    window.location.reload()
  }
  

  const formText = (
    <>
      <form
        className="grid grid-cols-1 place-items-center text-white/80"
        onSubmit={handleSaveChanges}
      >
        <img
          src={profile?.photos.find((x) => x.isMain)?.url}
          alt=""
          width={80}
          className="rounded-lg cursor-pointer hover:opacity-90 mb-2"
          onClick={() => setActivePage(1)}
        />
        <div
          className="duration-100 text-blue-600 hover:text-white cursor-pointer font-medium mb-4"
          onClick={() => setActivePage(1)}
        >
          Edit Photo
        </div>

        <div className="mb-5">
          <label htmlFor="displayName" className="text-white/70">
            Name
          </label>
          <div>
            <input
              onChange={onChangeHandler}
              name="displayName"
              type="text"
              defaultValue={editData.displayName}
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
            />
          </div>
        </div>
        <div className="mb-5">
          <label htmlFor="username" className="text-white/70">
            Username
          </label>
          <div>
            <input
              onChange={onChangeHandler}
              name="userName"
              type="text"
              defaultValue={editData.userName}
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
              onKeyDown={(event) => {
                if (event.key === ' ') {
                  event.preventDefault()
                }
              }}
            />
          </div>
          <p className="text-sm opacity-50">No spaces allowed in username!</p>
          <p className="text-sm text-red-700 absolute">Changing username will log you out.</p>
        </div>
        <div className="mb-5">
          <label htmlFor="bio" className="text-white/70">
            Bio
          </label>
          <div>
            <input
              onChange={onChangeHandler}
              name="bio"
              type="text"
              defaultValue={editData.bio}
              className="w-52 focus:outline-none bg-inherit border-white/70 border-b"
              autoComplete="off"
            />
          </div>
        </div>
        <button
          type="submit"
          className="duration-200 bg-gray-700 hover:bg-gray-800 hover:text-white text-center rounded-lg p-1 w-32"
        >
          Edit Profile
        </button>
      </form>
    </>
  )

  const backFromChangeToPhoto = (
    <>
      <div className="px-2">
        <button
          className="bg-red-600 text-gray-900 p-1 px-2 rounded-lg cursor-pointer"
          onClick={() => setActivePage(0)}
        >
          Cancel
        </button>
      </div>
        <ImageCrop profilePhoto={profile}/>
    </>
  )

  const PAGE = {
    0: formText,
    1: backFromChangeToPhoto,
  }

  return <div className="pb-24">{PAGE[activePage as keyof typeof PAGE]}</div>
})
