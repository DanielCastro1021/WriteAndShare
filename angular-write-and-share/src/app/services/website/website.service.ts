import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Publication } from 'src/app/Models/Publication';
import { Observable } from 'rxjs';
import { JwtHelperService } from "@auth0/angular-jwt"
import { Token } from "src/app/Models/Token"

const endpoint = "http://localhost:5001/Post/api/v1/post/"

const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json"
  })
};

@Injectable({
  providedIn: 'root'
})

export class WebsiteService {
  
  constructor(private http: HttpClient) { }

  getposts(): Observable<Publication[]> {
    return this.http
      .get<Publication[]>(endpoint + "posts");

  }

  createpost(publication: Publication, userId): any {
    return this.http
      .post<any>(endpoint + { userId } + "/newpost", { publication });
  }

  decodeToken(): Token {

    const helper = new JwtHelperService();

    const decodeToken = helper.decodeToken(localStorage.getItem("token"));
console.log(decodeToken)
    const user_token = new Token();

    user_token.unique_name = decodeToken.unique_name;
    user_token.nameId = decodeToken.nameid;
    user_token.email = decodeToken.email;

    return user_token;

  }


}
