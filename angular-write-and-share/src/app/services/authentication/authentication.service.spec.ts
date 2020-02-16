import { TestBed, inject, async } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController
} from "@angular/common/http/testing";

import { AuthenticationService } from "./authentication.service";
import { HttpClient } from "@angular/common/http";

describe("AuthenticationService", () => {
  let service: AuthenticationService;

  beforeEach(() =>
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthenticationService]
    })
  );

  it("should be created", () => {
    const service: AuthenticationService = TestBed.get(AuthenticationService);
    expect(service).toBeTruthy();
  });
});
