import {Component, inject} from '@angular/core';
import {TournamentListItem} from '../../models/tournamentListItem.model';
import {TournamentService} from '../../services/tournament.service';
import {ButtonModule} from 'primeng/button';
import {TableModule} from 'primeng/table';
import {NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';
import {StyleClass} from 'primeng/styleclass';

@Component({
  imports: [
    TableModule,
    NgForOf,
    RouterLink,
    ButtonModule,
    StyleClass
  ],
  templateUrl: './tournament-list.component.html',
  styleUrl: './tournament-list.component.scss'
})
export class TournamentListComponent {
  TournamentList: Array<TournamentListItem> = null!;

  tournamentService = inject(TournamentService);

  Colonne = [
    {field:'name', header:'Nom'},
    {field:"location", header:"Lieu"},
    {field:"playerAmount", header:"Nombre de joueurs"},
    {field:"minPlayerAmount", header:"Nombre de joueurs minimum"},
    {field:"maxPlayerAmount", header:"Nombre de joueurs maximum"},
    {field:"categories", header:"Categories"},
    {field:"minPlayerElo", header:"Elo minimal des joueurs"},
    {field:"maxPlayerElo", header:"Elo maximal des joueurs"},
    {field:"status", header:"Status"},
    {field:"inscriptionsEndDate", header:"Date de fin d'inscription"},
    {field:"currentRound", header:"Ronde courante"},
  ]

  constructor() {
    this.tournamentService.getAllTournament().subscribe(list => this.TournamentList = list);

  }
  test(){
    // this.tournamentService.getAllTournament().subscribe(x=>this.TournamentList = x);
    console.log(this.TournamentList);
  }
}
