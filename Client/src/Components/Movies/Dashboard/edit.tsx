import { useState } from "react"
import ReuseableForm from "../../../App/common/ReuseableForm";
import { Movie } from "../../../App/model/movie"

interface Props {
    Movie: Movie
    EditCreateHandler: (movie: Movie) => void,
    selectedMovieHandler: (id: string) => void,
    setOpenForm: (state: boolean) => void
}

export default function EditCompo({ Movie, EditCreateHandler, selectedMovieHandler, setOpenForm }: Props) {
    const intialValue = Movie ?? {
        id: "",
        title: "",
        picture: ""
    }

    const [formValue, setFormValue] = useState<Movie>(intialValue);

    const onChangehandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormValue({ ...formValue, [name]: value })
    }

    const submitHandler = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        EditCreateHandler(formValue);
        selectedMovieHandler(formValue.id);
        setOpenForm(false);
    }

    return (
        <div className="mt-4 max-w-lg bg-blue-300 max-h-fit m-4 p-2">
            <div className="text-xl font-semibold text-blue-800 max-w-full te text-center">
                Edit
            </div>
            <div className="bg-green-600 h-1 my-1"></div>
            <div className="text-slate-800 font-bold mt-5 text-2xl text-center m-2">
                {Movie.title}
            </div>
            <div className="flex items-center justify-center p-4">
                <div className="mx-auto w-full max-w-[550px]">
                    <ReuseableForm onChangehandler={onChangehandler} submitHandler={submitHandler} />
                </div>
            </div>
        </div>
    )
}
