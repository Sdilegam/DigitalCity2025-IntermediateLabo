import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {SessionService} from '../services/session.service';
import {MemberRole} from '../enums/member-role';

export const adminGuard: CanActivateFn = (route, state) => {
  console.log(inject(SessionService).session().role);
  console.log(MemberRole.Admin);
  if (inject(SessionService).session().role == MemberRole.Admin){
    return true;
  }

  const router = inject(Router);
  router.navigate(['login'], { queryParams: {redirection:route.url.join('/')}});
  return false;
};
