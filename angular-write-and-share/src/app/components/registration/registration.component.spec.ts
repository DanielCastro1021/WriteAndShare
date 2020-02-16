import {
  async,
  ComponentFixture,
  TestBed,
  inject
} from "@angular/core/testing";

import { RegistrationComponent } from "./registration.component";
import {
  HttpClientTestingModule,
  HttpTestingController
} from "@angular/common/http/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { RegistrationService } from "src/app/services/registration/registration.service";
import { Router } from "@angular/router";

describe("RegistrationComponent", () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrationComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [RegistrationService]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it(`should create`, async(
    inject(
      [HttpTestingController, RegistrationService],
      (service: RegistrationService, router: Router) => {
        expect(component).toBeTruthy();
      }
    )
  ));
});
