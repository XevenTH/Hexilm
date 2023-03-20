import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'

export default function Browse() {
  const [currentImage, setCurrentImage] = useState(0)

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImage((currentImage + 1) % 3)
    }, 7000)

    return () => clearInterval(interval)
  }, [currentImage])

  const images = [
    'https://source.unsplash.com/random/800x800',
    'https://source.unsplash.com/random/800x801',
    'https://source.unsplash.com/random/800x802',
  ]

  return (
    <div className="relative min-h-screen overflow-y-hidden ">
      {images.map((image, index) => (
        <img
          key={index}
          className={`absolute inset-0 transition-opacity duration-1000 w-full ${
            index === currentImage ? 'opacity-100' : 'opacity-0'
          }`}
          src={image}
          alt={`Unsplash ${index}`}
        />
      ))}
      <div className="absolute inset-0 bg-black opacity-70 bg-cover"></div>
      <div className="max-w-7xl mx-auto h-full flex flex-col justify-center relative z-9 min-h-screen">
        <div className="text-center">
          <h2 className="text-3xl font-extrabold tracking-tight text-white sm:text-4xl">
            <span className="block">
              Welcome to <span className="text-purple-500">CoolMovie</span>
            </span>
            <span className="block text-sky-400">
              The Best Website To Watch Movie
            </span>
          </h2>
          <p className="mt-4 text-lg text-slate-200">
            Experience the thrill of the big screen with the latest blockbusters
            and timeless classics
          </p>
          <div className="mt-6">
            <Link
              to={'/movies'}
              className="duration-300 inline-block shadow-lg hover:shadow-none bg-fuchsia-600 py-3 px-8 rounded-lg text-lg font-medium text-white hover:bg-purple-600 cursor-pointer z-20"
            >
              Go Watch!!
            </Link>
          </div>
        </div>
      </div>
    </div>
  )
}
