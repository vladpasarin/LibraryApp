import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Params } from '@angular/router';
import { Rating } from '../models/rating';
import { ApiService } from '../shared/services/api.service';

@Component({
  selector: 'app-rating-modal',
  templateUrl: './rating-modal.component.html',
  styleUrls: ['./rating-modal.component.scss']
})
export class RatingModalComponent implements OnInit {
  private ratingEndpoint = 'rating';
  autoTicks = false;
  max = 5;
  min = 0;
  showTicks = true;
  step = 1;
  thumbLabel = true;
  value = 0;
  tickInterval = 1;
  currentUserId: string;
  assetId: string;
  rating = {} as Rating;
  ratingForm: FormGroup;
  reviewText = new FormControl('');
  createRating: boolean;

  constructor( 
    private route: ActivatedRoute,
    private api: ApiService,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<RatingModalComponent>
  ) { }

  ngOnInit(): void {
    this.currentUserId = sessionStorage.getItem('userId');

    this.ratingForm = this.fb.group({
      review: ['']
    });
  }

  getSliderTickInterval(): number | 'auto' {
    if (this.showTicks) {
      return this.autoTicks ? 'auto' : this.tickInterval;
    }
    return 0;
  }

  getSubmitAction() {
    if (this.createRating == true) {
      this.addRating();
    }
    else {
      this.updateRating();
    }
    this.dialogRef.close();
  }

  addRating() {
    this.rating.score = this.value;
    this.rating.review = this.f.review.value;
    this.rating.assetId = parseInt(this.assetId);
    this.rating.userId = parseInt(this.currentUserId);
    this.api.post<Rating>(`${this.ratingEndpoint}`, this.rating)
      .subscribe(() => {
        this.openSnackBar('Rating successfully added!', null);
    }, err => {
      console.error(err);
      this.openSnackBar('Failed to add rating!', null);
    });
  } 

  updateRating() {
    this.rating.score = this.value;
    this.rating.review = this.f.review.value;
    this.rating.assetId = parseInt(this.assetId);
    this.rating.userId = parseInt(this.currentUserId);
    this.api.put<Rating>(`${this.ratingEndpoint}/${this.rating.id}`, this.rating)
      .subscribe((response: boolean) => {
        if (response = true) {
          this.openSnackBar('Rating successfully updated!', null);
        }
        else {
          this.openSnackBar('Failed to update rating!', null);
        }
    }, err => {
        console.error(err);
        this.openSnackBar('Failed to update rating!', null);
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, { duration: 3000});
  }

  get f() {
    return this.ratingForm.controls;
  }

  get review() {
    return this.ratingForm.get('review');
  }
}
