import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';
import { faBookOpen, faBookmark } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent implements OnInit {
  private readonly endpoint = 'book';
  books: any[];
  faBookOpen = faBookOpen;
  faBookmark = faBookmark;
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

  constructor(private apiService: ApiService,
              private router: Router) { 
  }

  ngOnInit(): void {
    //this.userToken = localStorage.getItem('token');
    this.loadBooks();
    this.slides = this.chunk(this.cards, 3);
  }

  loadBooks() {
    this.apiService.get<any>(`${this.endpoint}/all`)
      .subscribe(response => {
        this.books = response;
      }, error => {
        console.log(error);
      });
  }

  chunk(arr: any, chunkSize:any) {
    let R = [];
    for (let i = 0, len = arr.length; i < len; i += chunkSize) {
      R.push(arr.slice(i, i + chunkSize));
    }
    return R;
  }
}
