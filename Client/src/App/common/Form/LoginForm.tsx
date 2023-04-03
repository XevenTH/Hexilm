import React, { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Credentials, IntialUserLogin } from "../../model/user"
import { UseStore } from "../../Stores/BaseStore"
import ReuseableForm from "./ReuseableForm"
import ReuseableTextInput from "./ReuseableTextInput"

export default function LoginForm() {
  const { UserStore } = UseStore()
  const [error, setError] = useState("")

  const navigate = useNavigate()

  const [loginData, setLoginData] = useState<
    Pick<Credentials, "email" | "password">
  >(new IntialUserLogin())

  const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setLoginData({ ...loginData, [name]: value })
  }

  const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    UserStore.login(loginData)
      .then(() => navigate("/"))
      .catch((err) => {
        if (err instanceof Error) {
          setError(err.message)
          setTimeout(() => {
            setError("")
          }, 2000)
        }
      })
  }

  return (
    // tambah attribute type

    <ReuseableForm
      error={error}
      submitHandler={onSubmitHandler}
      formTitle="Login"
    >
      <ReuseableTextInput
        label="Email"
        name="email"
        type="email"
        onChangeHandler={onChangeHandler}
        nameSvg="bi bi-person-fill"
        placeholder="Enter your email"
      />
      <ReuseableTextInput
        label="Password"
        name="password"
        type="password"
        onChangeHandler={onChangeHandler}
        nameSvg="bi bi-lock"
        placeholder="Your password"
      />
    </ReuseableForm>
  )
}
