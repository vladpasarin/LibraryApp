import { Component, OnInit } from '@angular/core';
import {faFacebook, faTwitter, faLinkedinIn, faGithub} from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor() { }

  faFacebook = faFacebook;
  faTwitter = faTwitter;
  faLinkedin = faLinkedinIn;
  faGithub = faGithub;

  ngOnInit(): void {
  }

}
