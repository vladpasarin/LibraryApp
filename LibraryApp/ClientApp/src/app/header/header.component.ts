import { Component, OnInit } from '@angular/core';
import {faBookReader, faHome, faBook, faUser, faBell} from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { SharedDataService } from '../shared/services/shared-data.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userEmail: string;

  constructor(private route: ActivatedRoute, public authService: AuthService, 
    private router: Router, private data: SharedDataService) {
  }

  faLibrary = faBookReader;
  faHome = faHome;
  faBook = faBook;
  faUser = faUser;
  faBell = faBell;
  email: string;
  userId: string;
  message: string;
  success: boolean;

  ngOnInit(): void {
    this.isLoggedIn();
    this.email = sessionStorage.getItem('email');
    this.userId = sessionStorage.getItem('userId');
    this.data.currentMessage.subscribe(message => this.message = message);
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/home"]);
  }

  toProfile() {
    this.router.navigate(["profile", this.userId]);
  }
}
