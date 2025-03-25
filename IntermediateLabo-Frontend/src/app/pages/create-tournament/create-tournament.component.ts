import {Component, inject} from '@angular/core';
import {Button} from "primeng/button";
import {Card} from "primeng/card";
import {FloatLabel} from "primeng/floatlabel";
import {FormErrorComponent} from "../../components/form-error/form-error.component";
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {InputText} from "primeng/inputtext";
import {Router} from '@angular/router';
import {MessageService} from 'primeng/api';
import {finalize} from 'rxjs';
import {Checkbox} from 'primeng/checkbox';
import {DatePicker} from 'primeng/datepicker';
import {TournamentService} from '../../services/tournament.service';
import {MultiSelect} from 'primeng/multiselect';
import {TournamentCatEnum} from '../../enums/tournament-cat-enum';

@Component({
  imports: [
    Button,
    Card,
    FloatLabel,
    FormsModule,
    InputText,
    ReactiveFormsModule,
    Checkbox,
    DatePicker,
    FormErrorComponent,
    MultiSelect,
  ],
  templateUrl: './create-tournament.component.html',
  styleUrl: './create-tournament.component.scss'
})
export class CreateTournamentComponent {
  // créer le formulaire
  fb = inject(FormBuilder);
  // redirection
  router = inject(Router);
  // afficher toast
  messageService = inject(MessageService);
  tournamentService = inject(TournamentService);
  // faire des requètes vers l'api
  categories = TournamentCatEnum

  isLoading = false;

  PlayerAmountForm = this.fb.group({
    minPlayerAmount: [null, [Validators.required, Validators.min(2)]],
    maxPlayerAmount: [null, [Validators.required, Validators.max(32) ]]
  }, {validators:[control => (control.get("maxPlayerAmount")?.value >= control.get("minPlayerAmount")?.value) ? null : {maxTooLow:true}]})

  PlayerEloForm = this.fb.group({
    maxPlayerElo: [null, [Validators.max(3000)]],
    minPlayerElo: [null, [Validators.min(0)]]
  }, {validators:[control => (control.get("maxPlayerElo")?.value?? 3000) >= (control.get("minPlayerElo")?.value ?? 0)? null : {maxTooLow:true}]})

  registerForm = this.fb.group({
    name: [null, [Validators.required, Validators.maxLength(100)]],
    location: [null, [Validators.maxLength(200)]],
    playerAmount: this.PlayerAmountForm,
    playerElo: this.PlayerEloForm,
    inscriptionsEndDate:[new Date(), [Validators.required]],
    categories: [null, [Validators.required]],
    isWomenOnly: [false, []],
  });
  submit(){
    console.log({... this.registerForm.value, ...this.PlayerAmountForm.value, ...this.PlayerEloForm.value})
    if(this.registerForm.invalid) {
      return;
    }
    // soumettre le formulaire à l'api
    this.isLoading = true;
    this.tournamentService.register({... this.registerForm.value, ...this.PlayerAmountForm.value, ...this.PlayerEloForm.value}).pipe(
      finalize(() => this.isLoading = false)
    ).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'La sauvegarde a été effectuée avec succès' });
        this.router.navigate(['/']);
        //this.isLoading = false;
      },
      error: (error) => {
        if(error.status === 500) {
          this.messageService.add({ severity: 'error', summary: error.statusText });

        }
        else if(error.status === 409) {
          this.messageService.add({ severity: 'error', summary: error.error });

        }

        //this.isLoading = false;
      }
    })
  }
}
