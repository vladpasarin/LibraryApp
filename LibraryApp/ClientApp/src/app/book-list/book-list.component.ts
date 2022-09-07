import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { faSearch, faBan } from '@fortawesome/free-solid-svg-icons';
import { Tag } from '../models/tag';
import { GenericBook } from '../models/genericBook';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {

  private readonly endpoint = 'book';
  private readonly genericBookEndpoint = 'book/generic';
  private readonly tagEndpoint = 'tag';
  books: any[];
  genericBooks: GenericBook[];
  filteredBooks: GenericBook[];
  tags: Tag[];
  selectedTag: Tag = null;
  searchValue: string = "";
  faSearch = faSearch;
  faBan = faBan;
  listTag: string = "";

  constructor(private apiService: ApiService,
    private router: Router,
    private route: ActivatedRoute) { 
  }

  ngOnInit(): void {
    this.selectedTag = null;
    this.loadBooks();
    this.loadBookTags();
    this.loadGenericBooks();
    this.filterBooks();
  }

  loadBooks() {
    this.apiService.get<any>(`${this.endpoint}/all`)
      .subscribe(response => {
        this.books = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  loadGenericBooks() {
    this.apiService.get<any>(`${this.genericBookEndpoint}`)
      .subscribe(response => {
        this.genericBooks = response;
        this.filteredBooks = this.genericBooks;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  loadBookTags() {
    this.apiService.get<Tag>(`${this.tagEndpoint}`)
      .subscribe((response: Tag[]) => {
        this.tags = response;
        //this.checkListTag();
      }, error => {
        console.log(error);
      });
  }

  checkListTag() {
    this.listTag = this.route.snapshot.paramMap.get('tag');
    console.log("List tag: " + this.listTag);
    if (this.listTag != null) {
      this.selectTagByName(this.listTag);
    }
  }

  selectTagByName(tagName: string) {
    this.tags.forEach(tag => {
      if (tag.name == tagName) {
        this.selectTag(tag);
      }
    })
  }

  selectTag(tag: Tag) {
    this.selectedTag = tag;
    this.filterBooks();
    console.log(this.selectedTag)
  }

  resetSelectedTags() {
    this.selectTag(null);
  }


  filterBooks() {
    if (this.selectedTag != null) {
      this.filteredBooks = this.genericBooks.filter(book => book.tags.some(tag => tag.id == this.selectedTag.id));
      console.log(this.filteredBooks);
    }
    else {
      this.filteredBooks = this.genericBooks;
    }
  }

}
