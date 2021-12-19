import { Component, OnInit, ViewChild } from '@angular/core';
import { MatHeaderCell } from '@angular/material';
import { NewTournament } from '../../models/new-tournament';
import { GameType } from '../../models/game-type';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { TournamentService } from '../../services/tournament.service';
import { Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'tournament-info',
  styleUrls: ['tournament-info.component.css'],
  templateUrl: 'tournament-info.component.html',
})
export class TournamentInfoComponent implements OnInit {
  displayedColumns: string[] = ['title', 'game', 'winnerTeamId', 'numberOfTeams', 'open'];
  dataSource: MatTableDataSource<NewTournament>;
  tournamentData: NewTournament[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private tourServ: TournamentService, private router: Router) {

    tourServ.getAllTournaments().subscribe(tours => {
      this.tournamentData = tours;
      this.dataSource = new MatTableDataSource(this.tournamentData);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  ngOnInit() {
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onOpenMatch(tourId: string) {
    this.router.navigate(['tournament-brackets', tourId]);
  }
}
