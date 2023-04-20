import { useEffect } from 'react'

function AddMovie() {
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

  return (
    <div className="bg-[#181823] w-full h-full p-5 space-y-5 text-slate-200 max-w-3xl m-auto">
      <h1 className="text-3xl font-bold text-center mb-10">Add new movie</h1>
      <form className="grid grid-cols-12 gap-2">
        <div className="col-span-12 flex flex-col space-y-2">
          <label className="text-xs text-slate-400" htmlFor="title">
            Title
          </label>
          <input
            id="title"
            className="c p-2 text-sm text-slate-300 bg-transparent outline-none ring-[1px] ring-slate-600 rounded-lg focus:ring-cyan-600"
          />
        </div>
        <div className="col-span-12 flex flex-col space-y-2">
          <label className="text-xs text-slate-400" htmlFor="description">
            Description
          </label>
          <textarea
            id="description"
            className="col-span-12 h-28 p-2 text-sm text-slate-300 bg-transparent outline-none ring-[1px] ring-slate-600 rounded-lg focus:ring-cyan-600"
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
