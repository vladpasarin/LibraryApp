export class AuthRequest {
    email: string;
    password: string;
    constructor(input?: any) {
        Object.assign(this, input);
    }
}