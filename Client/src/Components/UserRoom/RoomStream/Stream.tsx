export default function Stream(props: any) {
  const { closeLiveChat = !true, filterRoom, closeRecommend = !true } = props
  return (
    <div className="w-full overflow-y-scroll">
      <iframe
        className={`w-full ${
          closeLiveChat === false && closeRecommend === false ? 'h-3/4' : 'h-full'
        } border-white/20`}
        src="http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"
        title="YouTube video player"
        allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
        allowFullScreen
      ></iframe>
      <div className="w-full p-5">
        <h1 className="text-gray-200 font-medium text-4xl mb-5">{filterRoom.title}</h1>
        <div className="flex items-center text-white mb-5">
          <div>
            <img
              src="https://i.pinimg.com/550x/0e/51/7e/0e517eb57cb5a992ef3230b0e0d792af.jpg"
              alt=""
              className="duration-200 rounded-full border-2 hover:border-4 border-sky-500"
              width={80}
            />
            <div className="text-center">
              <p className="-mt-4">
                <span className="bg-red-600 px-1 rounded-md text-sm font-medium">
                  LIVE
                </span>
              </p>
            </div>
          </div>
          <a
            href="#"
            className="px-4 text-2xl font-semibold hover:underline decoration-1 decoration-sky-500"
          >
            TenZ
          </a>
        </div>
        <div className="bg-gray-700 rounded-lg w-full p-5 text-white">
          <p>
            Description, Lorem ipsum dolor sit amet consectetur adipisicing
            elit. Velit quod cumque rem at optio numquam rerum asperiores
            dolorem corrupti ducimus?
          </p>
        </div>
      </div>
    </div>
  )
}
