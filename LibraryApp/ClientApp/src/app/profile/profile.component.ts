import { Component, OnInit } from '@angular/core';
import { faUserAlt } from '@fortawesome/free-solid-svg-icons';
import { GenericBook } from '../models/genericBook';
import { ApiService } from '../shared/services/api.service';
import { User } from '../shared/user.model'; 

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  userEndpoint = 'auth';
  bookmarkEndpoint = 'bookmark';

  constructor(private apiService: ApiService) { }

  faUser = faUserAlt;
  currentUserId: string;
  currentUser: User;
  bookmarkedBooks: GenericBook;

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');
    this.getUserProfile();
    this.getBookmarkedBooks();
  }

  getUserProfile() {
    this.apiService.get<any>(`${this.userEndpoint}/${this.currentUserId}`)
      .subscribe((response: User) => {
        this.currentUser = response;
      }, err => {
        console.error(err);
      });
  }

  getBookmarkedBooks() {
    this.apiService.get<any>(`${this.bookmarkEndpoint}/assets/${this.currentUserId}`)
      .subscribe((response: GenericBook) => {
        this.bookmarkedBooks = response;
      }, err => {
        console.error(err);
      });
  }
}
