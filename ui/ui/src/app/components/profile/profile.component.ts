import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  user: User | null | undefined;
  @Input()
  address = {
    cep: '',
    uf: '',
    bairro: '',
    // ... outros campos do endereço
  };
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
    console.log('aqui');
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
          const errorMessage = error.error; // Deserializing JSON
          this.toastr.error(errorMessage);
        }
      },
    });
  }
  onSubmit() {
    console.log(this.address);
  }
}
