import {Component, inject} from '@angular/core';
import {TournamentListItem} from '../../models/tournamentListItem.model';
import {TournamentService} from '../../services/tournament.service';
import {ButtonModule} from 'primeng/button';
import {TableModule} from 'primeng/table';
import {DatePipe, NgClass, NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';
import {Card} from 'primeng/card';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TournamentCatEnum} from '../../enums/tournament-cat-enum';
import {TournamentStatus} from '../../enums/tournament-status';
import {MessageService} from 'primeng/api';
import {Chip} from 'primeng/chip';

@Component({
  imports: [
    TableModule,
    RouterLink,
    ButtonModule,
    Card,
    FormsModule,
    ReactiveFormsModule,
    DatePipe,
    Chip,
    NgClass,
  ],
  templateUrl: './tournament-list.component.html',
  styleUrl: './tournament-list.component.scss'
})
export class TournamentListComponent {
  TournamentList: Array<TournamentListItem> = null!;
  messageService = inject(MessageService);

  tournamentService = inject(TournamentService);

  Colonne = [
    {field:'name', header:'Nom'},
    {field:"location", header:"Lieu"},
    {field:"categories", header:"Categories"},
    {field:"Elo", header:"Elo autorisés"},
    {field:"status", header:"Status"},
    {field:"inscriptionsEndDate", header:"Date de fin d'inscription"},
    {field:"playerAmount", header:"Nombre de joueurs"},
    {field:"currentRound", header:"Ronde courante"},
    {field:"Actions", header:""},
  ]


  deleteTournament(TournamentId:number){
    this.tournamentService.delete(TournamentId).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Le tournoi a bien été supprimé' });
        const indexToDelete = this.TournamentList.findIndex(tournament => tournament.id === TournamentId);
        this.TournamentList.splice(indexToDelete, 1);
      }
    })
  }
  constructor() {
    this.tournamentService.getAllTournament().subscribe(list => {
      this.TournamentList = list;

    console.log(this.TournamentList);
    });
  }

  protected readonly categories = TournamentCatEnum;
  protected readonly TournamentCatEnum = TournamentCatEnum;
  protected readonly TournamentStatus = TournamentStatus;
}
