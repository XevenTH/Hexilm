export default function Recommend(props: any) {
  const { closeRecommend, setCloseRecommend } = props
  const handleClick = () => {
    // Mengubah nilai closeRecommend
    setCloseRecommend(!closeRecommend)
  }
  return (
    <div
      className={`duration-200 bg-gray-800 text-white h-screen ${
        closeRecommend
          ? 'closeRecommend overflow-y-scroll'
          : 'hover:overflow-y-scroll'
      }  ${closeRecommend ? 'w-20' : 'w-96'}`}
    >
      <div className="flex items-center justify-center p-2">
        <h2 className={`${closeRecommend && 'hidden'}`}>RECOMMEND MOVIES</h2>
        <i
          className={`bi ${
            closeRecommend
              ? 'bi-arrow-bar-right text-center'
              : 'bi-arrow-bar-left ml-auto'
          } px-1 rounded-lg hover:bg-white/20 text-xl`}
          onClick={handleClick}
        ></i>
      </div>
      <ul>
        {Array.from({ length: 20 }).map((_, index) => {
          return (
            <li className="p-2 hover:bg-gray-900 flex items-center justify-center">
              <img
                src="https://media.tenor.com/NICoVNbKVGYAAAAM/profile-picture.gif"
                alt=""
                className="rounded-full bg-cover"
                width={40}
              />
              {!closeRecommend && (
                <>
                  <div className="ml-2">
                    <h3 className="text-sm font-semibold">DisplayName</h3>
                    <p className="text-sm text-gray-300">Movie Name</p>
                  </div>
                  <div className="flex items-center ml-auto">
                    <p className="bg-red-700 rounded-full w-2 h-2 mr-1"></p>
                    {index + 1}.4k
                  </div>
                </>
              )}
            </li>
          )
        })}
      </ul>
    </div>
  )
}
