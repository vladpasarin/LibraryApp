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

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.scss']
})
export class PasswordResetComponent implements OnInit {

  constructor(public authService: AuthService, public fb: FormBuilder,
    private router: Router, private api: ApiService, private data: SharedDataService,
    private _snackBar: MatSnackBar
  ) { }

  success: boolean;
  resetReq: AuthRequest;
  requestResponse: RequestResponse;
  passwordResetForm: FormGroup;
  message: string;
  paswordResetEndpoint = 'auth/resetPassword';

  ngOnInit(): void {
    this.resetReq = {
      email: '',
      password: ''
    };
    this.passwordResetForm = this.fb.group({
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

  resetPassword() {
    this.resetReq.email = this.f.email.value;
    this.resetReq.password = this.f.password.value;

    this.api.put<AuthRequest>(`${this.paswordResetEndpoint}`, this.resetReq)
      .subscribe((response: Boolean) =>{
        if (response == true) {
          this.logPasswordChanged();
        } else {
          this.logPasswordResetFailure();
        }
    });
  }

  logPasswordChanged() {
    this._snackBar.open('Password successfully changed!', 'Sign in', { duration: 3000});
  }

  logPasswordResetFailure() {
    this._snackBar.open('Password reset failed!', 'Try again', { duration: 3000});
  }

  get f() {
    return this.passwordResetForm.controls;
  }

  get email() {
    return this.passwordResetForm.get('email');
  }

  get password() {
    return this.passwordResetForm.get('password');
  }
}
