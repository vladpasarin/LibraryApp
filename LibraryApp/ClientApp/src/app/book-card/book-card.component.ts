import { Component, ComponentFactoryResolver, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import {
  faBookOpen,
  faBookmark as faBookmarkSolid,
} from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkRegular } from '@fortawesome/free-regular-svg-icons';
import { Input } from '@angular/core';
import { GenericBook } from '../models/genericBook';
import { Bookmark } from '../models/bookmark';
import { AuthService } from '../shared/services/auth.service';
import { map } from 'rxjs/operators';
import { Status } from '../models/status';
import { User } from '../shared/user.model';
import { Tag } from '../models/tag';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss'],
})
export class BookCardComponent implements OnInit {
  @Input() book: GenericBook;
  @Input() selectedTag: Tag;

  private readonly bookmarkEndpoint = 'bookmark';
  private readonly assetEndpoint = 'asset';
  private readonly checkoutEndpoint = 'checkout';
  private readonly authEndpoint = 'auth';
  private readonly tagEndpoint = 'assettag';
  faBookOpen = faBookOpen;
  faBookmarkSolid = faBookmarkSolid;
  faBookmarkRegular = faBookmarkRegular;
  isBookmarked: boolean;
  isBorrowed: boolean;
  currentUserId: string;
  currentUser: User;
  bookmarkCheck: Bookmark;
  bookmark = {} as Bookmark;
  deletedBookmarkId: number;
  status = {} as Status;
  tags: Tag[];
  bookTags: String[] = [];
  //userToken: string;
  cards = [
    {
      title: 'Card title 1',
      description:
        'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
      buttonText: 'BORROW',
      img: '../../assets/images/book-cover.jpg',
    },
    {
      title: 'Card Title 2',
      description:
        'This card has supporting text below as a natural lead-in to additional content.',
      buttonText: 'Button',
      img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg',
    },
    {
      title: 'Card Title 3',
      description:
        'This is a wider card with supporting text below as a natural lead-in to additional content. This card has even longer content than the first to show that equal height action. This text is much longer so that you can see a significant difference between the text in  previous tabs.',
      buttonText: 'Button',
      img: 'https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20(34).jpg',
    },
  ];
  slides: any = [[]];

  constructor(
    private apiService: ApiService,
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    //this.userToken = localStorage.getItem('token');
    this.slides = this.chunk(this.cards, 3);
    this.currentUserId = sessionStorage.getItem('userId');
    if (this.isLoggedIn()) {
      this.loadBookmarks();
      this.loadBorrowedBooks();
    }
    this.getBookStatus();
    this.getBookTags();
  }

  chunk(arr: any, chunkSize: any) {
    let R = [];
    for (let i = 0, len = arr.length; i < len; i += chunkSize) {
      R.push(arr.slice(i, i + chunkSize));
    }
    return R;
  }

  loadBookmarks() {
    this.apiService
      .get<Bookmark>(
        `${this.bookmarkEndpoint}/` +
          this.currentUserId +
          '/' +
          this.book.assetId
      )
      .subscribe(
        (response: Bookmark) => {
          this.bookmarkCheck = response;
          console.log(response);
          this.isBookmarked = true;
        },
        (error) => {
          console.log(error);
          this.isBookmarked = false;
        }
      );
  }

  loadBorrowedBooks() {
    this.apiService
      .get<User>(`${this.authEndpoint}/` + this.currentUserId)
      .subscribe(
        (response: User) => {
          this.currentUser = response;
          console.log(response);
          this.apiService
            .get(
              `${this.checkoutEndpoint}/` +
                this.book.assetId +
                '/' +
                this.currentUser.libraryCardId
            )
            .subscribe(
              (response: boolean) => {
                console.log(response);
                this.isBorrowed = response;
              },
              (error) => {
                console.log(error);
                this.isBorrowed = false;
              }
            );
        },
        (error) => {
          console.log(error);
        }
      );
  }

  addBookmark() {
    this.bookmark.userId = parseInt(this.currentUserId);
    this.bookmark.assetId = this.book.assetId;
    this.apiService
      .post<Bookmark>(`${this.bookmarkEndpoint}`, this.bookmark)
      .subscribe(() => {
        console.log('Bookmark added successfully');
        this.loadBookmarks();
      });
  }

  removeBookmark() {
    this.apiService
      .get<Bookmark>(
        `${this.bookmarkEndpoint}/` +
          this.currentUserId +
          '/' +
          this.book.assetId
      )
      .subscribe(
        (response: Bookmark) => {
          this.deletedBookmarkId = response.id;
          console.log(this.deletedBookmarkId);
          this.apiService
            .delete(`${this.bookmarkEndpoint}/` + this.deletedBookmarkId)
            .subscribe(() => {
              console.log('Bookmark deleted');
              this.loadBookmarks();
            });
        },
        (error) => {
          console.log(error);
        }
      );
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  getBookStatus() {
    this.apiService
      .get<Status>(`${this.assetEndpoint}/` + 'status/' + this.book.assetId)
      .subscribe(
        (response: Status) => {
          this.status = response;
        },
        (error) => {
          console.log(error);
        }
      );
  }

  getBookTags() {
    this.apiService
      .get<Tag[]>(`${this.tagEndpoint}/` + 'asset/' + this.book.assetId)
      .subscribe((response: Tag[]) => {
        this.tags = response;
        if (response.length != 0) {
          response.forEach(el => {
            this.bookTags.push(el.name);
          });
          console.log(this.bookTags);
        }
      }, error => {
        console.log(error);
      });
  }

  showBookCard() {
    if (this.selectedTag != null) {
      return this.bookTags.includes(this.selectedTag.name);
    }
    return true;
  }

  toBookProfile(assetId: number) {
    this.router.navigate(['asset', assetId]);
  }
}
