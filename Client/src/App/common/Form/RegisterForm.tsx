import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { IntialUserCredentials, Credentials } from "../../model/user";
import { UseStore } from "../../Stores/BaseStore";
import ReuseableForm from "./ReuseableForm";
import ReuseableTextInput from "./ReuseableTextInput";


export default function RegisterForm() {
    const { UserStore } = UseStore();
    const navigate = useNavigate();

    const [registerData, setRegisterData] = useState<Credentials>(new IntialUserCredentials());

    const onChangeHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRegisterData({ ...registerData, [name]: value })
    }

    const onSubmitHandler = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        UserStore.register(registerData).then(() => navigate("/movies"));
    }

    return (
        <ReuseableForm submitHandler={onSubmitHandler} formTitle="Register">
            <ReuseableTextInput label="Email" name="email" onChangeHandler={onChangeHandler} />
            <ReuseableTextInput label="Password" name="password" onChangeHandler={onChangeHandler} />
            <ReuseableTextInput label="Username" name="username" onChangeHandler={onChangeHandler} />
            <ReuseableTextInput label="Displayname" name="displayname" onChangeHandler={onChangeHandler} />
        </ReuseableForm>
    )
}
