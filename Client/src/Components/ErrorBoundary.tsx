import { useRouteError } from "react-router-dom";

type ErrorRoute = {
    status: number,
    statusText:string,
}

function ErrorBoundary() {
    let error = useRouteError() as ErrorRoute;


    return <div className="w-screen h-screen flex flex-col justify-center items-center bg-[#181823]">
        <p className="text-5xl text-slate-400 font-bold mb-5">{error.status}</p>
        <p className="text-lg text-slate-400 ">{error.statusText}</p>
    </div>;
  }

  export default ErrorBoundary