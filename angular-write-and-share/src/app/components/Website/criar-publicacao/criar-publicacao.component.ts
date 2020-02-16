import { Component, OnInit, Input } from '@angular/core';
import { Publication } from 'src/app/Models/Publication';

@Component({
  selector: 'app-criar-publicacao',
  templateUrl: './criar-publicacao.component.html',
  styleUrls: ['./criar-publicacao.component.css']
})
export class CriarPublicacaoComponent implements OnInit {

@Input() publicationData: Publication = new Publication();

  logoImage:File=null;
  constructor() { }

  ngOnInit(): void {
  }

  publicar(){

  }
  cancelar(){

  }
 // fileProgress(event: any):void{
   // this.logoImage = event.target.files[0] as File;
 // }
}
