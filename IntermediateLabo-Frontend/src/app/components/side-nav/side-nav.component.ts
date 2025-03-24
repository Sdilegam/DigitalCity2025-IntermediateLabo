import { Component } from '@angular/core';

import { routes } from '../../app.routes';

@Component({
  selector: 'app-side-nav',
  imports: [],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent {

  protected readonly routes = routes;
}
