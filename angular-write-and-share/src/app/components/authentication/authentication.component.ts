import { Component, OnInit, Input } from "@angular/core";
import { User } from "src/app/models/User";
import { Router } from "@angular/router";

@Component({
  selector: "app-authentication",
  templateUrl: "./authentication.component.html",
  styleUrls: ["./authentication.component.css"]
})
export class AuthenticationComponent implements OnInit {
  @Input() userData: User = new User();

  constructor() {}

  ngOnInit() {}

  /**
   * This function authenticates the credentials of a user.
   */
  login() {
    console.log(this.userData);
    if (this.userData.username === "") {
      alert("Please insert username.");
    } else if (this.userData.password === "") {
      alert("Please insert password.");
    } else {
      this.serviceLogin();
    }
  }

  /**
   * This function authenticates the credentials of a user, in the the REST API.
   */
  serviceLogin(): void {}
}
