import { SelectionModel } from '@angular/cdk/collections';
import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { Team } from '../../models/team';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'team-picker',
  styleUrls: ['team-picker.component.css'],
  templateUrl: 'team-picker.component.html',
})
export class TeamPickerComponent {
  displayedColumns: string[] = ['select', 'id', 'title'];
  selection = new SelectionModel<Team>(true, []);
  dataSource: MatTableDataSource<Team>;
  teamsToPut: Team[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private teamService: TeamService) {
    this.putTeams();
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
  ngOnInit() {

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
