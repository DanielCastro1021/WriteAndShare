import { Injectable } from "@angular/core";
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse
} from "@angular/common/http";

import { map } from "rxjs/operators";
import { User } from "../../models/User";

const endpoint = "http://localhost:3000/api/resgistration/";
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
        map(user => {
          // login successful if there's a jwt token in the response
          if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem("currentUser", JSON.stringify(user));
          }
          return user;
        })
      );
  }
}
