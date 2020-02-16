import {
  async,
  ComponentFixture,
  TestBed,
  inject
} from "@angular/core/testing";
import { AuthenticationComponent } from "./authentication.component";
import {
  HttpClientTestingModule,
  HttpTestingController
} from "@angular/common/http/testing";
import { AuthenticationService } from "src/app/services/authentication/authentication.service";
import { Router } from "@angular/router";
import { RouterTestingModule } from "@angular/router/testing";

describe("AuthenticationComponent", () => {
  let component: AuthenticationComponent;
  let fixture: ComponentFixture<AuthenticationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AuthenticationComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [AuthenticationService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it(`should create`, async(
    inject(
      [HttpTestingController, AuthenticationService],
      (service: AuthenticationService, router: Router) => {
        expect(component).toBeTruthy();
      }
    )
  ));
});
