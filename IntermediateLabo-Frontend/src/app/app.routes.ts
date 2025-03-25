import { Routes } from '@angular/router';
import { RegisterMemberComponent } from './pages/register-member/register-member.component';
import {CreateTournamentComponent} from './pages/create-tournament/create-tournament.component';
import {TournamentListComponent} from './pages/tournament-list/tournament-list.component';

export const routes: Routes = [
  { path: 'member/register', component: RegisterMemberComponent },
  { path: 'tournaments/create', component: CreateTournamentComponent },
  { path: 'tournaments', component: TournamentListComponent },
];
