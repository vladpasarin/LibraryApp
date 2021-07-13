import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudiobookListComponent } from './audiobook-list.component';

describe('AudiobookListComponent', () => {
  let component: AudiobookListComponent;
  let fixture: ComponentFixture<AudiobookListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AudiobookListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AudiobookListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
