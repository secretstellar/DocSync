import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { DocumentInfoComponent } from './components/document-info/document-info.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './components/register/register.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'register',
        component: RegisterComponent,
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: 'doc-info',
                component: DocumentInfoComponent,
                canActivate:[AuthGuard]
            }
        ]
    },
];
