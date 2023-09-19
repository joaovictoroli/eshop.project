import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RegisterAddress, User, UserAddress } from '../models/user';
import { BehaviorSubject, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}
  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
          this.getCurrentUserAfterChanges();
        }
      })
    );
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getCurrentUserAfterChanges() {
    var username = JSON.parse(localStorage.getItem('user') || '{}').username;
    this.http
      .get<User>(this.baseUrl + 'users/' + username)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            console.log('here');
            console.log(response);
            this.setCurrentUser(user);
          }
        })
      )
      .subscribe((response) => {
        console.log(response);
      });
  }

  registerAddress(address: RegisterAddress, cep: string) {
    return this.http.post<UserAddress>(
      this.baseUrl + 'users/register-address/' + cep,
      address
    );
  }
}
