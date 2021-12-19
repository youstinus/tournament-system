import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormArray, ValidatorFn } from '@angular/forms';
import { Team } from '../../models/team';
import { TeamService } from '../../services/team.service';
import { NewTournament } from '../../models/new-tournament';
import { TournamentService } from '../../services/tournament.service';
import { GameType } from '../../models/game-type';
import { Router } from '../../../../node_modules/@angular/router';
import { MatchService } from '../../services/match.service';
import { Match } from '../../models/match';
import { SelectionModel } from '../../../../node_modules/@angular/cdk/collections';
import { MatTableDataSource, MatPaginator, MatSort } from '../../../../node_modules/@angular/material';
/**
 * @title Stepper overview
 */

@Component({
  selector: 'new-tournament',
  templateUrl: './new-tournament.component.html',
  styleUrls: ['./new-tournament.component.css']
})
export class NewTournamentComponent implements OnInit {
  displayedColumns: string[] = ['select', 'title'];
  selection = new SelectionModel<Team>(true, []);
  dataSource: MatTableDataSource<Team>;
  teamsToPut: Team[];
  teamForm: FormGroup;

  isLinear = false;
  errorMessage: string;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  typeFormGroup: FormGroup;
  formation: FormGroup;
  form: FormGroup;
  teams: Team[];
  tournament: NewTournament;
  categories = GameType;
  matches: Match[];
  playingTeams: Team[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private _formBuilder: FormBuilder,
    private formBuilder: FormBuilder,
    private teamService: TeamService, private tservice: TournamentService,
    private router: Router, private matchService: MatchService) {
    this.putTeams();
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.form = new FormGroup({
      firstCtrl: new FormControl(),
      secondCtrl: new FormControl()
    });
    this.typeFormGroup = this._formBuilder.group({
      typeFormGroup: new FormControl(),
      typeCtrl: ['']
    });
    this.teamForm = new FormGroup({
      thirdCtrl: new FormControl()
    });

    this.getSub();

  }

  putTeams() {
    this.teamService.getAllTeams().subscribe(teams => {
      this.teamsToPut = teams;
      this.dataSource = new MatTableDataSource(this.teamsToPut);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
    ),
      error => {
        console.error(error);
      };
  }

  getSub() {
    this.teamService.getAllTeams().subscribe(teams => {
      this.teams = teams;
      if (this.teams.length > 0) {
        const controls = this.teams.map(c => new FormControl(false));
        this.form = this.formBuilder.group({
          teams: new FormArray(controls, this.minSelectedCheckboxes(3)), // set to 2 when generator completed
          secondCtrl: ['']
        });
      }
    },
      error => {
        console.log("erroras subscribe getSub()");
        console.error(error);
        this.errorMessage = error.message;
      });
  }
  ngOnInit() {
  }

  onSubmit(titleOfTournament: string, gameType: GameType) {


    const selectedTeamsFrom = this.selection.selected;

    console.log(selectedTeamsFrom);

    /* // A METHOD FOR WORKING TEAM SELECTION MAT-STEP
   const selectedOrderIds = this.form.value.teams
     .map((v, i) => v ? this.teams[i].id : null)
     .filter(v => v !== null);
   console.log(selectedOrderIds);
   this.playingTeams = [];
   for (let teamIds in selectedOrderIds) {
     this.playingTeams.push(this.teams[selectedOrderIds[teamIds] - 1]);
   }*/

    this.tournament = new NewTournament();
    this.tournament.title = titleOfTournament;
    this.tournament.game = gameType;
    this.tournament.maxParticipantPerTeam = 10;
    //this.tournament.numberOfTeams = this.playingTeams.length;
    this.tournament.numberOfTeams = selectedTeamsFrom.length;
    this.tservice
      .addTournament(this.tournament)
      .subscribe(tournament => {
        this.tournament = tournament;
        console.log("Tournament is sent, now creating matches");
        console.log(this.tournament);
        this.matchService.createMatches(this.tournament, selectedTeamsFrom).subscribe(matches => { //this.playingTeams).subscribe(matches => { // FOR WORKING TESM SELECT
          console.log("Created matches and got respond. Going to tournament-brackets page, providing tournament id");
          console.log(matches);
          this.router.navigate(['tournament-brackets', this.tournament.id]);
          return this.matches = matches;
        });
        // this.router.navigate(['tournament-brackets', this.tournament.id]); // didnt wait responce
      },
        error => {
          console.log("Error somewhere sending tournament or creating matches or navigating to brackets page");
          console.error(error + "\n error inside onSubmit method");
        });
  }


  // Method validates to check minimum amount of teams in new-tournament page
  minSelectedCheckboxes(min: number) {
    const validator: ValidatorFn = (formArray: FormArray) => {
      const totalSelected = formArray.controls
        // get a list of checkbox values (boolean)
        .map(control => control.value)
        // total up the number of checked checkboxes
        .reduce((prev, next) => next ? prev + next : prev, 0);

      // if the total is not greater than the minimum, return the error message
      return totalSelected >= min ? null : { required: true };
    };
    return validator;
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }
}