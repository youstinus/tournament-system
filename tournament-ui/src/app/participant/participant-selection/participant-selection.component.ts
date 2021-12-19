import { Component, OnInit } from '@angular/core';
import {SelectionModel} from '@angular/cdk/collections';
import {MatTableDataSource} from '@angular/material';
import { Participant } from '../../models/participant';
import { ParticipantService } from '../../services/participant.service';



/**
 * @title Table with selection
 */
@Component({
  selector: 'app-participant-selection',
  templateUrl: './participant-selection.component.html',
  styleUrls: ['./participant-selection.component.css']
})
export class ParticipantSelectionComponent {

  
}