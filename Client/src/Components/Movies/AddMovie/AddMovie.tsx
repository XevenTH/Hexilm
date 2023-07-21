import { ChangeEvent, useEffect, useState } from 'react'
import { UseStore } from '../../../App/Stores/BaseStore'

function AddMovie() {
  const [photoURL, setPhotoURL] = useState<string>('')
  const [isUploadingPhoto, setIsUploadingPhoto] = useState<boolean>(false)

  const { PhotoStore } = UseStore()

  useEffect(() => {
    window.scrollTo({
      top: 0,
    })
    document.body.style.background = '#181823'
    document.body.style.minHeight = '100vh'

    // Cleanup function to reset the styles on unmount
    return () => {
      document.body.style.background = ''
      document.body.style.minHeight = ''
    }
  }, [])

  const handleInputPhotoChange = async (e: ChangeEvent<HTMLInputElement>) => {
    setIsUploadingPhoto(true)
    const file = e.target.files?.[0]

    if (!file) return

    const resp = await PhotoStore.uploadPhoto(file)

    setPhotoURL(resp.data.url)
    setIsUploadingPhoto(false)
  }

  return (
    <div className="bg-[#181823] w-full h-full p-5 space-y-5 text-slate-200 max-w-3xl m-auto">
      <h1 className="text-3xl font-bold text-center mb-10">Add new movie</h1>

      <form className="grid grid-cols-12 gap-2">
        <input type="hidden" name="photoURL" value={photoURL} />
        <div className="col-span-12 flex flex-col space-y-2">
          <label className="text-xs text-slate-400" htmlFor="title">
            Title
          </label>
          <input
            required
            id="title"
            name="title"
            className="c p-2 text-sm text-slate-300 bg-transparent outline-none ring-[1px] ring-slate-600 rounded-lg focus:ring-cyan-600"
          />
        </div>
        <div className="col-span-12 flex flex-col space-y-2">
          <label className="text-xs text-slate-400" htmlFor="description">
            Description
          </label>
          <textarea
            required
            id="description"
            name="description"
            className="col-span-12 h-28 p-2 text-sm text-slate-300 bg-transparent outline-none ring-[1px] ring-slate-600 rounded-lg focus:ring-cyan-600"
          />
        </div>

        <div className="col-span-12 flex flex-col space-y-2">
          <label className="text-xs text-slate-400" htmlFor="image">
            Image
          </label>
          <label
            htmlFor="image"
            className="flex flex-col gap-4 items-center justify-center col-span-12 aspect-square text-sm text-slate-300 bg-transparent border-dashed border-slate-600 border rounded-lg cursor-pointer"
          >
            {photoURL ? (
              <img src={photoURL} className="w-full h-full object-cover" />
            ) : isUploadingPhoto ? (
              <p>Uploading...</p>
            ) : (
              <>
                <img
                  src="/icons/camera.svg"
                  className="w-10 h-10 object-contain text-gray-600 rounded-lg"
                />
                <p className="text-gray-500">Click to add image</p>
              </>
            )}
          </label>
          <input
            onChange={handleInputPhotoChange}
            type="file"
            id="image"
            hidden
          />
        </div>

        <button className="col-span-12 bg-purple-800 p-3 rounded-lg text-sm font-bold mt-5 mb-10">
          Add Movie
        </button>
      </form>
    </div>
  )
}

export default AddMovie
