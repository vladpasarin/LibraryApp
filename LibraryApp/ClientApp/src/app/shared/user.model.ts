export class User {
    id: number; 
    firstName: string;
    lastName: string;
    username: string;
    address: string;
    dateOfBirth: Date;
    email: string;
    phoneNr: string;
    overdueFees: number;
    constructor(input?: any) {
        Object.assign(this, input);
    }
}