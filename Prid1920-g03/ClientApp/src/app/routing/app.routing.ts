import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { CounterComponent } from '../components/count/counter/counter.component';
import { FetchDataComponent } from '../components/fetch-data/fetch-data.component';
import { UserListComponent } from '../components/user/userlist/userlist.component';
import { UserCardComponent } from '../components/user/userlist/usersCard.component';
import { LoginComponent } from '../components/auth/login/login.component';
import { SignupComponent } from '../components/auth/signup/signup.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { CounterParentComponent } from '../components/count/counter-stateless/counter-parent.component';
import { AuthGuard } from '../services/auth.guard';
import { Role } from '../models/user';
import { RelationshipsComponent } from '../components/relationships/relationships.component';
import { PostListComponent } from '../components/post/postlist/postlist.component';
import { SinglePostListComponent } from '../components/post/single-post/single-post.component';
import { TagListComponent } from '../components/tags/taglist.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'counter-stateless', component: CounterParentComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  {path: 'users', component: UserListComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] }},
  { path: 'friends', component: RelationshipsComponent, canActivate: [AuthGuard] },
  { path: 'UsersCard', component: UserCardComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostListComponent },
  { path: 'single-post/:id', component: SinglePostListComponent },
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  { path: 'tags', component: TagListComponent},
  { path: 'restricted', component: RestrictedComponent },
  { path: '**', component: UnknownComponent }

];

export const AppRoutes = RouterModule.forRoot(appRoutes);