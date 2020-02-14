import { Component, OnInit, Input } from "@angular/core";
import { User } from "src/app/models/User";
import { AuthenticationService } from "src/app/services/authentication/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-authentication",
  templateUrl: "./authentication.component.html",
  styleUrls: ["./authentication.component.css"]
})
export class AuthenticationComponent implements OnInit {
  @Input() userData: User = new User();

  constructor(
    public authenticationService: AuthenticationService,
    private router: Router
  ) {}

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
    this.authenticationService
      .login(this.userData.username, this.userData.password)
      .subscribe(
        result => {
          //TODO:Add correct route
          this.router.navigate(["/feed"]);
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
