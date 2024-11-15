import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAPIResponseModel, IUser, IUserDetails } from '../models/interfaces';
import { environment } from '../../environments/environment.development';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userPayload: any;

  http = inject(HttpClient);
  route = inject(Router);

  constructor() {
    this.userPayload = this.decodedToken();
  }

  login(user: IUser): Observable<IAPIResponseModel> {
    return this.http.post<IAPIResponseModel>(environment.API_URL + "Auth/login", user)
  }

  register(userDetails: IUserDetails){
    return this.http.post<IAPIResponseModel>(environment.API_URL + "Auth/register", userDetails)
  }

  logOut() {
    localStorage.clear();
    this.route.navigate([('login')]);
  }

  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue);
  }

  storeUserName(userName: string) {
    localStorage.setItem('userName', userName);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getUserName() {
    return localStorage.getItem('userName');
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token));
    return jwtHelper.decodeToken(token);
  }

  getNameFromToken() {
    if (this.userPayload)
      return this.userPayload.name;
  }

  getRoleFromToken() {
    if (this.userPayload)
      return this.userPayload.role;
  }
}
