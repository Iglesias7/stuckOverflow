import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../components/home/home.component';
import { UsersListComponent } from '../components/user/userlist/userslist.component';
import { LoginComponent } from '../components/auth/login/login.component';
import { SignupComponent } from '../components/auth/signup/signup.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { AuthGuard } from '../services/auth.guard';
import { PostListComponent } from '../components/post/postlist/postlist.component';
import { SinglePostListComponent } from '../components/post/single-post/single-post.component';
import { TagListComponent } from '../components/tags/taglist/taglist.component';
import { PostListByTagComponent } from '../components/post/postlistbytag/postlistbytag.component';


const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'users', component: UsersListComponent, canActivate: [AuthGuard] },
  { path: 'posts', component: PostListComponent },
  { path: 'single-post/:id', component: SinglePostListComponent },
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent}, 
  {path: 'posts/tagged/:name', component: PostListByTagComponent},
  { path: 'tags', component: TagListComponent},
  { path: 'restricted', component: RestrictedComponent },
  { path: '**', component: UnknownComponent }

];

export const AppRoutes = RouterModule.forRoot(appRoutes);