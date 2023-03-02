import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { IntialUserLogin, UserLogin } from "../../model/user";
import { UseStore } from "../../Stores/BaseStore";
import ReuseableForm from "./ReuseableForm";
import ReuseableTextInput from "./ReuseableTextInput";

export default function LoginForm() {
    const { UserStore } = UseStore();
    const navigate = useNavigate();

    const [loginData, setLoginData] = useState<UserLogin>(new IntialUserLogin());

    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setLoginData({ ...loginData, [name]: value })
    }

    const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        UserStore.login(loginData).then(() => navigate("/movies"));
    }

    return (
        <ReuseableForm submitHandler={onSubmitHandler}>
            <ReuseableTextInput label="Email" name="email" onChangeHandler={onChangeHandler} />
            <ReuseableTextInput label="Password" name="password" onChangeHandler={onChangeHandler} />
        </ReuseableForm>
    )
}
