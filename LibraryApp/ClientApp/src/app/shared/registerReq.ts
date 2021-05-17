export class RegisterReq {
    email: string;
    password: string;
    constructor(input?: any) {
        Object.assign(this, input);
    }
}