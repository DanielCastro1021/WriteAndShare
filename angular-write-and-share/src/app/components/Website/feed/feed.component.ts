import { Component, OnInit } from '@angular/core';
import { WebsiteService } from 'src/app/services/website/website.service';
import { Publication } from 'src/app/Models/Publication';


@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

  publicacoes:Publication[];

  constructor(private Service: WebsiteService) { }
  

  ngOnInit(): void {
   this.Service.getposts().subscribe((posts:Publication[])=> this.publicacoes = posts);
  }

  searchText; 

  count = 0;
  showlog = false;

  favAdd() {
    this.showlog = true;
    return this.count += 1;
  }

  

}
