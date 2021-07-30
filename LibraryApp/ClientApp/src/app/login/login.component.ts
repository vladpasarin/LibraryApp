import { Component, OnInit } from '@angular/core';
import {faFacebook, faTwitter, faLinkedinIn, faGithub} from '@fortawesome/free-brands-svg-icons';
import {FormBuilder, FormGroup, Validators, FormControl} from "@angular/forms";
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { User } from '../shared/user.model';
import { AuthRequest } from "../shared/authRequest";
import { RequestResponse } from "../shared/requestResponse";
import { SharedDataService } from "../shared/services/shared-data.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public authService: AuthService, public fb: FormBuilder,
      private router: Router, private api: ApiService, private data: SharedDataService
  ) { }

  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faLinkedin = faLinkedinIn;
  faGithub = faGithub;

  success: boolean;
  authReq: AuthRequest;
  requestResponse: RequestResponse;
  loginForm: FormGroup;
  message: string;

  ngOnInit(): void {
    this.authReq = {
      email: '',
      password: ''
    };
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

    this.data.currentMessage.subscribe(message => this.message = message)
  }

  newMessage() {
    this.data.changeMessage(this.authReq.email);
  }

  login() {
    this.authReq.email = this.f.email.value;
    this.authReq.password = this.f.password.value;
    this.newMessage();

    this.authService.login(this.authReq).subscribe(next => {
      console.log('Logged in successfully');
    }, error => {
      console.log('Failed to login');
    });
    setTimeout(() => {
    }, 3000);
    this.router.navigate(["/home"]);
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
