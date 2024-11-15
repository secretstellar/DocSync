import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserStoreService } from '../../services/user-store.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent implements OnInit {

  name: string = "";
  authService = inject(AuthService);
  userStore = inject(UserStoreService);

  ngOnInit(): void {
    this.userStore.getNameFromStore().subscribe((res: any) => {
      let nameFromToken = this.authService.getNameFromToken();
      this.name = res || nameFromToken;
    });
  }
  onLogOut() {
    this.authService.logOut();
  }
}
