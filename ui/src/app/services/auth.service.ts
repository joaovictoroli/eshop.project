import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User, UserAddress } from '../models/user';
import { BehaviorSubject, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { dataFetched } from '../components/user-profile/register-address-modal/register-address-modal.component';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private cartService: CartService) {
    const user: User | null = JSON.parse(
      localStorage.getItem('user') || 'null'
    );
    if (user) {
      this.setCurrentUser(user);
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

  registerUser(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model, {
      observe: 'response',
    });
  }

  logout() {
    console.log('Logout');
    this.cartService.clearCart();
    this.currentUserSource.next(null);
    localStorage.removeItem('user');
  }

  isUserLoggedIn(): boolean {
    const currentUser = this.currentUserSource.getValue();
    console.log('Current user:', currentUser);
    return !!currentUser;
  }

  getUserProfile(username: string) {
    return this.http.get<User>(this.baseUrl + 'Users/' + username);
    // return this.http.get<User>(this.baseUrl + 'Users/' + username);
  }

  registerAddress(address: RegisterAddress, cep: string) {
    return this.http.post<UserAddress>(
      this.baseUrl + 'Users/register-adress/' + cep,
      address,
      {
        observe: 'response',
      }
    );
  }

  setMainAdress(id: number) {
    return this.http.put(
      this.baseUrl + 'users/set-main-address/' + id,
      {},
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

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
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
