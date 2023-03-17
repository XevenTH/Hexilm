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

    return ( // tambah atribute type
        <ReuseableForm submitHandler={onSubmitHandler} formTitle="Register">
            <ReuseableTextInput label="Email" name="email" type="email" onChangeHandler={onChangeHandler} placeholder="Enter your email"/>
            <ReuseableTextInput label="Password" name="password" type="password" onChangeHandler={onChangeHandler} placeholder="8-16 char, at least contain 1 capital and number" />
            <ReuseableTextInput label="Username" name="username" type="text" onChangeHandler={onChangeHandler} placeholder="Enter your username" />
            <ReuseableTextInput label="DisplayName" name="displayname" type="text" onChangeHandler={onChangeHandler} placeholder="Enter your DisplayName" />
        </ReuseableForm>
    )
}
