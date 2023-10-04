import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { first, take } from 'rxjs';
import { User, UserAddress } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  user: User | null | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdRef: ChangeDetectorRef,
    private toastr: ToastrService
  ) {
    this.authService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => (this.user = user),
    });
  }

  ngOnInit() {
    this.loadAuthorizedUser();
  }

  loadAuthorizedUser() {
    if (this.user && this.user.username) {
      this.authService.getUserProfile(this.user.username).subscribe({
        next: (user) => {
          this.user = user;
          this.cdRef.detectChanges();
          console.log(user.addresses);
        },
        error: (error) => console.error('Error fetching user:', error),
      });
    }
  }

  addressAddedSuccessfully(wasAdded: boolean) {
    if (wasAdded) {
      this.loadAuthorizedUser();
      console.log('Endereço adicionado com sucesso.');
    }
  }

  deleteAddress(id: number) {
    this.authService.deleteAddress(id).subscribe({
      next: (response) => {
        if (response.status === 204) {
          this.loadAuthorizedUser();
          this.toastr.success('Endereço deletado com sucesso.');
        }
      },
      error: (error) => {
        console.error('Error deleting address:', error);
        if (error.status === 400) {
          const errorMessage = error.error;
          this.toastr.error(errorMessage);
        }
      },
    });
  }

  addressEdit(mainAddressId: number) {
    console.log('Endereço principal alterado:', mainAddressId);
    if (mainAddressId) {
      this.authService.setMainAdress(mainAddressId!).subscribe({
        next: (response) => {
          if (response.status === 200) {
            // this.toastr.success('Endereço principal alterado com sucesso.');
            // this.loadAuthorizedUser();
            // window.location.reload();
            console.log('padrao no content');
          }
        },
        error: (error) => {
          if (error.status === 200) {
            this.toastr.success('Endereço principal alterado com sucesso.');
            this.loadAuthorizedUser();
          } else {
            this.toastr.info('Alguma coisa aconteceu de errado');
          }
        },
      });
    }
  }
}
