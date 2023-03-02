export interface User {
    displayname: string,
    username: string,
    token: string
}

export interface UserLogin {
    email: string,
    password: string
}

export interface UserRegister {
    email: string,
    password: string
    displayname: string,
    username: string,
}

export class IntialUserRegister implements UserRegister {
    email = "";
    password = "";
    displayname = "";
    username = "";

    constructor(registerData?: UserRegister) {
        if (registerData) {
            this.email = registerData.email;
            this.password = registerData.password;
            this.displayname = registerData.displayname;
            this.username = registerData.username;
        }
    }
}

export class IntialUserLogin implements UserLogin {
    email = "";
    password = "";

    constructor(loginData?: UserLogin) {
        if (loginData) {
            this.email = loginData.email;
            this.password = loginData.password;
        }
    }
}
