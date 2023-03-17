import { observer } from "mobx-react-lite";
import { useState } from "react"
import ReuseableForm from "../../../App/common/Form/ReuseableForm";
import ReuseableTextInput from "../../../App/common/Form/ReuseableTextInput";
import NewMovie, { Movie } from "../../../App/model/movie"
import { UseStore } from "../../../App/Stores/BaseStore";

export default observer(function Edit() {
    const { MovieStore } = UseStore();
    const { selectedMovie, OpenFormCloseDetailsHandler: setOpenFormHandler, EditMovie, CreateNewMovie } = MovieStore

    const [formValue, setFormValue] = useState<Movie>(new NewMovie(selectedMovie));

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()

        if (formValue.id) {
            EditMovie(formValue);
        }
        else {
            CreateNewMovie(formValue);
        }

        setOpenFormHandler(false);
    }

    const onChangeHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormValue({ ...formValue, [name]: value })
    }


    return (
        <div className="rounded mt-4 max-w-lg bg-blue-300 max-h-fit m-4 p-2">
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
                    <ReuseableForm submitHandler={submitHandler} formTitle="Save">
                        <ReuseableTextInput onChangeHandler={onChangeHandler} label="Title" name="title" type="text" placeholder="Input movie's title" />
                    </ReuseableForm>
                </div>
            </div>
        </div>
    )
})
