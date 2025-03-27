import {Component, inject} from '@angular/core';
import {TournamentListItem} from '../../models/tournamentListItem.model';
import {TournamentService} from '../../services/tournament.service';
import {ButtonModule} from 'primeng/button';
import {TableModule} from 'primeng/table';
import {NgForOf} from '@angular/common';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Card} from 'primeng/card';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TournamentCatEnum} from '../../enums/tournament-cat-enum';

@Component({
  imports: [
    TableModule,
    NgForOf,
    RouterLink,
    ButtonModule,
    Card,
    FormsModule,
    ReactiveFormsModule,
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
  route = inject(ActivatedRoute)
  constructor() {
    this.tournamentService.getAllTournament().subscribe(list => this.TournamentList = list);
    console.log(this.route.snapshot.params['id']);
  }
  test(){
    // this.tournamentService.getAllTournament().subscribe(x=>this.TournamentList = x);
    console.log(this.TournamentList);
  }

  protected readonly categories = TournamentCatEnum;
}
