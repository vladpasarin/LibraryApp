import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthRequest } from '../authRequest';
import { RegisterReq } from '../registerReq';
import { RequestResponse } from '../requestResponse';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = "https://localhost:44346/api/auth/";
  private readonly endpoint = 'auth';

  constructor(private http: HttpClient,
              private apiService: ApiService  
  ) { }

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

  register(userData: RegisterReq): Observable<any> {
    return this.apiService.post<any>(`${this.endpoint}/register`, userData).pipe(
      map((response: any) => {
        if (response.success) {
          localStorage.setItem('userProfile', response.userProfile);
        }
      })
    );
  }
}
