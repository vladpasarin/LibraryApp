import { Component, OnInit } from '@angular/core';
import {faBookReader, faHome, faBook, faUser} from '@fortawesome/free-solid-svg-icons'
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  faLibrary = faBookReader;
  faHome = faHome;
  faBook = faBook;
  faUser = faUser;

  ngOnInit(): void {
  }

}
