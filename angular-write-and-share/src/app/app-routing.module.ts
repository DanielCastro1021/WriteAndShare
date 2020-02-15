import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeaderSidnaveComponent } from './components/Website/header-sidnave/header-sidnave.component';
import { FeedComponent } from './components/Website/feed/feed.component';


const routes: Routes = [
{path:"feed",  component:FeedComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
