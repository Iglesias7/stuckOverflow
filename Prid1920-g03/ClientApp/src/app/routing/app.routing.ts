import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { CounterComponent } from '../components/counter/counter.component';
import { FetchDataComponent } from '../components/fetch-data/fetch-data.component';
import { UserListComponent } from '../components/userlist/userlist.component';
import { LoginComponent } from '../components/login/login.component';
import { SignupComponent } from '../components/signup/signup.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { CounterParentComponent } from '../components/counter-stateless/counter-parent.component';
import { AuthGuard } from '../services/auth.guard';
import { Role } from '../models/user';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'counter-stateless', component: CounterParentComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  {path: 'users', component: UserListComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }},
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  { path: 'restricted', component: RestrictedComponent },
  { path: '**', component: UnknownComponent }

];

export const AppRoutes = RouterModule.forRoot(appRoutes);