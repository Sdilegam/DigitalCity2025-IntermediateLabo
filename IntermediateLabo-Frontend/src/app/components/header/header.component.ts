import {Component, Input, model} from '@angular/core';
import {Button} from 'primeng/button';

@Component({
  selector: 'app-header',
  imports: [
    Button
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  sideBarOpened = model<boolean>(false);


  toggleSide(){
    this.sideBarOpened.update(variable=> variable = !variable);
  }
}
