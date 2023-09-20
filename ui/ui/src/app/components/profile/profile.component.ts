import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(
    private authService: AuthService,
    private router: Router,
    private cdRef: ChangeDetectorRef
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
          console.log(user);
        },
        error: (error) => console.error('Error fetching user:', error),
      });
    }
  }
}
