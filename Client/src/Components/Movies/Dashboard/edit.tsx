import { observer } from "mobx-react-lite";
import { useState } from "react"
import ReuseableForm from "../../../App/common/ReuseableForm";
import { Movie } from "../../../App/model/movie"
import { UseStore } from "../../../App/Stores/BaseStore";

export default observer(function EditCompo() {
    const { MovieStore: { selectedMovie, OpenFormCloseDetailsHandler: setOpenFormHandler, EditCreateHandler } } = UseStore()

    let intialValue = selectedMovie;

    if (intialValue == undefined) {
        intialValue = {
            id: "",
            title: "",
            picture: null
        }
    }

    const [formValue, setFormValue] = useState<Movie>(intialValue);

    const onChangehandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormValue({ ...formValue, [name]: value })
        console.log(formValue);
    }

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        EditCreateHandler(formValue);
        setOpenFormHandler(false);
        // console.log(formValue);
    }

    return (
        <div className="mt-4 max-w-lg bg-blue-300 max-h-fit m-4 p-2">
            <div className="text-xl font-semibold text-blue-800 max-w-full te text-center">
                Edit
            </div>
            <div className="bg-green-600 h-1 my-1"></div>
            <div className="text-slate-800 font-bold mt-5 text-2xl text-center m-2">
                {selectedMovie ?
                    selectedMovie.title : "New Title"}
            </div>
            <div className="flex items-center justify-center p-4">
                <div className="mx-auto w-full max-w-[550px]">
                    <ReuseableForm onChangehandler={onChangehandler} submitHandler={submitHandler} />
                </div>
            </div>
        </div>
    )
})
