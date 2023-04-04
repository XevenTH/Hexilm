import { makeAutoObservable } from "mobx"
import { Profile } from "../model/profile";

export default class ProfileStore {
    Profile: Profile | null = null;
    public ProfileStore(){
        makeAutoObservable(this)
    }
    
    }