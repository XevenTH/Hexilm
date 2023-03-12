import { observer } from "mobx-react-lite"
import { UseStore } from "../../../App/Stores/BaseStore"

export default observer(function MovieDetails() {
    const { MovieStore: { selectedMovie, OpenFormCloseDetailsHandler: setOpenFormHandler } } = UseStore()

    return (
        <div className="m-4 max-w-lg max-h-fit p-2 bg-slate-300 rounded-md">
            <h2 className='text-center font-semibold text-lg'>
                Movie Details
            </h2>
            <div className='bg-green-700 h-2 w-full rounded-lg'></div>
            <div className="my-2 text-center font-bold text-xl space-x-2">
                {selectedMovie ?
                    selectedMovie.title : "New Title"}
            </div>
            <div className="my-2 text-center text-xl space-x-2">
                This is Picture
            </div>
            <div className="flex justify-center w-full gap-5 m-4">
                <button className="bg-red-200 p-2 rounded-xl shadow-md font-semibold capitalize w-24" onClick={() => setOpenFormHandler(true)}>Edit</button>
                <button className="bg-red-200 p-2 rounded-xl shadow-md font-semibold capitalize w-24" onClick={() => setOpenFormHandler(false)}>Cancel</button>
            </div>
        </div>
    )
})
