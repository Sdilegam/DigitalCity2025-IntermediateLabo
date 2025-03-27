import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {TournamentListItem} from '../models/tournamentListItem.model';

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  httpClient = inject(HttpClient);

  constructor() { }

  getAllTournament(){
    return this.httpClient.get<TournamentListItem[]>(environment.baseApiUrl + '/Tournament')
  }
  register(form: any){
    return this.httpClient.post(environment.baseApiUrl + '/Tournament', form);
  }
}
