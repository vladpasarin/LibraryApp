import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
    providedIn: "root",
})
  
export class ApiService {
    constructor(private http: HttpClient) {}

    header = new HttpHeaders({
        "Content-Type": "application/json",
    });

    baseUrl = "https://localhost:44346/api/";

    LoginToken(request: Request) {
        return this.http.post(this.baseUrl + "auth/login", request, {
            headers: this.header, observe: 'response',
        });
    }
}