import { Component, OnInit, Input } from "@angular/core";
import { User } from "src/app/models/User";
import { Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication/authentication.service";

@Component({
  selector: "app-authentication",
  templateUrl: "./authentication.component.html",
  styleUrls: ["./authentication.component.css"]
})
export class AuthenticationComponent implements OnInit {
  @Input() userData: User = new User();

  constructor(public service: AuthenticationService, private router: Router) {}

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
  serviceLogin(): void {
    this.service
      .login(this.userData.username, this.userData.password)
      .subscribe(
        result => {
          this.router.navigate(["/"]);
        },
        err => {
          console.log(err);
          alert(
            "Sorry we could not found you, please check if username and password are correct."
          );
        }
      );
  }
}
