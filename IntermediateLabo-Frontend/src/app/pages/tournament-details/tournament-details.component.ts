import {Component, inject} from '@angular/core';
import {TournamentDetailsDTO} from '../../models/tournament-details-dto';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {TournamentService} from '../../services/tournament.service';
import {MessageService} from 'primeng/api';
import {Card} from 'primeng/card';
import {TournamentCatEnum} from '../../enums/tournament-cat-enum';
import {TournamentStatus} from '../../enums/tournament-status';
import {Divider} from 'primeng/divider';
import {DatePipe, NgClass, NgTemplateOutlet} from '@angular/common';
import {Chip} from 'primeng/chip';
import {StyleClass} from 'primeng/styleclass';

@Component({
  imports: [
    Card,
    Divider,
    NgClass,
    Chip,
    DatePipe,
    NgTemplateOutlet,
    StyleClass,
  ],
  templateUrl: './tournament-details.component.html',
  styleUrl: './tournament-details.component.scss'
})
export class TournamentDetailsComponent {
tournamentService = inject(TournamentService);
messageService = inject(MessageService);
router = inject(Router);

currentTournament: TournamentDetailsDTO=null!;
tournamentID: number = null!;


constructor() {
  inject(ActivatedRoute).params.subscribe(params => this.tournamentID = params['id']);
  this.tournamentService.getTournament(this.tournamentID).subscribe({
    next: data => {
      this.currentTournament = data;
    console.log(this.currentTournament)
    },
    error: err => {
      this.messageService.add({
        severity: 'error',
        summary: 'The tournament was not found',
      })
      this.router.navigate(['/tournaments']);
    }
  })
}


  protected readonly TournamentCatEnum = TournamentCatEnum;
  protected readonly TournamentStatus = TournamentStatus;
}
