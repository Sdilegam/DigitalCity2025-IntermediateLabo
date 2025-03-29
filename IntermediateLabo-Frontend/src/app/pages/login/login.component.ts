import {Component, inject} from '@angular/core';
import {Button} from "primeng/button";
import {Calendar} from "primeng/calendar";
import {Card} from "primeng/card";
import {FloatLabel} from "primeng/floatlabel";
import {FormErrorComponent} from "../../components/form-error/form-error.component";
import {InputText} from "primeng/inputtext";
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {Select} from "primeng/select";
import {MessageService} from 'primeng/api';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';
import {environment} from '../../../environments/environment';
import {SessionService} from '../../services/session.service';

@Component({
  imports: [
    Button,
    Card,
    FloatLabel,
    InputText,
    ReactiveFormsModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  formBuilder = inject(FormBuilder);
  httpClient = inject(HttpClient);
  messageService = inject(MessageService);
  router = inject(Router);
  route = inject(ActivatedRoute)
  sessionService = inject(SessionService);
  returnUrl:string;
  loginForm = this.formBuilder.group({
    username: [null, [Validators.required]],
    password: [null, [Validators.required]]
  })

  constructor() {
    this.returnUrl = this.route.snapshot.queryParams['redirection'] || '/';
  }
  login(){
    if(this.loginForm.invalid){
      return;
    }
    console.log(this.route.queryParams)
    this.httpClient.post<{ token: string }>(environment.baseApiUrl+"/login", this.loginForm.value).subscribe(
      {
        next: ({token}) => {
          this.sessionService.start(token);
          this.router.navigate([this.returnUrl]);
        },
        error: (err) => {
          this.messageService.add({severity: 'error', summary: err.message});
        }
      }
    )
  }
}
