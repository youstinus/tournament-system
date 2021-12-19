import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TournamentBracketsComponent } from './tournament-brackets.component';

describe('TestComponent', () => {
  let component: TournamentBracketsComponent;
  let fixture: ComponentFixture<TournamentBracketsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TournamentBracketsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TournamentBracketsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
