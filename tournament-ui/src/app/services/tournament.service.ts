import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { NewTournamentComponent } from '../tournament/new-tournament/new-tournament.component';
import { NewTournament } from '../models/new-tournament';


@Injectable({
  providedIn: 'root'
})
export class TournamentService {

  constructor(private http: HttpClient) { }
  private readonly tournamentApi = `${environment.webApiUrl}/tournaments`;
  private readonly httpOptions =
    {
      headers: new HttpHeaders
        ({
          'Content-Type': 'application/json'
        })
    };
  addTournament(tournament: NewTournament): Observable<NewTournament> {
    return this.http.post<NewTournament>(this.tournamentApi, tournament, this.httpOptions);
  }

  getAllTournaments(): Observable<NewTournament[]> {
    return this.http.get<NewTournament[]>(this.tournamentApi);
  }

  getTour(id: number): Observable<NewTournament> {
    return this.http.get<NewTournament>(this.tournamentApi + "/" + id);
  }

  updateTour(tour: NewTournament): Observable<NewTournament> {
    return this.http.put<NewTournament>(this.tournamentApi + "/" + tour.id, tour);
  }
}
