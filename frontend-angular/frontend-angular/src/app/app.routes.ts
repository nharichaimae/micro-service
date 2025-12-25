import { Routes } from '@angular/router';
import { LoginComponent } from './login/login';
import { Dashboard } from './dashboard/dashboard';
import { Pieces } from './pieces/pieces';
import { authGuard } from './guards/auth-guard';
import{equipement} from './equipement/equipement'

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: Dashboard, canActivate: [authGuard] },
  { path: 'pieces', component: Pieces, canActivate: [authGuard] },
   { path: 'add-equipement/:id', component: equipement ,canActivate: [authGuard] }
];
