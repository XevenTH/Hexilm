import React from "react"

interface Props {
    submitHandler: (e: React.FormEvent<HTMLFormElement>) => void,
    onChangehandler: (e: React.ChangeEvent<HTMLInputElement>) => void
}

export default function ReuseableForm({ submitHandler, onChangehandler }: Props) {
    return (
        <form onSubmit={submitHandler}>
            <div className="mb-5">
                <label
                    className="mb-3 block text-base font-medium text-[#07074D]">
                    New Title
                </label>
                <input
                    onChange={onChangehandler}
                    // value={formValue.title}
                    type="text"
                    name="title"
                    placeholder="Enter The New Title"
                    className="w-full rounded-md border border-[#e0e0e0] bg-white py-3 px-6 text-base font-medium text-[#6B7280] outline-none focus:border-[#6A64F1] focus:shadow-md"
                />
            </div>
            <div>
                <button className="hover:shadow-form rounded-md bg-[#6A64F1] py-3 px-8 text-base font-semibold text-white outline-none"
                >
                    Submit
                </button>
            </div>
        </form>

    )
}
