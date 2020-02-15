import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit {

  constructor() { }
  
  searchText; 

  count = 0;
  showlog = false;

  favAdd() {
    this.showlog = true;
    return this.count += 1;
  }

  cards = [
    { title: 'Shiba InuShiba Inu', subtitle: 'Dog Breed', src: "https://material.angular.io/assets/img/examples/shiba2.jpg", body: 'teste' },
    { title: 'Shiba InuShiba Inu', subtitle: 'Dog Bree', src: 'https://material.angular.io/assets/img/examples/shiba2.jpg', body: 'Tesste1'},
    {
      title: 'Shiba InuShiba Inu', subtitle: 'Dog Breed', src: 'https://material.angular.io/assets/img/examples/shiba2.jpg', body: 'teste2'
    },
  
  {title:'Jose', subtitle:'cao de caça', src:'https://i.imgur.com/ueMY27J.png', body:'Criação do novo website WriteAndShare tem crescido nas trends'}];

  ngOnInit(): void {
  }

}
