import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { faFacebook, faGithub, faLinkedinIn, faTwitter } from '@fortawesome/free-brands-svg-icons';
import { ApiService } from '../shared/services/api.service';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public registrationForm = this.fb.group({
    email: ['', Validators.email],
    password: ['', Validators.required,
      Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
    ]
  });
  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faLinkedin = faLinkedinIn;
  faGithub = faGithub;

  constructor(public authService: AuthService, public fb: FormBuilder,
    private router: Router, private api: ApiService) { }

  ngOnInit(): void {
  }

  register() {
    
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

}
