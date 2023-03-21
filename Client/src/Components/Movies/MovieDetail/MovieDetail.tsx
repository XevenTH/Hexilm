import { observer } from "mobx-react-lite"
import { useParams } from "react-router-dom"
import ApiAgent from "../../../App/API/Agent"
import { useEffect, useState } from "react"
import { Movie } from "../../../App/model/movie"

export default observer(function MovieDetail() {
  const { id } = useParams()
  const [loading, setloading] = useState(true)

  const [movie, setMovie] = useState<Movie | null>(null)

  useEffect(() => {
    ApiAgent.movieApi
      .getMovie(id!)
      .then((mov) => {
        setMovie(mov)
      })
      .catch((err) => console.log(err))
      .finally(() => {
        setloading(false)
      })
  }, [])

  if (loading) return <p>Loading...</p>

  if (!movie) return <p>404 Not Found</p>

  return (
    <div>
      <img
        src="https://images.unsplash.com/photo-1494972688394-4cc796f9e4c5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8d2FyfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
        alt="cover"
        className="h-[240px] lg:h-[400px] w-full object-cover relative -z-[3] filter brightness-75"
      />

      <div className="flex md:p-8 rounded-tl-xl rounded-tr-xl bg-[#181823] -mt-5 min-h-screen shadow-xl">
        <img
          src="https://images.unsplash.com/photo-1616530940355-351fabd9524b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTR8fG1vdmllfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
          alt="cover"
          className="object-cover aspect-[6/8] hidden md:block w-[max(30%,240px)] rounded-xl self-start"
        />
        <div className="px-5 text-neutral-200">
          <h1 className="text-3xl text-neutral-100 my-5 font-semibold">
            The War
          </h1>

          <h3 className="text-md text-gray-500 font-bold">SYNOPSIS</h3>
          <p className="text-md text-gray-200">
            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Explicabo
            suscipit fugit ad similique iusto, omnis officiis atque, repellat,
            dolor sapiente et qui voluptas earum quia in. Pariatur soluta
            consectetur quod.
          </p>
        </div>
      </div>
    </div>
  )
})

// {
//     "id": "1bf60c1a-dbf4-4da1-aa79-f7b7fca2581e",
//     "title": "Marvel",
//     "description": "This is Marvel Movie Description",
//     "picture": ""
//   },
