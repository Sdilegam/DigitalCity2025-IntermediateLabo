import {Component, Input, input} from '@angular/core';

import { routes } from '../../app.routes';
import {NgClass} from '@angular/common';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-side-nav',
  imports: [
    NgClass,
    RouterLink
  ],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent {
  @Input()
  sideBarOpenend:boolean = false

  protected readonly routes = routes;
}
