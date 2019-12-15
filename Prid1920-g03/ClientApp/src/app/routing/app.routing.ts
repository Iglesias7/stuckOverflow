import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { CounterComponent } from '../components/count/counter/counter.component';
import { UserListComponent } from '../components/user/userlist/userlist.component';
import { UserCardComponent } from '../components/user/userlist/usersCard.component';
import { LoginComponent } from '../components/auth/login/login.component';
import { SignupComponent } from '../components/auth/signup/signup.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { CounterParentComponent } from '../components/count/counter-stateless/counter-parent.component';
import { AuthGuard } from '../services/auth.guard';
import { Role } from '../models/user';
import { PostListComponent } from '../components/post/postlist/postlist.component';
import { SinglePostListComponent } from '../components/post/single-post/single-post.component';
import { TagListComponent } from '../components/tags/taglist/taglist.component';
import { PostListByTagComponent } from '../components/post/postlistbytag/postlistbytag.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'counter-stateless', component: CounterParentComponent },
  { path: 'users', component: UserCardComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostListComponent },
  { path: 'single-post/:id', component: SinglePostListComponent },
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent}, 
  {path: 'postlistbytag/:name', component: PostListByTagComponent},
  { path: 'tags', component: TagListComponent},
  { path: 'restricted', component: RestrictedComponent },
  { path: '**', component: UnknownComponent }

];

export const AppRoutes = RouterModule.forRoot(appRoutes);