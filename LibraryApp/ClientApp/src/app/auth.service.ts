import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { AuthRequest } from './shared/authRequest';
import { RequestResponse } from './shared/requestResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = "https://localhost:44346/api/auth/";

  constructor(private http: HttpClient) { }

  logout(): void {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('userId');
    sessionStorage.removeItem('email');
  }

  login(authReq: AuthRequest) {
    return this.http.post(this.baseUrl + 'login', authReq).pipe(
      map((response: RequestResponse) => {
        localStorage.setItem('token', response.token);
        sessionStorage.setItem('userId', response.id);
        sessionStorage.setItem('email', response.email);
      })
    );
  }
}
