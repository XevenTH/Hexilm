import { makeAutoObservable, runInAction } from "mobx";
import ApiAgent from "../API/Agent";
import { UserRoom } from "../model/userRoom";

export class UserRoomStore {
    userRooms: UserRoom[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    getRoomList = async () => {
        try {
            var data = await ApiAgent.userRoomAPi.getRoomList();

            runInAction(() => {
                this.userRooms = data;
            })
        } catch (error) {
            console.log(error);
        }
    }
}