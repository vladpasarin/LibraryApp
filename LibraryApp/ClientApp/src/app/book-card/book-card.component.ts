import { Component, ComponentFactoryResolver, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { faBookOpen, faBookmark as faBookmarkSolid } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkRegular } from '@fortawesome/free-regular-svg-icons';
import { Input } from '@angular/core';
import { GenericBook } from '../models/genericBook';
import { Bookmark } from '../models/bookmark';
import { AuthService } from '../shared/services/auth.service';
import { map } from 'rxjs/operators';
import { Status } from '../models/status';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {
  @Input() book: GenericBook;

  private readonly bookmarkEndpoint = 'bookmark';
  private readonly assetEndpoint = 'asset';
  faBookOpen = faBookOpen;
  faBookmarkSolid = faBookmarkSolid;
  faBookmarkRegular = faBookmarkRegular;
  isBookmarked: boolean;
  currentUserId: string;
  bookmarkCheck: Bookmark;
  bookmark = {} as Bookmark;
  deletedBookmarkId: number;
  status = {} as Status;
  //userToken: string;
  cards = [
    {
      title: 'Card title 1',
      description: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.' ,
      buttonText: 'BORROW',
      img: '../../assets/images/book-cover.jpg'
    },
    {
      title: 'Card Title 2',
      description: 'This card has supporting text below as a natural lead-in to additional content.',
      buttonText: 'Button',
      img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg'
    },
    {
      title: 'Card Title 3',
      description: 'This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action. This text is much longer so that you can see a significant difference between the text in  previous tabs.',
      buttonText: 'Button',
      img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg'
    }
  ]
  slides: any = [[]];

  constructor(private apiService: ApiService, public authService: AuthService,
              private router: Router) { 
  }

  ngOnInit(): void {
    //this.userToken = localStorage.getItem('token');
    this.isLoggedIn();
    this.slides = this.chunk(this.cards, 3);
    this.currentUserId = sessionStorage.getItem("userId");
    this.loadBookmarks();
    this.getBookStatus();
    console.log(this.currentUserId);
  }

  chunk(arr: any, chunkSize:any) {
    let R = [];
    for (let i = 0, len = arr.length; i < len; i += chunkSize) {
      R.push(arr.slice(i, i + chunkSize));
    }
    return R;
  }

  loadBookmarks() {
    this.apiService.get<Bookmark>(`${this.bookmarkEndpoint}/` + this.currentUserId + '/' + this.book.assetId)
      .subscribe((response: Bookmark) => {
        this.bookmarkCheck = response;
        console.log(response);
        this.isBookmarked = true;
      }, error => {
        console.log(error);
        this.isBookmarked = false;
    });
  }

  addBookmark() {
    this.bookmark.userId = parseInt(this.currentUserId);
    this.bookmark.assetId = this.book.assetId;
    this.apiService.post<Bookmark>(`${this.bookmarkEndpoint}`, this.bookmark)
      .subscribe(() => {
        console.log("Bookmark added successfully");
        this.loadBookmarks();
    });
  }

  removeBookmark() {
    this.apiService.get<Bookmark>(`${this.bookmarkEndpoint}/` + this.currentUserId +'/' + this.book.assetId)
      .subscribe((response: Bookmark) => {
        this.deletedBookmarkId = response.id;
        console.log(this.deletedBookmarkId);
        this.apiService.delete(`${this.bookmarkEndpoint}/` + this.deletedBookmarkId)
          .subscribe(() => {
            console.log("Bookmark deleted");
            this.loadBookmarks();
          });
      }, error => {
        console.log(error);
    });
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  getBookStatus() {
    this.apiService.get<Status>(`${this.assetEndpoint}/` + 'status/' + this.book.assetId)
      .subscribe((response: Status) => {
        this.status = response;
      }, error => {
        console.log(error);
    });
  }
}
