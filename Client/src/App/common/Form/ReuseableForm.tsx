import React from "react";

interface Props {
    submitHandler: (e: React.FormEvent<HTMLFormElement>) => void,
    children?: JSX.Element | JSX.Element[];
}

export default function ReuseableForm({ submitHandler, children }: Props) {
    return (
        <form onSubmit={submitHandler}>
            {children}
            <div>
                <button className="hover:shadow-form rounded-md bg-[#6A64F1] py-3 px-8 text-base font-semibold text-white outline-none"
                >
                    Submit
                </button>
            </div>
        </form>

    )
}
