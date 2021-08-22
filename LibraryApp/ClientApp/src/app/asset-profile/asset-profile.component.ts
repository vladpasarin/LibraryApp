import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { GenericBook } from '../models/genericBook';
import { ActivatedRoute, Params } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { AuthService } from '../shared/services/auth.service';
import { Asset } from '../models/asset';
import { faBookOpen, faBookmark as faBookmarkSolid } from '@fortawesome/free-solid-svg-icons';
import { faBookmark as faBookmarkRegular } from '@fortawesome/free-regular-svg-icons';
import { TimeoutError } from 'rxjs';
import { Tag } from '../models/tag';
import {cloneDeep} from 'lodash';

@Component({
  selector: 'app-asset-profile',
  templateUrl: './asset-profile.component.html',
  styleUrls: ['./asset-profile.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AssetProfileComponent implements OnInit {

  private endpoint = 'asset';
  private bookEndpoint = 'book';
  private tagEndpoing = 'assetTag';
  faBookOpen = faBookOpen;
  faBookmarkSolid = faBookmarkSolid;
  faBookmarkRegular = faBookmarkRegular;
  book = {} as GenericBook;
  asset = {} as Asset;
  assetId: string;
  currentUserId: string;
  tags: Tag[] = [];
  constructor(private route: ActivatedRoute, private api: ApiService, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => this.assetId = params['id']);
    this.isLoggedIn();
    this.currentUserId = sessionStorage.getItem("userId");
    this.getAssetById();
    this.getBookByAssetId();
    this.getTagsByAssetId();
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
    this.api.get<Tag[]>(`${this.tagEndpoing}/` + 'asset/' + this.assetId).subscribe((response: Tag[]) => {
      response.map(val => this.tags.push(Object.assign({}, val)));
      console.log(this.tags);
    }, error => {
      console.log(error);
    });
  }

  checkForISBN(): boolean {
    if (this.book.isbn == null){
      return false;
    }
    else return true;
  }

  checkForASIN(): boolean {
    if (this.book.asin == null){
      return false;
    }
    else return true;
  }

  checkSize(): boolean {
    if (this.book.size == 0) {
      return false;
    }
    else return true;
  }

  checkLength(): boolean {
    if (this.book.lengthMinutes == 0) {
      return false;
    }
    else return true;
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}
