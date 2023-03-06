import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

export default function Browse() {
  const [currentImage, setCurrentImage] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImage((currentImage + 1) % 3);
    }, 7000);

    return () => clearInterval(interval);
  }, [currentImage]);

  const images = [
    'https://source.unsplash.com/random/800x800',
    'https://source.unsplash.com/random/800x801',
    'https://source.unsplash.com/random/800x802'
  ];

  return (
    <div className="relative h-screen overflow-y-hidden">
      {images.map((image, index) => (
        <img
          key={index}
          className={`absolute inset-0 transition-opacity duration-1000 w-full ${index === currentImage ? 'opacity-100' : 'opacity-0'
            }`}
          src={image}
          alt={`Unsplash ${index}`}
        />
      ))}
      <div className="absolute inset-0 bg-black opacity-70 bg-cover"></div>
      <div className="max-w-7xl mx-auto h-full flex flex-col justify-center relative z-10">
        <div className="text-center">
          <h2 className="text-3xl font-extrabold tracking-tight text-white sm:text-4xl">
            <span className="block">Welcome to <span className="text-red-500">CoolMovie</span></span>
            <span className="block text-indigo-600">The Best Website To Watch Movie</span>
          </h2>
          <p className="mt-4 text-lg text-white">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ac lorem vel nibh fringilla commodo. Sed
            condimentum, nunc sed ultricies facilisis, ex odio dictum elit, nec aliquam sapien massa ac ex.
          </p>
          <div className="mt-6">
            <Link to={"/movies"} className="inline-block bg-indigo-600 py-3 px-8 rounded-lg text-lg font-medium text-white hover:bg-indigo-700">
              Go Watch!!
            </Link>
          </div>
        </div>
      </div>
    </div>

  );
}
