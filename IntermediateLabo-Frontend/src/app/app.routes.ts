import { Routes } from '@angular/router';
import { RegisterMemberComponent } from './pages/register-member/register-member.component';
import {CreateTournamentComponent} from './pages/create-tournament/create-tournament.component';
import {TournamentListComponent} from './pages/tournament-list/tournament-list.component';
import {LoginComponent} from './pages/login/login.component';
import {AuthenticatedGuard} from './guards/authenticatedGuard';
import {adminGuard} from './guards/admin.guard';

export const routes: Routes = [
  { path: 'member/register', component: RegisterMemberComponent, canActivate:[adminGuard]},
  { path: 'tournaments/create', component: CreateTournamentComponent, canActivate:[adminGuard]},
  { path: 'tournaments', component: TournamentListComponent, canActivate:[AuthenticatedGuard]},
  { path: 'login', component: LoginComponent },
];
