export class RegisterReq {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    phoneNr: string;
    dateOfBirth: Date;
    constructor(input?: any) {
        Object.assign(this, input);
    }
}