import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private jwtHelper = new JwtHelperService();

  constructor(private router: Router) {}

  getAuthToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    this.router.navigateByUrl('/login');
    localStorage.removeItem('token');
  }

  getUserName(): string | null {
    const token = this.getAuthToken();
    return token ? this.jwtHelper.decodeToken(token)['unique_name'] : null;
  }
  getUserId(): string | null {
    const token = this.getAuthToken();
    return token ? this.jwtHelper.decodeToken(token)['nameid'] : null;
  }

  isAuthenticated(): boolean {
    const token = this.getAuthToken();
    return token ? !this.jwtHelper.isTokenExpired(token) : false;
  }
}
