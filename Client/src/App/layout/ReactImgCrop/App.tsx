import './styles.css'
import React, { useState, useRef, useEffect } from 'react'

import ReactCrop, {
  centerCrop,
  makeAspectCrop,
  Crop,
  PixelCrop,
} from 'react-image-crop'
import { canvasPreview } from './canvasPreview'
import { useDebounceEffect } from './useDebounceEffect'

import 'react-image-crop/dist/ReactCrop.css'
import { UseStore } from '../../Stores/BaseStore'
import { json } from 'react-router-dom'

// This is to demonstate how to make and center a % aspect crop
// which is a bit trickier so we use some helper functions.
function centerAspectCrop(
  mediaWidth: number,
  mediaHeight: number,
  aspect: number,
) {
  return centerCrop(
    makeAspectCrop(
      {
        unit: '%',
        width: 90,
      },
      aspect,
      mediaWidth,
      mediaHeight,
    ),
    mediaWidth,
    mediaHeight,
  )
}

export default function App(props: any) {
  const [imgSrc, setImgSrc] = useState('')
  const previewCanvasRef = useRef<HTMLCanvasElement>(null)
  const imgRef = useRef<HTMLImageElement>(null)
  const hiddenAnchorRef = useRef<HTMLAnchorElement>(null)
  const blobUrlRef = useRef('')
  const [crop, setCrop] = useState<Crop>()
  const [completedCrop, setCompletedCrop] = useState<PixelCrop>()
  const [scale, setScale] = useState(1)
  const [rotate, setRotate] = useState(0)
  const [aspect, setAspect] = useState<number | undefined>(16 / 9)

  const [dataSVPhoto, setDataSVPhoto] = useState<any>(null)
  const [btnSVPhoto, setbtnSVPhoto] = useState(false)
  const [loadbtnSVPhoto, setLoadBtnSVPhoto] = useState(false)

  const { profilePhoto } = props
  const {
    PhotoStore: { changePhoto, uploadPhoto, savePhoto },
    UserStore: { User },
  } = UseStore()

  function addHorizontalScrollListener(element: HTMLElement) {
    element.addEventListener('wheel', (e) => {
      e.preventDefault()
      element.scrollLeft += e.deltaY
    })
  }
  useEffect(() => {
    const AttendeesXScroll = document.querySelector(
      '.xScroll',
    ) as HTMLDivElement
    addHorizontalScrollListener(AttendeesXScroll)
  }, [])

  function onSelectFile(e: React.ChangeEvent<HTMLInputElement>) {
    if (e.target.files && e.target.files.length > 0) {
      setCrop(undefined) // Makes crop preview update between images.
      const reader = new FileReader()
      reader.addEventListener('load', () =>
        setImgSrc(reader.result?.toString() || ''),
      )
      reader.readAsDataURL(e.target.files[0])
    }
  }

  function onImageLoad(e: React.SyntheticEvent<HTMLImageElement>) {
    if (aspect) {
      const { width, height } = e.currentTarget
      setCrop(centerAspectCrop(width, height, aspect))
    }
  }

  function addImage() {
    setLoadBtnSVPhoto(true)
    if (!previewCanvasRef.current) {
      throw new Error('Crop canvas does not exist')
    }

    previewCanvasRef.current.toBlob(async (blob) => {
      if (!blob) {
        throw new Error('Failed to create blob')
      }
      if (blobUrlRef.current) {
        URL.revokeObjectURL(blobUrlRef.current)
      }
      blobUrlRef.current = URL.createObjectURL(blob)
      const dataPhoto = await uploadPhoto(blob)
      if (dataPhoto) {
        setLoadBtnSVPhoto(false)
        setbtnSVPhoto(true)
        setDataSVPhoto(dataPhoto)
      }
    })
  }

  function triggerSavePhoto() {
    const data = dataSVPhoto?.data
    savePhoto(data.publicId,data.url,User?.id)
    window.location.reload()
  }

  useDebounceEffect(
    async () => {
      if (
        completedCrop?.width &&
        completedCrop?.height &&
        imgRef.current &&
        previewCanvasRef.current
      ) {
        // We use canvasPreview as it's much faster than imgPreview.
        canvasPreview(
          imgRef.current,
          previewCanvasRef.current,
          completedCrop,
          scale,
          rotate,
        )
      }
    },
    100,
    [completedCrop, scale, rotate],
  )

  function handleToggleAspectClick() {
    if (aspect) {
      setAspect(undefined)
    } else if (imgRef.current) {
      const { width, height } = imgRef.current
      setAspect(16 / 9)
      setCrop(centerAspectCrop(width, height, 16 / 9))
    }
  }

  return (
    <div className="App text-white md:mt-0 mt-5">
      <div className="Crop-Controls">
        <div className="grid md:grid-cols-6 md:place-items-center">
          <div className="md:col-start-3 md:col-end-5">
            <div className="flex md:justify-center mb-5">
              <img
                src={
                  profilePhoto?.photos.find((x: any) => x.isMain)?.url ??
                  'https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8dXNlciUyMHByb2ZpbGV8ZW58MHx8MHx8&w=1000&q=80'
                }
                alt=""
                width={80}
                className="rounded-lg"
              />
            </div>
            <h2>Change Image</h2>
            <div
              className={`duration-300 rounded-lg flex px-2 p-1 mb-5 overflow-x-scroll xScroll ${
                !profilePhoto?.photos && 'w-0 h-0 absolute top-0 left-0'
              } w-full static`}
            >
              {profilePhoto?.photos.map((photo: any) => (
                <img
                  key={photo.id}
                  src={photo.url}
                  onClick={() => {
                    changePhoto(photo.id)
                    window.location.reload()
                  }}
                  alt=""
                  width={80}
                  className="rounded-lg mr-5"
                />
              ))}
            </div>
            <label className="w-full py-2 px-3 bg-gray-700 rounded-lg text-white font-semibold shadow-md cursor-pointer duration-200 hover:bg-gray-800">
              <i className="bi bi-image mr-2"></i>
              <span>Add Image</span>
              <input
                type="file"
                accept="image/*"
                onChange={onSelectFile}
                className="hidden"
              />
            </label>
          </div>
        </div>
      </div>
      {!!imgSrc && (
        <>
          <div className="Crop-Controls">
            <div className="mt-2">
              <label htmlFor="scale-input">Scale </label>
              <input
                id="scale-input"
                type="number"
                step="0.1"
                className="ml-7 w-52 rounded-lg bg-inherit border-white border text-white px-2"
                value={scale}
                disabled={!imgSrc}
                onChange={(e) => setScale(Number(e.target.value))}
              />
            </div>
            <div>
              <label htmlFor="rotate-input">Rotate </label>
              <input
                id="rotate-input"
                type="number"
                className="ml-5 w-52 rounded-lg bg-inherit border-white border text-white px-2"
                value={rotate}
                disabled={!imgSrc}
                onChange={(e) =>
                  setRotate(
                    Math.min(180, Math.max(-180, Number(e.target.value))),
                  )
                }
              />
            </div>
            <div>
              <button onClick={handleToggleAspectClick}>
                Toggle aspect {aspect ? 'off' : 'on'}
              </button>
            </div>
          </div>
          <ReactCrop
            crop={crop}
            onChange={(_, percentCrop) => setCrop(percentCrop)}
            onComplete={(c) => setCompletedCrop(c)}
            aspect={aspect}
          >
            <img
              ref={imgRef}
              alt="Crop me"
              src={imgSrc}
              style={{ transform: `scale(${scale}) rotate(${rotate}deg)` }}
              onLoad={onImageLoad}
            />
          </ReactCrop>
        </>
      )}
      {!!completedCrop && (
        <>
          <div>
            <canvas
              ref={previewCanvasRef}
              style={{
                border: '1px solid black',
                objectFit: 'contain',
                width: completedCrop.width,
                height: completedCrop.height,
              }}
            />
          </div>
          <div className="mt-2">
            {loadbtnSVPhoto ? (
              <img
                src="https://www.wpfaster.org/wp-content/uploads/2013/06/loading-gif.gif"
                alt=""
                width={40}
              />
            ) : !btnSVPhoto && (
              <button
                onClick={addImage}
                className="py-2 px-3 bg-gray-700 rounded-lg text-white font-semibold shadow-md cursor-pointer duration-200 hover:bg-gray-800"
              >
                Add Image
              </button>
            )}
            {btnSVPhoto && (
              <button
                className="py-2 px-3 bg-gray-700 rounded-lg text-white font-semibold shadow-md cursor-pointer duration-200 hover:bg-gray-800"
                onClick={triggerSavePhoto}
              >
                Save Photo
              </button>
            )}
            <a
              ref={hiddenAnchorRef}
              download
              style={{
                position: 'absolute',
                top: '-200vh',
                visibility: 'hidden',
              }}
            >
              Hidden download
            </a>
          </div>
        </>
      )}
    </div>
  )
}
