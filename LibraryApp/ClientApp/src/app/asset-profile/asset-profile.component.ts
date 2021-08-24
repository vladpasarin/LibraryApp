import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { GenericBook } from '../models/genericBook';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { AuthService } from '../shared/services/auth.service';
import { Asset } from '../models/asset';
import { faBookOpen, faBookmark as faBookmarkSolid } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkRegular } from '@fortawesome/free-regular-svg-icons';
import { TimeoutError } from 'rxjs';
import { Tag } from '../models/tag';
import { cloneDeep } from 'lodash';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Bookmark } from '../models/bookmark';

@Component({
  selector: 'app-asset-profile',
  templateUrl: './asset-profile.component.html',
  styleUrls: ['./asset-profile.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AssetProfileComponent implements OnInit {

  private endpoint = 'asset';
  private bookEndpoint = 'book';
  private tagEndpoint = 'assetTag';
  private bookmarkEndpoint = 'bookmark';
  faBookOpen = faBookOpen;
  faBookmarkSolid = faBookmarkSolid;
  faBookmarkRegular = faBookmarkRegular;
  book = {} as GenericBook;
  asset = {} as Asset;
  assetId: string;
  currentUserId: string;
  tags: Tag[] = [];
  bookmark = {} as Bookmark;
  isBookmarked: boolean;
  deletedBookmarkId: number;
  constructor(private route: ActivatedRoute, private api: ApiService, private authService: AuthService,
    private _snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.assetId = params['id']);
    this.isLoggedIn();
    this.currentUserId = sessionStorage.getItem("userId");
    this.getAssetById();
    this.getBookByAssetId();
    this.getTagsByAssetId();
    this.loadBookmarks();
  }

  getAssetById() {
    this.api.get<Asset>(`${this.endpoint}/` + this.assetId).subscribe((response: Asset) => {
      this.asset = response;
      console.log(this.asset.type);
    }, error => {
      console.log(error);
    });
  }

  getBookByAssetId() {
    this.api.get<GenericBook>(`${this.bookEndpoint}/` + 'asset/' + this.assetId).subscribe((response: GenericBook) => {
      this.book = response;
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  getTagsByAssetId() {
    this.api.get<Tag[]>(`${this.tagEndpoint}/` + 'asset/' + this.assetId).subscribe((response: Tag[]) => {
      console.log(response);
      this.tags = cloneDeep(response);
      console.log(this.tags);
    }, error => {
      console.log(error);
    });
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  openSnackBar() {
    if (this.currentUserId == null) {
      this._snackBar.open('Account required!', 'Sign in', { duration: 3000});
    }
  }

  loadBookmarks() {
    if (this.currentUserId == null) {
      return false;
    }
    this.api.get<Bookmark>(`${this.bookmarkEndpoint}/` + this.currentUserId + '/' + this.assetId)
      .subscribe((response: Bookmark) => {
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
    this.api.post<Bookmark>(`${this.bookmarkEndpoint}`, this.bookmark)
      .subscribe(() => {
        console.log("Bookmark added successfully");
        this.loadBookmarks();
    });
  }

  removeBookmark() {
    this.api.get<Bookmark>(`${this.bookmarkEndpoint}/` + this.currentUserId +'/' + this.book.assetId)
      .subscribe((response: Bookmark) => {
        this.deletedBookmarkId = response.id;
        console.log(this.deletedBookmarkId);
        this.api.delete(`${this.bookmarkEndpoint}/` + this.deletedBookmarkId)
          .subscribe(() => {
            console.log("Bookmark deleted");
            this.loadBookmarks();
          });
      }, error => {
        console.log(error);
    });
  }

  toLogin() {
    this.router.navigate(["login"]);
  }
}