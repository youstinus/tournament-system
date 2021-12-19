import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Team } from '../models/team';
import { Participant } from '../models/participant';

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {
  constructor(private http: HttpClient) { }

  private readonly participantApi = `${environment.webApiUrl}/Participants`;
  private readonly httpOptions =
    {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json'
        })
    };
  createParticipant(newParticipant: Participant): Observable<Participant> {
    return this.http.post<Participant>(this.participantApi, newParticipant, this.httpOptions);
  }
  
  getParticipants(): Observable<Participant[]> {
    return this.http.get<Participant[]>(this.participantApi);
  } 
  
  removeParticipants(id: number): Observable<Participant> {
    return this.http.delete<Participant>(`${this.participantApi}/${id}`);
  }  
}
