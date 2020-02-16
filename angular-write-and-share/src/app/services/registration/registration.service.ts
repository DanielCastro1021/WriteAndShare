import { Injectable } from "@angular/core";
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse
} from "@angular/common/http";

import { map } from "rxjs/operators";
import { User } from "../../models/User";

const endpoint = "https://localhost:5001/User/api/v1/identity/";
@Injectable({
  providedIn: "root"
})
export class RegistrationService {
  constructor(private http: HttpClient) {}

  /**
   * This function makes a http post request to REST API, to register a user.
   * @param user This is a User.
   */
  public register(user: User): any {
    return this.http
      .post<any>(endpoint + "register", { user })
      .pipe(
        map(token => {
          // login successful if there's a jwt token in the response
          if (token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem("token", JSON.stringify(token));
          }
          return user;
        })
      );
  }
}
