import { Component, OnInit } from '@angular/core';
import { Team } from '../../models/team';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { element } from '../../../../node_modules/@angular/core/src/render3/instructions';
import { ElementData, asElementData } from '../../../../node_modules/@angular/core/src/view';
import { EmptyError } from '../../../../node_modules/rxjs';
import { MatTableDataSource, MatTableModule } from '../../../../node_modules/@angular/material';
import { last } from '../../../../node_modules/@angular/router/src/utils/collection';
import { Participant } from '../../models/participant';
import { ParticipantService } from '../../services/participant.service';

export class PeriodicElement {
  position: number;
  id: number;
  firstName: string;
  lastName: string;
  age: number;
}


@Component({
  selector: 'participant-table',
  styleUrls: ['./participant-table.component.css'],
  templateUrl: './participant-table.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class ParticipantTableComponent implements OnInit {
  constructor(private participantService: ParticipantService) {
    participantService.getParticipants().subscribe(a=> {
      console.log(a);
      this.InstantiateTable(a);
    },
    error => {
      console.log("negavome matchu");
      console.error(error);
    });
  }

  dataSource = new MatTableDataSource(ELEMENT_DATA);
  columnsToDisplay = ['id', 'firstName', 'lastName', 'age'];
  expandedElement: PeriodicElement;

  title = ""; 
  newParticipant = new Participant();

  ngOnInit() {
  }

  onRemoveButtonClick(element: PeriodicElement){
    this.participantService.removeParticipants(element.id).subscribe(p =>{
      ELEMENT_DATA.splice(element.position-1, 1);
      this.UpdateTable();
    })
  }

  onAddButtonClick(name: string, lastName: string, age: number) {
    if(name.length > 0 && lastName.length > 0 && age != null)
    {
      this.newParticipant = new Participant();
      this.newParticipant.firstName = name;
      this.newParticipant.lastName = lastName;
      this.newParticipant.age = age;

      this.participantService.createParticipant(this.newParticipant).subscribe(participant => {
      this.AddElement(participant);
      });   
    }
  }   

  AddElement(participant: Participant){
    ELEMENT_DATA.splice(ELEMENT_DATA.length-1, 1);

    ELEMENT_DATA.push({
      position: ELEMENT_DATA.length +1,
      id: participant.id,
      firstName: participant.firstName,
      lastName: participant.lastName,
      age: participant.age
    });

    ELEMENT_DATA.push({
      position: ELEMENT_DATA.length +1,
      id: null,
      firstName: 'Add new participant',
      lastName: '',
      age: null
    });
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }
  
  InstantiateTable(participants: Participant[]){
    if(ELEMENT_DATA.length > 0){
      ELEMENT_DATA.splice(0, ELEMENT_DATA.length);
      for (let participant of participants) {
        this.AddElement(participant);
      }
    }
  }

  UpdateTable(){
    var count = 1;
    for (let participant of ELEMENT_DATA) {
      participant.position = count;
      count++;
    }
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }
}

var ELEMENT_DATA: PeriodicElement[] = [
  {
    position: 1,
    id: null,
    firstName: 'Add new participant',
    lastName: '',
    age: null,
  }
];