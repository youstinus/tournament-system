import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParticipantSeletionDialogComponent } from './participant-seletion-dialog.component';

describe('ParticipantSeletionDialogComponent', () => {
  let component: ParticipantSeletionDialogComponent;
  let fixture: ComponentFixture<ParticipantSeletionDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParticipantSeletionDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParticipantSeletionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
