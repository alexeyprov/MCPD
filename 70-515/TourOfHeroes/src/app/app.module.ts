import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { InMemoryWebApiModule } from "angular-in-memory-web-api";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { HeroDetailComponent } from "./hero-detail/hero-detail.component";
import { HeroesComponent } from "./heroes/heroes.component";
import { HeroSearchComponent } from "./hero-search/hero-search.component";
import { HeroService } from "./hero.service";
import { InMemoryDataService } from "./in-memory-data.service";
import { MessagesComponent } from "./messages/messages.component";
import { MessagesService } from "./messages.service";

@NgModule({
  declarations: [
    AppComponent,
    HeroesComponent,
    HeroDetailComponent,
    MessagesComponent,
    DashboardComponent,
    HeroSearchComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    InMemoryWebApiModule.forRoot(InMemoryDataService)
  ],
  bootstrap: [ AppComponent ],
  providers: [ HeroService, MessagesService, InMemoryDataService ]
})
export class AppModule { 
}