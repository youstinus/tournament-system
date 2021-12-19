import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Team } from '../models/team';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  constructor(private http: HttpClient) { }
  private readonly teamApi = `${environment.webApiUrl}/Teams`;
  private readonly httpOptions =
    {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json'
        })
    };
  getAllTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(this.teamApi);
  }  

  createTeam(team: Team): Observable<Team> {
    return this.http.post<Team>(this.teamApi, team, this.httpOptions);
  }  

  removeTeam(id: number): Observable<Team> {
    return this.http.delete<Team>(`${this.teamApi}/${id}`);
  }

  getTeamById(id: number): Observable<Team> {
    return this.http.get<Team>(`${this.teamApi}/${id}`);
  }
}
