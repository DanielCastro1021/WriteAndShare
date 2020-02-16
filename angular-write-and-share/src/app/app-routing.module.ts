import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthenticationComponent } from "./components/authentication/authentication.component";
import { RegistrationComponent } from "./components/registration/registration.component";

const routes: Routes = [
  {
    path: "login",
    component: AuthenticationComponent
  },
  {
    path: "register",
    component: RegistrationComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
