import { Component, OnInit } from '@angular/core';
import {faFacebook, faTwitter, faLinkedinIn, faGithub} from '@fortawesome/free-brands-svg-icons';
import {FormBuilder, FormGroup, Validators, FormControl} from "@angular/forms";
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../shared/api.service';
import { User } from '../shared/user.model';
import { AuthRequest } from "../shared/authRequest";
import { RequestResponse } from "../shared/requestResponse";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public authService: AuthService, public fb: FormBuilder,
      private router: Router, private api: ApiService
  ) { }

  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faLinkedin = faLinkedinIn;
  faGithub = faGithub;

  success: boolean;
  authReq = new AuthRequest();
  requestResponse = new RequestResponse();
  loginForm: FormGroup;

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      password: ['', [
        Validators.required,
        Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
      ]]
    });
  }

  login() {
    this.authReq.email = this.f.email.value;
    this.authReq.password = this.f.password.value;

    this.authService.login(this.authReq).subscribe(next => {
      console.log('Logged in successfully');
    }, error => {
      console.log('Failed to login');
    });
    this.router.navigate(["/home"]);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/home"]);
  }

  get f() {
    return this.loginForm.controls;
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
