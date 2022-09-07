import { Component, OnInit, Output } from '@angular/core';
import { Notification } from '../models/notification';
import { ApiService } from '../shared/services/api.service';
import * as _ from 'lodash';
import { EventEmitter } from 'stream';

@Component({
  selector: 'app-notification-modal',
  templateUrl: './notification-modal.component.html',
  styleUrls: ['./notification-modal.component.scss']
})
export class NotificationModalComponent implements OnInit {
  userId: string;
  isLoggedIn: boolean;
  private notifEndpoint = 'auth';

  constructor(private apiService: ApiService) { }

  userNotifications: Notification[] = [];

  ngOnInit(): void {
    this.getUserNotifications();
  }

  getUserNotifications() {
    if (this.isLoggedIn) {
      this.apiService.get<Notification[]>(`${this.notifEndpoint}/latestNotifications/` + this.userId)
        .subscribe((response: Notification[]) => {
          console.log(_.isEmpty(response));
          if (_.isEmpty(response)) {
            const noNotif: Notification = {
              message: 'No notifications to display!',
              dateCreated: new Date,
              seen: false
            };
            this.userNotifications.push(noNotif);
          } else {
            this.userNotifications = response;
          }
      }, err => {
        console.error(err);
        const errorNotif: Notification = {
          message: 'Failed to get notifications!',
          dateCreated: new Date,
          seen: false
        };
        this.userNotifications.push(errorNotif);
      });
    }
  }
}
