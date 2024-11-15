import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IAPIResponseModel, IUser } from '../../models/interfaces';
import { AuthService } from '../../services/auth.service';
import { UserStoreService } from '../../services/user-store.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  userName: string = '';
  password: string = '';
  user: IUser = { userName: '', password: '' };
  constructor() {
  }

  router = inject(Router);
  authService = inject(AuthService);
  userStore = inject(UserStoreService);

  onLogin(loginForm: any): void {
    if (loginForm.valid) {

      this.user.userName = this.userName;
      this.user.password = this.password;

      this.authService.login(this.user).subscribe((res: IAPIResponseModel) => {
        if (res.isSuccess) {
          this.authService.storeToken(res.data?.token);
          this.authService.storeUserName(this.user.userName);

          const tokePayload = this.authService.decodedToken();
          this.userStore.setNameFromStore(tokePayload.name);
          this.userStore.setRoleFromStore(tokePayload.role);
          alert("Login success !");
          this.router.navigate(['/doc-info']);
        }
        else {
          loginForm.reset();
          alert("Login failed !");
        }
      }, error => {
        loginForm.reset();
        alert("Login failed !");
      });
    }
  }

  onRegister()
  {
    this.router.navigate(['/register']);
  }
}
