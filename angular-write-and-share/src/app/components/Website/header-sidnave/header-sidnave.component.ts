import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-header-sidnave',
  templateUrl: './header-sidnave.component.html',
  styleUrls: ['./header-sidnave.component.css']
})
export class HeaderSidnaveComponent implements OnInit {

  username="John";

  isAuthenticated = false;

  isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(Breakpoints.Handset);
  
  constructor(private breakpointObserver: BreakpointObserver) { }

  ngOnInit(): void {
  }

}
