import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { Tag } from '../models/tag';
import { GenericBook } from '../models/genericBook';

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
  selectedTag: number = null;
  searchValue: string = "";
  faSearch = faSearch;

  constructor(private apiService: ApiService,
    private router: Router) { 
  }

  ngOnInit(): void {
    this.loadBooks();
    this.loadBookTags();
    this.loadGenericBooks();
    //this.filterBooks();
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
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  loadBookTags() {
    this.apiService.get<Tag>(`${this.tagEndpoint}`)
      .subscribe((response: Tag[]) => {
        this.tags = response;
      }, error => {
        console.log(error);
      });
  }

  selectTag(tagId: number) {
    this.selectedTag = tagId; 
    console.log(this.selectedTag)
  }
/*
  filterBooks() {
    if (this.selectedTag != null) {
      this.filteredBooks = this.genericBooks.filter(book => book.tags.filter(tag => tag.id == this.selectedTag));
      console.log(this.filteredBooks);
    }
    else {
      this.filteredBooks = this.genericBooks;
    }
  }
*/
}
