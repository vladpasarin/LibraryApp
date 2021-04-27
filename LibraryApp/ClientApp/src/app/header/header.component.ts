import { Component, OnInit } from '@angular/core';
import {faBookReader, faHome, faBook, faUser} from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(public authService: AuthService, private router: Router) { }

  faLibrary = faBookReader;
  faHome = faHome;
  faBook = faBook;
  faUser = faUser;
  email: string;
  userId: string;

  ngOnInit(): void {
    this.loggedIn();
    this.email = sessionStorage.getItem('email');
    this.userId = sessionStorage.getItem('userId');
  }

  logout() {
    this.authService.logout();
    localStorage.removeItem('token');
    this.router.navigate(["/home"]);
  } 

  loggedIn() {
    const token = localStorage.getItem('token');
    console.log(token);
    console.log(this.email, this.userId);
    return !!token;
  }

  toProfile() {
    this.router.navigate(["profile", this.userId]);
  }
}
