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
import { UserChallenge } from '../models/userChallenge';
import { ProgressBarMode } from '@angular/material/progress-bar';
import { ThemePalette } from '@angular/material/core';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { Quote } from '../models/quote';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GoalType } from '../models/goalType';

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
  private quoteEndpoint = 'quote';
  private goalEndpoint = 'goal';
  mode: ProgressBarMode = 'determinate';
  color: ThemePalette = 'primary';

  constructor(
    private apiService: ApiService,
    private dialog: MatDialog,
    private _snackBar: MatSnackBar
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
  challengeDict = {};
  booksRead: number;
  bookHolds: number;
  currentReads: GenericBook[];
  author = new FormControl('');
  quote = new FormControl('');
  bookQuote = new FormControl('');
  authorList: string[];
  selectedAuthor = null;
  selectedBook: GenericBook;
  bookList = {} as GenericBook[];
  quoteModel = {} as Quote;
  userQuotes: Quote[];
  goalTypes: GoalType[];

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');
    this.getUserProfile();
    this.getBookmarkedBooks();
    this.getUserRatings();
    //this.getChallenges();
    this.getNumberOfBooksRead();
    this.getNumberOfBookHolds();
    this.getCurrentRead();
    this.getUserQuotes();
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

  /*getChallenges() {
    this.apiService.get<Challenge>(`${this.challengeEndpoint}`)
      .subscribe((response: Challenge[]) => {
        this.challenges = response;
      }, err => {
        console.error(err);
    });
  }*/

  getNumberOfBooksRead() {
    this.apiService.get<number>(`${this.userEndpoint}/history/${this.currentUserId}`)
      .subscribe((response: number) => {
        this.booksRead = response;
      });
  }

  getNumberOfBookHolds() {
    this.apiService.get<number>(`${this.userEndpoint}/holds/${this.currentUserId}`)
      .subscribe((response: number) => {
        this.bookHolds = response;
      });
  }

  getCurrentRead() {
    this.apiService.get<any>(`${this.userEndpoint}/currentRead/${this.currentUserId}`)
      .subscribe((response: GenericBook[]) => {
        this.currentReads = response;
      });
  }

  searchAuthors() {
    let value = this.author.value;
    console.log(value);
    this.apiService.get<any>(`${this.bookEndpoint}/searchAuthor/${value.toLowerCase()}`)
      .subscribe((response: string[]) => {
        console.log(response);
        this.authorList = response;
    });
  }

  searchBooksByAuthor() {
    console.log(this.selectedAuthor);
    this.apiService.get<any>(`${this.bookEndpoint}/searchBookByAuthor/${this.author.value.toLowerCase()}`)
      .subscribe((response: GenericBook[]) => {
        this.bookList = response;
        console.log(this.bookList);
      });
  }

  getSelectedAuthor() {
    this.selectedAuthor = this.author.value;
    console.log(this.selectedAuthor);
  }

  addQuote() {
    this.quoteModel.content = this.quote.value;
    let books = this.bookList.filter((book) => 
      book.title.includes(this.bookQuote.value)
      && book.author.includes(this.author.value)
    );
    console.log(books);
    this.quoteModel.bookId = books[0]?.id;
    this.quoteModel.userId = parseInt(this.currentUserId);
    this.apiService.post<any>(`${this.quoteEndpoint}`, this.quoteModel)
      .subscribe((response: boolean) => {
        if (response == true) {
          this.logQuoteAdded();
          this.getUserQuotes();
        }
        else {
          this.logQuoteAddingFailure();
        }
      })
  }

  logQuoteAdded() {
    this._snackBar.open('Quote successfully added!', '', { duration: 3000});
  }

  logQuoteAddingFailure() {
    this._snackBar.open('Failed to add quote!', '', { duration: 3000});
  }

  getUserQuotes() {
    this.apiService.get<Quote[]>(`${this.quoteEndpoint}/userQuotes/${this.currentUserId}`)
      .subscribe((response: Quote[]) => {
        this.userQuotes = response;
      });
  }

  getGoalTypes() {
    this.apiService.get<GoalType[]>(`${this.goalEndpoint}/types`)
      .subscribe((response: GoalType[]) => {
        this.goalTypes = response;
      });
  }
}
