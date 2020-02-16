import { Component, OnInit, Input } from '@angular/core';
import { Publication } from 'src/app/Models/Publication';
import { WebsiteService } from 'src/app/services/website/website.service';
import { Router } from '@angular/router';
import { Token } from 'src/app/Models/Token'

@Component({
  selector: 'app-criar-publicacao',
  templateUrl: './criar-publicacao.component.html',
  styleUrls: ['./criar-publicacao.component.css']
})
export class CriarPublicacaoComponent implements OnInit {

  @Input() publicationData: Publication = new Publication();

  userId = null;
  token:Token;
  logoImage: File = null;
  constructor(private webserviceCreate: WebsiteService, public router: Router) { }

  ngOnInit(): void {
    this.token= new Token();
    this.token= this.webserviceCreate.decodeToken();
    this.userId= this.token.nameId;
  }

  publicar() {
    console.log(this.publicationData);
    this.webserviceCreate.createpost(this.publicationData, this.userId);
    this.router.navigate(["/feed"]);
  }
  cancelar() {
    this.router.navigate(["/feed"]);
  }
  // fileProgress(event: any):void{
  // this.logoImage = event.target.files[0] as File;
  // }

}
