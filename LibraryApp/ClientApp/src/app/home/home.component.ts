import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatCarousel, MatCarouselComponent } from '@ngbmodule/material-carousel';
import { MatCarouselSlide, MatCarouselSlideComponent } from '@ngbmodule/material-carousel';
import { GenericBook } from '../models/genericBook';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  private readonly bookEndpoint = 'book';
  private readonly eBookEndpoint = 'book/ebook';
  private readonly audioBookEndpoint = 'book/audiobook';
  books: any[];
  eBooks: any[];
  audioBooks: any[];
  constructor(private apiService: ApiService,
    private router: Router) { 
  }

  ngOnInit(): void {
    this.loadBooks();
    this.loadEBooks();
    this.loadEBooks();
  }

  loadBooks() {
    this.apiService.get<any>(`${this.bookEndpoint}/all`)
      .subscribe(response => {
        this.books = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  loadEBooks() {
    this.apiService.get<any>(`${this.eBookEndpoint}/all`)
      .subscribe(response => {
        this.eBooks = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  loadAudioBooks() {
    this.apiService.get<any>(`${this.audioBookEndpoint}/all`)
      .subscribe(response => {
        this.audioBooks = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }

  slides = [{'image': 'https://i.guim.co.uk/img/media/a7c46bbd5365d7a46cd4ea95fa7b418f5c906ab5/1_0_2418_1451/master/2418.jpg?width=445&quality=45&auto=format&fit=max&dpr=2&s=b2698c618d416aa1999f53275a79f91e'},
            {'image': 'https://compote.slate.com/images/64f54020-be26-4b16-9020-913ceab99a5f.jpeg?width=780&height=520&rect=1560x1040&offset=0x0'},
            {'image': 'https://149349728.v2.pressablecdn.com/wp-content/uploads/2019/05/Untitled-design-27.png'}];
}
