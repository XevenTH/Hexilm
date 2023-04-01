import { makeAutoObservable, runInAction } from "mobx"
import ApiAgent from "../API/Agent"
import { Room } from "../model/userRoom"

export class UserRoomStore {
  userRooms: Room[] = []

  constructor() {
    makeAutoObservable(this)
  }

  getRoomList = async () => {
    try {
      var data = await ApiAgent.userRoomAPi.getRoomList()

      runInAction(() => {
        this.userRooms = data
      })
    } catch (error) {
      console.log(error)
    }
  }
}
