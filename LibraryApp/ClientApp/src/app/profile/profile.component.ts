import { Component, OnInit } from '@angular/core';
import { faUserAlt } from '@fortawesome/free-solid-svg-icons';
import { Book } from '../models/book';
import { GenericBook } from '../models/genericBook';
import { Rating } from '../models/rating';
import { ApiService } from '../shared/services/api.service';
import { User } from '../shared/user.model'; 
import { faPen, faQuoteLeft, faQuoteRight } from '@fortawesome/free-solid-svg-icons';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { RatingModalComponent } from '../rating-modal/rating-modal.component';
import { Challenge } from '../models/challenge';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  private userEndpoint = 'auth';
  private bookmarkEndpoint = 'bookmark';
  private ratingEndpoint = 'rating';
  private bookEndpoint = 'book';
  private challengeEndpoint = 'challenge';

  constructor(
    private apiService: ApiService,
    private dialog: MatDialog
  ) { }

  faUser = faUserAlt;
  faPen = faPen;
  faQuoteLeft = faQuoteLeft;
  faQuoteRight = faQuoteRight;
  currentUserId: string;
  currentUser: User;
  bookmarkedBooks: Book[];
  userRatings: Rating[];
  challenges: Challenge[];

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');
    this.getUserProfile();
    this.getBookmarkedBooks();
    this.getUserRatings();
    this.getChallenges();
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
    console.log("getting bookmarked books...")
    this.apiService.get<any>(`${this.bookmarkEndpoint}/books/${this.currentUserId}`)
      .subscribe(response => {
        this.bookmarkedBooks = response;
        console.log(this.bookmarkedBooks);
      }, err => {
        console.error(err);
    });
  }

  getUserRatings() {
    this.apiService.get<Rating>(`${this.ratingEndpoint}/user/${this.currentUserId}`)
      .subscribe((response: Rating[]) => {
        this.userRatings = response;
        console.log(this.userRatings);
        this.getRatedBooks();
      }, err => {
        console.error(err);
    });
  }

  getRatedBooks() {
    this.userRatings.forEach(rating => {
      this.apiService.get<Book>(`${this.bookEndpoint}/asset/${rating.assetId}`)
        .subscribe((response: Book) => {
          rating.ratedBook = response;
        }, err => {
          console.error(err);
        });
    });
  }

  openRatingModal(assetId: number, ratingId: number) {
    console.log("sent assetId: " + assetId);
    let config = new MatDialogConfig();
    let dialogRef: MatDialogRef<RatingModalComponent> = this.dialog.open(RatingModalComponent, config);
    dialogRef.componentInstance.assetId = assetId.toString();
    dialogRef.componentInstance.rating.id = ratingId;
    dialogRef.componentInstance.createRating = false;
    dialogRef.afterClosed().subscribe(() => {
      this.getUserRatings();
    });
  }

  reloadBookmarks(value: boolean) {
    console.log("Parent isBookmarked value: " + value)
    if (value == true) {
      this.getBookmarkedBooks();
    }
  }

  getChallenges() {
    this.apiService.get<Challenge>(`${this.challengeEndpoint}`)
      .subscribe((response: Challenge[]) => {
        this.challenges = response;
      }, err => {
        console.error(err);
    });
  }

  startChallenge(challengeId: number) {

  }
}
