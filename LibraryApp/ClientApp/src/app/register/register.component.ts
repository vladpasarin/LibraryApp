import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { faFacebook, faGithub, faLinkedinIn, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { RegisterReq } from '../shared/registerReq';
import { ApiService } from '../shared/services/api.service';
import { AuthService } from '../shared/services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public registrationForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required,
      Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')]
    ],
    firstName: ['', [Validators.required, Validators.maxLength]],
    lastName: ['', [Validators.required, Validators.maxLength]],
    phoneNr: ['', Validators.pattern('[0-9]{3}-[0-9]{2}-[0-9]{3}')]
  });
  
  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faLinkedin = faLinkedinIn;
  faGithub = faGithub;

  date: Date;
  registerRequest = new RegisterReq();

  constructor(public authService: AuthService, public fb: FormBuilder,
    private router: Router, private api: ApiService,
    private _snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.date = new Date();
  }

  openSnackBar() {
    this._snackBar.open('Register succesful!', 'Sign in');
  }

  register() {
    this.registerRequest.email = this.f.email.value;
    this.registerRequest.firstName = this.f.firstName.value;
    this.registerRequest.lastName = this.f.lastName.value;
    this.registerRequest.password = this.f.password.value;
    this.registerRequest.phoneNr = this.f.phoneNr.value;
    this.registerRequest.dateOfBirth = this.date;
    console.log(this.date);

    this.authService.register(this.registerRequest)
    .subscribe(data => {
      console.log('Succesfully registered', data);
      this.router.navigateByUrl('login');
    });
  }

  addEvent(event: MatDatepickerInputEvent<Date>) {
    this.date = event.value;
  }

  get f() {
    return this.registrationForm.controls;
  }

  get email() {
    return this.registrationForm.get('email');
  }

  get password() {
    return this.registrationForm.get('password');
  }

  get firstName() {
    return this.registrationForm.get('firstName');
  }

  get lastName() {
    return this.registrationForm.get('lastName');
  }

  get phoneNr() {
    return this.registrationForm.get('phoneNr');
  }

  get dateOfBirth(){
    return this.registrationForm.get('dateOfBirth');
  }
}
