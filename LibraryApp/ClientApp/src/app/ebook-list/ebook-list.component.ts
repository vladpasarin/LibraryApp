import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-ebook-list',
  templateUrl: './ebook-list.component.html',
  styleUrls: ['./ebook-list.component.scss']
})
export class EbookListComponent implements OnInit {

  private readonly endpoint = 'book/ebook';
  ebooks: any[];

  constructor(private apiService: ApiService,
    private router: Router) { 
  }

  ngOnInit(): void {
    this.loadEBooks();
  }

  loadEBooks() {
    this.apiService.get<any>(`${this.endpoint}/all`)
      .subscribe(response => {
        this.ebooks = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }
}
