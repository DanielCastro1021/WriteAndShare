import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { WebsiteService } from 'src/app/services/website/website.service';
import { Token } from 'src/app/Models/Token'
@Component({
  selector: 'app-header-sidnave',
  templateUrl: './header-sidnave.component.html',
  styleUrls: ['./header-sidnave.component.css']
})
export class HeaderSidnaveComponent implements OnInit {

  token: Token;

  isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(Breakpoints.Handset);

  constructor(private breakpointObserver: BreakpointObserver, private router: Router, private webservice: WebsiteService) { }

  //Chamar authentication Service no constructor


  username = null;

  isAuthenticated = false;

  ngOnInit(): void {
    this.token = new Token();
    this.token = this.webservice.decodeToken();

    console.log(this.token);
    if (this.token) {
      this.username = this.token.nameId;
      this.isAuthenticated = true;
    }
    this.router.navigate(["/"]);
    this.router.navigate(["/feed"]);
  }

  logout() {
    //this.serviceAuth.logout();
    this.isAuthenticated = false;
    this.router.navigate(["/"]);
  }
}
