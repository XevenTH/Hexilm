import { makeAutoObservable } from "mobx"

export interface Modal {
    content: JSX.Element | null,
    isOpen: Boolean
}

export class ModalStore {
    Modal: Modal = {
        content: null,
        isOpen: false
    }

    constructor() {
        makeAutoObservable(this);
    }

    setOpen = (jsx: JSX.Element) => {
        this.Modal.content = jsx;
        this.Modal.isOpen = true;
    }

    setClose = () => {
        this.Modal.content = null;
        this.Modal.isOpen = true;
    }
}