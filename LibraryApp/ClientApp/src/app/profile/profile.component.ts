import { Component, OnInit } from '@angular/core';
import { faUserAlt } from '@fortawesome/free-solid-svg-icons';
import { ApiService } from '../shared/services/api.service';
import { User } from '../shared/user.model'; 

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  faUser = faUserAlt;
  currentUserId: string;
  userEndpoint = 'auth';
  currentUser: User;

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');
    this.getUserProfile();
  }

  getUserProfile() {
    this.apiService.get<any>(`${this.userEndpoint}/${this.currentUserId}`)
      .subscribe((response: User) => {
        this.currentUser = response;
      }, err => {
        console.error(err);
      });
  }
}
