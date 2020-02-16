import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
@Component({
  selector: 'app-header-sidnave',
  templateUrl: './header-sidnave.component.html',
  styleUrls: ['./header-sidnave.component.css']
})
export class HeaderSidnaveComponent implements OnInit {

  isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(Breakpoints.Handset);

  constructor(private breakpointObserver: BreakpointObserver, private router:Router) { }

  //Chamar authentication Service no constructor

 
  username = "Example";

  isAuthenticated: boolean;

  ngOnInit(): void {
    this.isAuthenticated = false;
     //const token = localStorage.getItem("token");
  //if(token){
   // this.isAuthenticated=true;
   //this.router.navigate(["/"]);
   //this.router.navigate(["/feed"]);
  //}
   

  }

  logout() {
    //this.serviceAuth.logout();
    this.isAuthenticated = false;
    //this.router.navigate(["/"]);
  }
}
