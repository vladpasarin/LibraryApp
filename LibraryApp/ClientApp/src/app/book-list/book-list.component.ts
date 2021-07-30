import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  private readonly endpoint = 'book';
  books: any[];
  searchValue: string = "";
  faSearch = faSearch;

  constructor(private apiService: ApiService,
    private router: Router) { 
  }

  ngOnInit(): void {
    this.loadBooks();
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
}
