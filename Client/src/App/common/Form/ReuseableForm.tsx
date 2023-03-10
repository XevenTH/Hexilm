import React from "react";

interface Props {
    submitHandler: (e: React.FormEvent<HTMLFormElement>) => void,
    children?: JSX.Element | JSX.Element[];
    formTitle?: string; // tambahkan prop formTitle
}

export default function ReuseableForm({ submitHandler, children, formTitle }: Props) {
    return (
        <form onSubmit={submitHandler}>
            {children}
            <div>
                <button className="duration-300 hover:brightness-90 hover:shadow-form rounded-full bg-gradient-to-r from-sky-400 via-purple-500 to-fuchsia-600 py-3 px-36 text-base font-semibold text-white outline-none my-5"
                >
                    {formTitle} {/* gunakan formTitle untuk menampilkan teks di dalam button */}
                </button>
            </div>
        </form>

    )
}
