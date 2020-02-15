import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing.module";
import {
  HttpClientModule,
  HTTP_INTERCEPTORS,
  HttpClient
} from "@angular/common/http";
import { JwtInterceptor } from "./helper/jwt.interceptor";

import { AppComponent } from "./app.component";
import { MainPageComponent } from "./components/website/main-page/main-page.component";
import { RegistrationComponent } from "./components/registration/registration.component";

@NgModule({
  declarations: [AppComponent, MainPageComponent, RegistrationComponent],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, FormsModule],
  providers: [
    HttpClient,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
