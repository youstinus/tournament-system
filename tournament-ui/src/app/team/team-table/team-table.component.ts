import { Component, OnInit, Inject } from '@angular/core';
import { Team } from '../../models/team';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { element } from '../../../../node_modules/@angular/core/src/render3/instructions';
import { ElementData } from '../../../../node_modules/@angular/core/src/view';
import { EmptyError } from '../../../../node_modules/rxjs';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatTableDataSource, MatTableModule} from '@angular/material';
import { TeamService } from '../../services/team.service';
import { last } from '../../../../node_modules/@angular/router/src/utils/collection';
import { ParticipantSeletionDialogComponent } from '../../dialogs/participant-seletion-dialog/participant-seletion-dialog.component';
import { ParticipantService } from '../../services/participant.service';
import { Participant } from '../../models/participant';
import {SelectionModel} from '@angular/cdk/collections';

export interface TeamElement {
  position: number,
  id: number,
  teamTitle: string
}

export interface ParticipantElement {
  position: number;
  id:number;
  firstName: string;  
  lastName: string;
  age: number;
}

const TEAM_DATA: TeamElement[] = [
  {
    position: 1, id: null, teamTitle:'Add new team'
  }];

const PARTICIPANT_DATA: ParticipantElement[] = [
  {position: 1, id: 1, firstName: '', lastName: '1', age: 1},
];

@Component({
  selector: 'app-team-table',
  styleUrls: ['./team-table.component.css'],
  templateUrl: './team-table.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class TeamTableComponent implements OnInit {
  constructor(private teamService: TeamService, private participantService: ParticipantService) {
    teamService.getAllTeams().subscribe(a=> {
      this.InstantiateTeamTable(a);
    },
    error => {
      console.log("negavome teamu");
      console.error(error);
    });

    participantService.getParticipants().subscribe(a=> {
      this.InstantiateParticipantTable(a);
    },
    error => {
      console.log("negavome matchu");
      console.error(error);
    });
  }

  teamDataSource = new MatTableDataSource(TEAM_DATA);
  teamColumnsToDisplay = ['id', 'teamTitle'];
  participantDataSource = new MatTableDataSource(PARTICIPANT_DATA);
  participantColumnsToDisplay = ['select', 'id', 'firstName', 'lastName', 'age'];
  selection = new SelectionModel<ParticipantElement>(true, []);

  title = ""; 
  newTeam = new Team();

  ngOnInit() {
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.participantDataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.participantDataSource.data.forEach(row => this.selection.select(row));
  }

  onRemoveButtonClick(element: TeamElement){
    this.teamService.removeTeam(element.id).subscribe(t =>{
      TEAM_DATA.splice(element.position-1, 1);
      this.UpdateTeamTable();
    })
  }

  onAddButtonClick(name: string) {
    if(name.length > 0 && this.selection.selected.length > 0)
    {
      this.newTeam = new Team();
      this.newTeam.title = name;

      this.teamService.createTeam(this.newTeam).subscribe(team => {
        this.newTeam = team;
        this.AddTeam(this.newTeam);
      });   
    }
  }   

  AddTeam(team: Team){
    TEAM_DATA.splice(TEAM_DATA.length-1, 1);
    TEAM_DATA.push({
      position: TEAM_DATA.length +1,
      id: team.id,
      teamTitle: team.title
    });

    TEAM_DATA.push({
      position: TEAM_DATA.length +1,
      id:null,
      teamTitle: "Add new team"
    });
    this.teamDataSource = new MatTableDataSource(TEAM_DATA);
  }

  AddParticipant(participant: Participant){
    PARTICIPANT_DATA.push({
      position: PARTICIPANT_DATA.length+1,
      id: participant.id,
      firstName: participant.firstName,
      lastName: participant.lastName, 
      age: participant.age
    });
    this.participantDataSource = new MatTableDataSource(PARTICIPANT_DATA);
  }
  
  InstantiateTeamTable(teams: Team[]){
    TEAM_DATA.splice(0, TEAM_DATA.length);
    for (let team of teams) {
      this.AddTeam(team);
    }
  }

  InstantiateParticipantTable(participants: Participant[]){
    PARTICIPANT_DATA.splice(0, PARTICIPANT_DATA.length);
    for (let participant of participants) {
      this.AddParticipant(participant);
    }
  }

  UpdateTeamTable(){
    var count = 1;
    for (let team of TEAM_DATA) {
      team.position = count;
      count++;
    }
    this.teamDataSource = new MatTableDataSource(TEAM_DATA);
  }
}