import { Component, OnInit, Output } from '@angular/core';
import {faBookReader, faHome, faBook, faUser, faBell} from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';
import { SharedDataService } from '../shared/services/shared-data.service';
import { stringify } from 'querystring';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Notification } from '../models/notification';
import { ApiService } from '../shared/services/api.service';
import { NotificationModalComponent } from '../notification-modal/notification-modal.component';
import { EventEmitter } from 'stream';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  userEmail: string;
  private notifEndpoint = 'auth';

  constructor(private route: ActivatedRoute, 
    public authService: AuthService, 
    private router: Router, 
    private data: SharedDataService,
    private dialog: MatDialog,
    private apiService: ApiService
    ) { }

  faLibrary = faBookReader;
  faHome = faHome;
  faBook = faBook;
  faUser = faUser;
  faBell = faBell;
  email: string;
  userId: string;
  message: string;
  success: boolean;
  selectedBookType: string;
  userNotifications: Notification[];
  noOfNotifs: number;

  ngOnInit(): void {
    this.isLoggedIn();
    this.email = sessionStorage.getItem('email');
    this.userId = sessionStorage.getItem('userId');
    this.data.currentMessage.subscribe(message => this.message = message);
    this.getSelectedBookType();
    this.getNoOfNewNotifs();
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

  getSelectedBookType() {
    let selection = this.selectedBookType = this.router.url.replace("/", "");
    if (selection == 'books' || selection == 'ebooks' || selection == 'audiobooks') {
      this.selectedBookType = selection.charAt(0).toUpperCase() + selection.slice(1);
    } else {
      this.selectedBookType = 'Books';
    }
  }

  getNoOfNewNotifs() {
    if (this.isLoggedIn()) {
      this.apiService.get<number>(`${this.notifEndpoint}/newNotifications/` + this.userId)
        .subscribe((response: number) => {
          this.noOfNotifs = response;
      }, err => {
        console.error(err);
        this.noOfNotifs = 0;
      });
    }
  }

  openNotificationModal() {
    let config = new MatDialogConfig();
    let dialogRef: MatDialogRef<NotificationModalComponent> = this.dialog.open(NotificationModalComponent, config);
    dialogRef.componentInstance.userId = this.userId.toString();
    dialogRef.componentInstance.isLoggedIn = this.isLoggedIn();
  }
}
