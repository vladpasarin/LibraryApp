import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { AuthRequest } from '../authRequest';
import { RequestResponse } from '../requestResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = "https://localhost:44346/api/auth/";

  constructor(private http: HttpClient) { }

  logout(): void {
    localStorage.removeItem('token');
    sessionStorage.removeItem('userId');
    sessionStorage.removeItem('email');
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    console.log('Auth Token: ', token);
    return !!token;
  }

  login(authReq: AuthRequest) {
    return this.http.post(this.baseUrl + 'login', authReq).pipe(
      map((response: RequestResponse) => {
        localStorage.setItem('token', response.token);        
        sessionStorage.setItem('email', response.email);
        sessionStorage.setItem('userId', response.id);
      })
    );
  }
}
