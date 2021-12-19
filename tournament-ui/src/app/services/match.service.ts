import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Match } from '../models/match';
import { Team } from '../models/team';
import { NewTournament } from '../models/new-tournament';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private http: HttpClient) { }
  private readonly generatorApi = `${environment.webApiUrl}/generator`;
  private readonly matchesApi = `${environment.webApiUrl}/matches`;
  private readonly httpOptions =
    {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json'
        })
    };
  getAllMatches(tournamentId: number): Observable<Match[]> {
    console.log("Trying to get all saved matches from DB. Adress:  " + this.generatorApi + "/" + tournamentId);
    return this.http.get<Match[]>(this.generatorApi + "/" + tournamentId);
  }
getMatches(tournamentId: number): Observable<Match[]> {
  return this.http.get<Match[]>(this.matchesApi + "/tournament/" + tournamentId);
}

  createMatches(tournament: NewTournament, teams: Team[]): Observable<Match[]> {
    console.log("Creating matches, new tournament object and teams list. Check here");
    console.log(tournament, teams);
    console.log("Sending id with teams");
    return this.http.post<Match[]>(this.generatorApi + "/" + tournament.id, teams);
  }
  
  updateMatch(match: Match): Observable<Match> {
    console.log(this.matchesApi + "/" + match.id);
    return this.http.put<Match>(this.matchesApi + "/" + match.id, match);
  }
}
