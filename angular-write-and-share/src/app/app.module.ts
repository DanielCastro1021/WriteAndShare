import { BrowserModule } from "@angular/platform-browser";
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { MainPageComponent } from "./components/website/main-page/main-page.component";
import { ResgistrationComponent } from "./components/resgistration/resgistration/resgistration.component";

@NgModule({
  declarations: [AppComponent, MainPageComponent, ResgistrationComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
