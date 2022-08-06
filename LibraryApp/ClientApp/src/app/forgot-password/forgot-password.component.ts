import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators, FormControl} from "@angular/forms";
import { AuthService } from '../shared/services/auth.service';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { User } from '../shared/user.model';
import { AuthRequest } from "../shared/authRequest";
import { RequestResponse } from "../shared/requestResponse";
import { SharedDataService } from "../shared/services/shared-data.service";
import { MatSnackBar } from '@angular/material/snack-bar';
import { forgotPassword } from '../models/forgotPassword';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  constructor(public authService: AuthService, public fb: FormBuilder,
    private router: Router, private api: ApiService, private data: SharedDataService,
    private _snackBar: MatSnackBar
  ) { }

  success: boolean;
  forgotPasswordReq: forgotPassword;
  requestResponse: RequestResponse;
  forgotPasswordForm: FormGroup;
  message: string;
  forgotPasswordEndpoint = 'auth/forgotPassword';

  ngOnInit(): void {
    this.forgotPasswordReq = {
      email: '',
      clientURI: ''
    };
    this.forgotPasswordForm = this.fb.group({
      email: ['', [
        Validators.required,
        Validators.email
      ]]
    });
  }

  forgotPassword() {
    this.forgotPasswordReq.email = this.f.email.value;

    this.api.post<AuthRequest>(`${this.forgotPasswordEndpoint}`, this.forgotPasswordReq)
      .subscribe((response: Boolean) =>{
        if (response == true) {
          this.logEmailSent();
        } else {
          this.logEmailNotFound();
        }
    });
  }

  redirectToPassReset() {
    this.router.navigate(['/password-reset'])
  }

  logEmailSent() {
    this._snackBar.open('Password reset email has been sent!', 'Sign in', { duration: 3000});
  }

  logEmailNotFound() {
    this._snackBar.open('Email not found!', 'Try again', { duration: 3000});
  }

  get f() {
    return this.forgotPasswordForm.controls;
  }

  get email() {
    return this.forgotPasswordForm.get('email');
  }
}
