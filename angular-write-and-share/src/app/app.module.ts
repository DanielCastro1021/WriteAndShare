import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AuthenticationComponent } from "./components/authentication/authentication.component";
import { MainPageComponent } from "./components/web/main-page/main-page.component";

@NgModule({
  declarations: [AppComponent, AuthenticationComponent, MainPageComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
