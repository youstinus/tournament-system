import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Match } from '../../models/match';
import { HttpClient } from '@angular/common/http';
import { MatchService } from '../../services/match.service';
import { TournamentService } from '../../services/tournament.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Team } from '../../models/team';
import { NewTournament } from '../../models/new-tournament';
import { Title } from '../../../../node_modules/@angular/platform-browser';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'tournaments-bracket',
  templateUrl: './tournament-brackets.component.html',
  styleUrls: ['./tournament-brackets.component.css']
})
export class TournamentBracketsComponent implements OnInit {
  constructor(private matchService: MatchService, private route: ActivatedRoute, private tService: TournamentService, private teamService: TeamService) {
  }
  tournamentObject: NewTournament;
  matches: Match[];
  errorMessage: string;
  // matchesChanged: Match[];
  teamsToPrint: Team[];
  winnerTitle: string;


  ngOnInit() {
    
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        const id = params.get('id');
        this.tService.getTour(+id).subscribe(tour => {
          return this.tournamentObject = tour
        });
        //console.log(this.matchService.getMatches(+id).subscribe());
        return this.matchService.getMatches(+id);//+id); // +id converts to number from string
      }
      ))
      .subscribe(matches => {
        this.matches = matches;
        if (this.matches != null && this.matches[this.matches.length - 1].winnerId != null) {
          this.teamService.getTeamById(this.matches[this.matches.length - 1].winnerId).subscribe(x => {
            return this.winnerTitle = x.title;
          });
        }
      },
        error => {
          console.error(error);
          this.errorMessage = error.message;
        }
      );
  }

  onAddButtonClick(teamScore1: number, teamScore2: number, sequenceNumber: number) {
    if (teamScore1 != null && teamScore2 != null && sequenceNumber != null && sequenceNumber > 0) {
      let matchIndex = sequenceNumber - 1;
      let matchToUpdate = this.matches[matchIndex];
      matchToUpdate.scoreTeam1 = teamScore1;
      matchToUpdate.scoreTeam2 = teamScore2;
      if (teamScore1 == teamScore2) {
        // its a draw. Enter scores of the rematch
      }
      else if (teamScore1 > teamScore2) {
        matchToUpdate.winnerId = matchToUpdate.matchTeams[0].id;
        this.whereGoesWinner(matchIndex, this.matches[matchIndex].matchTeams[0]);
        this.whereGoesLoser(matchIndex, this.matches[matchIndex].matchTeams[1]);

      }
      else if (teamScore2 > teamScore1) {
        matchToUpdate.winnerId = matchToUpdate.matchTeams[1].id;
        this.whereGoesWinner(matchIndex, this.matches[matchIndex].matchTeams[1]);
        this.whereGoesLoser(matchIndex, this.matches[matchIndex].matchTeams[0]);
      }
      else {
        console.log("wrong after button click");
      }
      if (this.matches != null && this.matches.length == sequenceNumber) {
        let finalWinner = new Team();
        finalWinner.title = "Unknown";
        if (teamScore1 > teamScore2) {
          finalWinner = this.matches[sequenceNumber - 1].matchTeams[0];
        }
        else if (teamScore2 > teamScore1) {
          finalWinner = this.matches[sequenceNumber - 1].matchTeams[1];
        }
        else if (teamScore1 == teamScore2) {
          // its a draw
        }
        else {
          console.log("Something wrong in finals");
        }
        matchToUpdate.winnerId = finalWinner.id;
        this.tournamentObject.winnerTeamId = finalWinner.id;
        this.tService.updateTour(this.tournamentObject).subscribe(x => {
          //console.log(x, this.tournamentObject);
          return this.tournamentObject.winnerTeamId = matchToUpdate.winnerId;
        });


        if (matchToUpdate.winnerId != null) {
          this.teamService.getTeamById(matchToUpdate.winnerId).subscribe(x => {
            return this.winnerTitle = x.title;
          });
        }
        this.tService.updateTour(this.tournamentObject);
        alert("Tournament winner is " + finalWinner.title); // is alert ok?

      }
      this.matchService.updateMatch(matchToUpdate).subscribe(x => {
        return matchToUpdate = x;
      });
    }
  }
  whereGoesWinner(matchIndex: number, team: Team) {
    if (this.matches[matchIndex].winnerGoesToId != null) {
      var winnersMatch = this.matches[this.matches[matchIndex].winnerGoesToId - 1];
      if (winnersMatch.matchTeams == null || winnersMatch.matchTeams[0] == null) {// && winnersMatch.team1.id == 0) {
        winnersMatch.matchTeams = []; // how to create new()?
        winnersMatch.matchTeams[0] = new Team();
        winnersMatch.matchTeams[0] = team;//.id = team.id;
      }
      else if (winnersMatch.matchTeams != null && winnersMatch.matchTeams[1] == null && winnersMatch.matchTeams[0] != team) {
        winnersMatch.matchTeams[1] = new Team();
        winnersMatch.matchTeams[1] = team;//.id = team.id;
      }
      console.log(winnersMatch);
      this.matchService.updateMatch(winnersMatch).subscribe(x => {
        return winnersMatch = x;
      });
    }
  }
  whereGoesLoser(matchIndex: number, team: Team) {
    if (this.matches[matchIndex].loserGoesToId != null) {
      let losersMatch = this.matches[this.matches[matchIndex].loserGoesToId - 1];
      if (losersMatch.matchTeams == null || losersMatch.matchTeams[0] == null) {//losersMatch.team1.id == 0) { // ar null matchTeams?
        losersMatch.matchTeams = []; // How to create new instance?
        losersMatch.matchTeams[0] = new Team();
        losersMatch.matchTeams[0] = team;//.id = team.id;
      }
      else if (losersMatch.matchTeams != null && losersMatch.matchTeams[1] == null && losersMatch.matchTeams[0] != team) {
        losersMatch.matchTeams[1] = new Team();
        losersMatch.matchTeams[1] = team;//.id = team.id;
      }
      //if (losersMatch.id != this.matches[this.matches[matchIndex].loserGoesToId - 1].id) {
      console.log(losersMatch);
      this.matchService.updateMatch(losersMatch).subscribe(x => {
        return losersMatch = x;
      });
      // }

    }
  }
  getStyleTeam1(i: number) {
    if (this.matches[i].winnerId != 0 && this.matches[i].winnerId == this.matches[i].matchTeams[0].id) {
      return "green";
    } else {
      return "";
    }
  }
  getStyleTeam2(i: number) {
    if (this.matches[i].winnerId != 0 && this.matches[i].winnerId == this.matches[i].matchTeams[1].id) {
      return "green";
    } else {
      return "";
    }
  }
}
