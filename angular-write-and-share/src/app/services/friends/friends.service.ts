import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { User } from "src/app/models/User";
import { retry, catchError } from "rxjs/operators";
const endpoint = "https://localhost:5001/Friends/api/v1/friendslist";
import { JwtHelperService } from "@auth0/angular-jwt";
@Injectable({
  providedIn: "root"
})
export class FriendsService {
  constructor(private http: HttpClient) {}

  /**
   *
   * @param _id
   */
  public getFriends(_id: string): Observable<User[]> {
    return this.http
      .get<User[]>(endpoint + "/" + _id)
      .pipe(retry(1), catchError(this.errorHandler));
  }

  /**
   *
   * @param _id1
   * @param _id2
   */
  public removeFriend(_id1: string, _id2: string): any {
    return this.http
      .delete<any>(endpoint + "/remove/" + _id1 + "/" + _id2)
      .pipe(retry(1), catchError(this.errorHandler));
  }

  private errorHandler(error) {
    let errorMessage = "";
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }

  public getToken(): string {
    return localStorage.getItem("token");
  }
  public decodePayloadJWT(): any {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(this.getToken());
    return decodedToken;
  }
}
