import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {SessionService} from '../services/session.service';

export const AuthenticatedGuard: CanActivateFn = (route, state) => {
  if (inject(SessionService).session().isAuthenticated){
    return true;
  }

  const router = inject(Router);
  router.navigate(['login'], { queryParams: {redirection:route.url.join('/')}});
  return false;
};
