import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { map } from "rxjs/operators";
import { Publication } from 'src/app/Models/Publication';
import { Observable } from 'rxjs';

const endpoint = "http://localhost:5001/api/v1/post/"

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
  


}
