import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FeedComponent } from './components/Website/feed/feed.component';
import { CriarPublicacaoComponent } from './components/Website/criar-publicacao/criar-publicacao.component';


const routes: Routes = [
{path:'feed',  component:FeedComponent},{path:'criar-publicacao', component:CriarPublicacaoComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
