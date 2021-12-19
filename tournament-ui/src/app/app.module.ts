import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NewTournamentComponent } from './tournament/new-tournament/new-tournament.component';
//import { Route } from '@angular/compiler/src/core';
import { MainComponent } from './main/main.component';
import { TournamentInfoComponent } from './tournament/tournament-info/tournament-info.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TournamentBracketsComponent } from './tournament/tournament-table/tournament-brackets.component';
import { HttpClientModule } from '@angular/common/http';
import { NewTeamComponent } from './team/new-team/new-team.component';
import { ParticipantTableComponent } from './participant/participant-table/participant-table.component';
import { MatGridListModule, MatStepperModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, MatNativeDateModule, MatPaginatorModule } from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '../../node_modules/@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthorsComponent } from './authors/authors.component';
import { TeamTableComponent } from './team/team-table/team-table.component';
import {MatTableModule} from '@angular/material/table';
import { ParticipantSeletionDialogComponent } from './dialogs/participant-seletion-dialog/participant-seletion-dialog.component';
import { ParticipantSelectionComponent } from './participant/participant-selection/participant-selection.component';

const appRoutes: Routes = [
  { path: 'new-tournament', component: NewTournamentComponent },
  { path: 'new-team', component: NewTeamComponent },
  { path: 'team-table', component: TeamTableComponent },
  { path: 'participant-table', component: ParticipantTableComponent },
  { path: 'app-authors', component: AuthorsComponent },
  { path: 'app-main', component: MainComponent },
  { path: '', redirectTo: 'app-main', pathMatch: 'full' },
  { path: 'tournament-brackets/:id', component: TournamentBracketsComponent },
  { path: '**', component: PageNotFoundComponent }

];
@NgModule({
  declarations: [
    AppComponent,
    NewTournamentComponent,
    AuthorsComponent,
    MainComponent,
    PageNotFoundComponent,
    TournamentInfoComponent,
    TournamentBracketsComponent,
    NewTeamComponent,
    ParticipantTableComponent,
    TeamTableComponent,
    ParticipantSeletionDialogComponent,
    ParticipantSelectionComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    HttpClientModule,
    MatGridListModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatCheckboxModule,
    FormsModule,
    HttpClientModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
