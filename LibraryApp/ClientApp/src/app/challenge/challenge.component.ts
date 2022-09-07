import { Component, Input, OnInit } from '@angular/core';
import { Challenge } from '../models/challenge';
import { UserChallenge } from '../models/userChallenge';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-challenge',
  templateUrl: './challenge.component.html',
  styleUrls: ['./challenge.component.scss']
})
export class ChallengeComponent implements OnInit {
  @Input() challenge: Challenge;

  private userChallengeEndpoint = 'userChallenge';
  private challengeEndpoint = 'challenge';
  currentUserId: string;
  userChallenge: UserChallenge;
  value = 50;

  constructor(private api: ApiService,) { }

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');
    this.getUserChallenge(this.challenge.id);
  }

  startChallenge(challengeId: number) {
    this.api.post(`${this.challengeEndpoint}/start/${challengeId}/${this.currentUserId}`)
      .subscribe(() => {
        this.getUserChallenge(challengeId);
      }, err =>{
        console.error(err);
      });
  }

  getUserChallenge(challengeId: number) {
    this.api.get<UserChallenge>(`${this.userChallengeEndpoint}/challenge=${challengeId}/user=${this.currentUserId}`)
      .subscribe((response: UserChallenge) => {
        this.userChallenge = response;
        this.updateChallengeProgress;
        console.log(this.userChallenge);
      }, err => {
        console.error(err);
      });
  }

  updateChallengeProgress() {
    this.api.put(`${this.userChallengeEndpoint}/getProgress/${this.userChallenge.id}/${this.currentUserId}`)
      .subscribe(() => {

      });
  }
}
