import {Component, inject} from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';
import { Toast } from 'primeng/toast';
import { ConfirmDialog } from 'primeng/confirmdialog';
import {HeaderComponent} from './components/header/header.component';
import {SideNavComponent} from './components/side-nav/side-nav.component';
import { HttpClient } from '@angular/common/http';
import {SessionService} from './services/session.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Toast, ConfirmDialog, HeaderComponent, SideNavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  httpClient = inject(HttpClient);
  sessionService = inject(SessionService);
  router = inject(Router);
  sideBarOpened = false;
}
