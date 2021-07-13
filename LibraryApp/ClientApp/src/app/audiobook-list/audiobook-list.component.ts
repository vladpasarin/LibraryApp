import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-audiobook-list',
  templateUrl: './audiobook-list.component.html',
  styleUrls: ['./audiobook-list.component.scss']
})
export class AudiobookListComponent implements OnInit {
  private readonly endpoint = 'book/audiobook';
  audiobooks: any[];

  constructor(private apiService: ApiService,
    private router: Router) { 
  }

  ngOnInit(): void {
    this.loadAudioBooks();
  }

  loadAudioBooks() {
    this.apiService.get<any>(`${this.endpoint}/all`)
      .subscribe(response => {
        this.audiobooks = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }
}
