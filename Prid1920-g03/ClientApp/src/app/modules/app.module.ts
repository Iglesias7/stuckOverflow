import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { AppRoutes } from '../routing/app.routing';
import { AppComponent } from '../components/app/app.component';
import { JwtInterceptor } from '../interceptors/jwt.interceptor';
import { NavMenuComponent } from '../components/nav-menu/nav-menu.component';
import { HomeComponent } from '../components/home/home.component';
import { LoginComponent } from '../components/auth/login/login.component';
import { SignupComponent } from '../components/auth/signup/signup.component';
import { UnknownComponent } from '../components/unknown/unknown.component';
import { RestrictedComponent } from '../components/restricted/restricted.component';
import { SharedModule } from './shared.module';
import { EditUserComponent } from '../components/user/edit-user/edit-user.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SetFocusDirective } from '../directives/setfocus.directive';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { UsersListComponent } from '../components/user/userlist/userslist.component';
import { PostListComponent } from '../components/post/postlist/postlist.component';
import { SinglePostListComponent } from '../components/post/single-post/single-post.component';
import { PostViewComponent } from '../components/post/post-view/post-view.component';
import { EditPostComponent } from '../components/post/edit-post/edit-post.component';
import { SimplemdeModule } from 'ngx-simplemde';
import { MarkdownModule, MarkedOptions  } from 'ngx-markdown';
import { TagListComponent } from '../components/tags/taglist/taglist.component'
import { EditCommentComponent } from '../components/comment/edit-comment.component'
import { EditTagComponent } from '../components/tags/edit-tag/edit-tag.component';
import { PostListByTagComponent } from '../components/post/postlistbytag/postlistbytag.component';
import {TimeAgoPipe} from 'time-ago-pipe';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    UnknownComponent,
    RestrictedComponent,
    SignupComponent,
    SetFocusDirective,
    EditUserComponent,
    UsersListComponent,
    PostListComponent,
    SinglePostListComponent,
    TagListComponent,
    EditPostComponent,
    PostViewComponent,
    EditTagComponent,
    EditPostComponent,
    EditCommentComponent,
    PostListByTagComponent,
    TimeAgoPipe
    
  ],
  entryComponents: [EditUserComponent, EditPostComponent, EditCommentComponent,  EditTagComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutes,
    BrowserAnimationsModule,
    SharedModule,
    NgbModule,
    SimplemdeModule.forRoot({
      
    }),
    MarkdownModule.forRoot(),
    // MarkdownModule.forRoot({ loader: HttpClient }),
    // MarkdownModule.forRoot({
    //   loader: HttpClient, // optional, only if you use [src] attribute
    //   markedOptions: {
    //     provide: MarkedOptions,
    //     useValue: {
    //       gfm: true,
    //       tables: true,
    //       breaks: false,
    //       pedantic: false,
    //       sanitize: false,
    //       smartLists: true,
    //       smartypants: false,
    //     },
    //   },
    // }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }