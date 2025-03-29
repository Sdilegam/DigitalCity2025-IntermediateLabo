import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { provideAnimations } from '@angular/platform-browser/animations';
import Material from '@primeng/themes/material';
import Lara from '@primeng/themes/lara';
import Aura from '@primeng/themes/Aura';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {tokenInterceptor} from './interceptors/token.interceptor';


export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(withInterceptors([
      tokenInterceptor,
    ])),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
      }
    }),
    MessageService,
    ConfirmationService,
  ]
};
