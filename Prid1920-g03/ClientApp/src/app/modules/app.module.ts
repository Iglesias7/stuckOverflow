import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutes } from '../routing/app.routing';
import { AppComponent } from '../components/app/app.component';
import { JwtInterceptor } from '../interceptors/jwt.interceptor';
import { NavMenuComponent } from '../components/nav-menu/nav-menu.component';
import { HomeComponent } from '../components/home/home.component';
import { LoginComponent } from '../components/auth/login/login.component';
import { SignupComponent } from '../components/auth/signup/signup.component';
import { CounterComponent } from '../components/count/counter/counter.component';
import { FetchDataComponent } from '../components/fetch-data/fetch-data.component';
import { UserListComponent } from '../components/user/userlist/userlist.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { CounterStatelessComponent } from '../components/count/counter-stateless/counter-stateless.component';
import { CounterParentComponent } from '../components/count/counter-stateless/counter-parent.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { SharedModule } from './shared.module';
import { EditUserComponent } from '../components/user/edit-user/edit-user.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SetFocusDirective } from '../directives/setfocus.directive';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RelationshipsComponent } from '../components/relationships/relationships.component';
import { UserCardComponent } from '../components/user/userlist/usersCard.component';
import { PostListComponent } from '../components/post/postlist/postlist.component';
import { SinglePostListComponent } from '../components/post/single-post/single-post.component';
import { SimplemdeModule } from 'ngx-simplemde';
import { MarkdownModule } from 'ngx-markdown';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    UserListComponent,
    UnknownComponent,
    RestrictedComponent,
    SignupComponent,
    CounterStatelessComponent,
    CounterParentComponent,
    SetFocusDirective,
    EditUserComponent,
    RelationshipsComponent,
    UserCardComponent,
    PostListComponent,
    SinglePostListComponent
  ],
  entryComponents: [EditUserComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutes,
    BrowserAnimationsModule,
    SharedModule,
    NgbModule,
    SimplemdeModule.forRoot(),
    MarkdownModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }