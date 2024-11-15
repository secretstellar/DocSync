import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  authService = inject(AuthService);
  route = inject(Router);

  canActivate(): boolean {
    if (this.authService.isLoggedIn()) {
      return true;
    } else {
      alert('Please login first!')
      this.route.navigate(['login']);
      return false;
    }
  }
};
