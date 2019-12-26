import { Component, ViewChild } from '@angular/core';
import { User, Role } from '../../models/user';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class MenuComponent {
    isExpanded = false;
    currentUser: User;

    constructor(
      private router: Router,
      private authenticationService: AuthenticationService
    ) { 
      this.currentUser = this.authenticationService.currentUser;
    }

    collapse() {
      this.isExpanded = false;
    }

    toggle() {
      this.isExpanded = !this.isExpanded;
    }

    // get currentUser() {
    //   return this.authenticationService.currentUser;
    // }
    
    get isAdmin() {
      return this.currentUser && this.currentUser.role === Role.Admin;
    }
    
    logout() {
      this.authenticationService.logout();
      this.router.navigate(['/login']);
    }
}
