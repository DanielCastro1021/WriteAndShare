import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AuthenticationComponent } from "./components/authentication/authentication.component";
import { JwtInterceptor } from "./helper/jwt.interceptor";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatCardModule } from "@angular/material/card";
import { RegistrationComponent } from "./components/registration/registration.component";
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatListModule} from '@angular/material/list';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatExpansionModule} from '@angular/material/expansion';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { FormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import { HeaderSidnaveComponent } from './components/Website/header-sidnave/header-sidnave.component';
import { FeedComponent } from './components/Website/feed/feed.component';
import { CriarPublicacaoComponent } from './components/Website/criar-publicacao/criar-publicacao.component';
@NgModule({
  declarations: [AppComponent,
   AuthenticationComponent,
   RegistrationComponent,
    HeaderSidnaveComponent,
    FeedComponent,
    CriarPublicacaoComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCardModule
    MatToolbarModule,
    MatMenuModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatCardModule,
    MatExpansionModule,
    FormsModule,
    MatInputModule,
    Ng2SearchPipeModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
