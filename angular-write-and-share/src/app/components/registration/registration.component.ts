import { Component, OnInit, Input } from "@angular/core";
import { RegistrationService } from "src/app/services/registration/registration.service";
import { Router } from "@angular/router";
import { User } from "src/app/models/User";
import { ValidationService } from "src/app/services/validation/validation.service";

@Component({
  selector: "app-registration",
  templateUrl: "./registration.component.html",
  styleUrls: ["./registration.component.css"]
})
export class RegistrationComponent implements OnInit {
  @Input() userData: User = new User();
  cpassword: string;

  constructor(
    private resgistrationService: RegistrationService,
    private validationService: ValidationService,
    private router: Router
  ) {}
  ngOnInit(): void {}

  /**
   * This function registers the user, if form is valid.
   */
  register(): void {
    console.log(this.userData);
    if (
      this.validationService.validateUsername(this.userData.username) &&
      this.validationService.validatePassword(
        this.userData.password,
        this.cpassword
      )
    ) {
      this.registerUserService();
    }
  }

  /**
   * This function posts the user, in the REST API.
   */
  registerUserService(): void {
    this.resgistrationService.register(this.userData).subscribe(
      result => {
        console.log(result);
        this.router.navigate(["/login"]);
      },
      err => {
        console.log(err);
      }
    );
  }

  swithToSignInPage() {
    this.router.navigate(["/"]);
    this.router.navigate(["/feed"]);
  }
}
