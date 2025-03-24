import { Routes } from '@angular/router';
import { RegisterMemberComponent } from './pages/register-member/register-member.component';
import {CreateTournamentComponent} from './pages/create-tournament/create-tournament.component';

export const routes: Routes = [
  { path: 'member/register', component: RegisterMemberComponent },
  { path: 'tournament/create', component: CreateTournamentComponent },
];
