import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IAPIResponseModel, IUserDetails } from '../../models/interfaces';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  roles = ['Admin', 'User', 'Manager'];
  userDetails : IUserDetails = {name:'', email:'',role:'', userName:'', password:''};

  router = inject(Router);
  authService = inject(AuthService);
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    // Initialize form group
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$')]],
      role: ['', Validators.required],
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [
        Validators.required,
        Validators.pattern('^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$')
      ]]
    });
  }

  onSubmit(registerForm: any): void {
    if (this.registerForm.valid) {

        this.userDetails.name= this.registerForm?.value?.name;
        this.userDetails.email=this.registerForm?.value?.email;
        this.userDetails.role=this.registerForm?.value?.role;
        this.userDetails.userName=this.registerForm?.value?.username;
        this.userDetails.password=this.registerForm?.value?.password;
  
        this.authService.register(this.userDetails).subscribe((res: IAPIResponseModel) => {
          if (res.isSuccess) {
            alert("User registered successfully !");
            this.router.navigate(['/login']);
          }
          else {
            this.registerForm.reset();
            alert("User registration failed !");
          }
        }, error => {
          this.registerForm.reset();
          alert("User registration failed  !");
        });
      }
  }

  onCancel(): void {
    this.registerForm.reset();
    this.router.navigate(['login']);
  }
}
