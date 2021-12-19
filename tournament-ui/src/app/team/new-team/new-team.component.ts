import { Component, OnInit } from '@angular/core';
import { Team } from '../../models/team';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { element } from '../../../../node_modules/@angular/core/src/render3/instructions';
import { ElementData } from '../../../../node_modules/@angular/core/src/view';
import { EmptyError } from '../../../../node_modules/rxjs';
import { MatTableDataSource, MatTableModule } from '../../../../node_modules/@angular/material';
import { last } from '../../../../node_modules/@angular/router/src/utils/collection';

export interface PeriodicElement {
  position: number;
  firstName: string;
  lastName: string;
  age : number;
  description: string;
}

@Component({
  selector: 'app-new-team',
  styleUrls: ['./new-team.component.css'],
  templateUrl: './new-team.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class NewTeamComponent implements OnInit {
  constructor() { }

  dataSource = new MatTableDataSource(ELEMENT_DATA);
  columnsToDisplay = ['firstName', 'lastName', 'age'];
  expandedElement: PeriodicElement;

  title = ""; 
  team = new Team();

  ngOnInit() {
  }

  onCreateButtonClick(value: string) {
    this.team.title = value;
  }

  onRemoveButtonClick(index: number){
    ELEMENT_DATA.splice(index-1, 1);
    this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }

  onAddButtonClick(name: string, lastName: string, age: number) {

    ELEMENT_DATA.splice(ELEMENT_DATA.length-1, 1);

    ELEMENT_DATA.push({
      position: ELEMENT_DATA.length +1,
      firstName: name,
      lastName: lastName,
      age: age,
      description: ''});

    ELEMENT_DATA.push({
      position: ELEMENT_DATA.length +1,
      firstName: 'Add new participant',
      lastName: '',
      age: null,
      description: ''});
      this.dataSource = new MatTableDataSource(ELEMENT_DATA);
  }
}

const ELEMENT_DATA: PeriodicElement[] = [
  {
    position: 1,
    firstName: 'example',
    lastName: 'example',
    age: 21,
    description: 'Very good attacker'
  }, {
    position: 2,
    firstName: 'Example',
    lastName: 'gas',
    age: 22,
    description: 'Very good attacker'
  },{
    position: 3,
    firstName: 'Add new participant',
    lastName: '',
    age: null,
    description: ''
  }
];
