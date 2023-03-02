import { makeAutoObservable, runInAction } from "mobx";
import ApiAgent from "../API/Agent";
import { User, UserLogin, UserRegister } from "../model/user";
import { storeContainer } from "./BaseStore";

export default class CommonStore {
    User: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get IsLogging() {
        return !!this.User;
    }

    login = async (loginData: UserLogin) => {
        try {
            const user = await ApiAgent.accountApi.login(loginData);

            runInAction(() => {
                this.User = user;
                storeContainer.CommonStore.setToken(user.token);
            })
        } catch (error) {
            console.log(error);
        }
    }

    register = async (registerData: UserRegister) => {
        try {
            const user = await ApiAgent.accountApi.register(registerData);
            
            runInAction(() => {
                this.User = user;
                storeContainer.CommonStore.setToken(user.token);
            })
        } catch (error) {
            console.log(error);
        }
    }

    logout = () => {
        this.User = null;
        storeContainer.CommonStore.setToken(null);
    }

    getUser = async () => {
        try {
            const user = await ApiAgent.accountApi.getUser();

            runInAction(() => {
                this.User = user;
            });

        } catch (error) {
            runInAction(() => {
                console.log(error);
            })
        }
    }
}