import React from "react";

interface Props {
  submitHandler: (e: React.FormEvent<HTMLFormElement>) => void;
  children?: JSX.Element | JSX.Element[];
  formTitle?: string; // tambahkan prop formTitle
  error?: string;
}

export default function ReuseableForm({
  submitHandler,
  children,
  formTitle,
  error,
}: Props) {
  return (
    <form onSubmit={submitHandler}>
      {error && <p className="bg-red-200 text-red-700 p-3 ">{error}</p>}
      {children}
      <div>
        <button className="duration-300 hover:brightness-90 hover:shadow-form rounded-full bg-gradient-to-r from-sky-400 via-purple-500 to-fuchsia-600 py-3 px-[6.5rem] sm:px-36 max-w-full text-base font-semibold text-white outline-none my-5">
          {formTitle}{" "}
          {/* gunakan formTitle untuk menampilkan teks di dalam button */}
        </button>
      </div>
    </form>
  );
}
