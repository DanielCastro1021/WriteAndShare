import { Injectable } from "@angular/core";
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse
} from "@angular/common/http";

import { map } from "rxjs/operators";

const endpoint = "https://localhost:5001/User/api/v1/identity/";
const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json"
  })
};
@Injectable({
  providedIn: "root"
})
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  /**
   * This function makes a http post request to REST API, to login a user.
   * @param username This is a string, with the username of a User.
   * @param password This is a string, with the password of a User.
   */
  login(username: string, password: string) {
    return this.http
      .post<any>(endpoint + "login", { username, password })
      .pipe(
        map(token => {
          // login successful if there's a jwt token in the response
          if (token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem("token", JSON.stringify(token));
          }
          return token;
        })
      );
  }

  /**
   * This function makes a http get request to REST API, to logout a user and removes credentials from localStorage.
   */
  logout() {
    localStorage.removeItem("token");
    return this.http.get<any>(endpoint + "logout");
  }
}
