import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User, UserAddress } from '../models/user';
import { BehaviorSubject, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { dataFetched } from '../components/user-profile/register-address-modal/register-address-modal.component';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {
    const user: User | null = JSON.parse(
      localStorage.getItem('user') || 'null'
    );
    if (user) {
      this.currentUserSource.next(user);
    }
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
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

  getUserProfile(username: string) {
    return this.http.get<User>(this.baseUrl + 'Users/' + username);
  }
  ///api/Users/register-adress/{cep}
  registerAddress(address: RegisterAddress, cep: string) {
    return this.http.post<UserAddress>(
      this.baseUrl + 'Users/register-adress/' + cep,
      address,
      {
        observe: 'response',
      }
    );
  }

  deleteAddress(id: number) {
    return this.http.delete(this.baseUrl + 'users/delete-address/' + id, {
      observe: 'response',
    });
  }
}

export class RegisterAddress {
  numero: number;
  infoAdicional: string;
  apartamento: number;

  constructor(numero: number, infoAdicional: string, aparmatamento: number) {
    this.numero = numero;
    this.infoAdicional = infoAdicional;
    this.apartamento = aparmatamento;
  }
}
