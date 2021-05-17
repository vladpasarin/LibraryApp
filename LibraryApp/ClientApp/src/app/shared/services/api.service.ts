import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { RequestResponse } from "../requestResponse";
import { AuthRequest } from "../authRequest";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: "root",
})
  
export class ApiService {
    constructor(private http: HttpClient) {}

    header = new HttpHeaders({
        "Content-Type": "application/json",
    });

    baseUrl = "https://localhost:44346/api/";
    private readonly apiUrl = environment.apiEndpointUrl;

    loginToken(request: AuthRequest) {
        return this.http.post(this.baseUrl + "auth/login", request, {
            headers: this.header, observe: 'response',
        });
    }

    get<T>(path: string, params = {}): Observable<any> {
        return this.http.get<T>(`${this.apiUrl}${path}`,
          {
            params
          });
    }
    
    put<T>(path: string, body: object = {}): Observable<any> {
        return this.http.put<T>(
          `${this.apiUrl}${path}`, body
        );
    }
    
    post<T>(path: string, body: object = {}): Observable<any> {
        return this.http.post<T>(
          `${this.apiUrl}${path}`, body
        );
    }
    
    delete<T>(path): Observable<any> {
        return this.http.delete<T>(
            `${this.apiUrl}${path}`
        );
    }
}