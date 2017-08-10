import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { InMemoryWebApiModule } from "angular2-in-memory-web-api";

import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app.routing";
import { DashboardComponent } from "./dashboard.component";
import { HeroDetailComponent } from "./hero-detail.component";
import { HeroesComponent } from "./heroes.component";
import { HeroSearchComponent } from "./hero-search.component";
import { HeroService } from "./hero.service";
import { InMemoryDataService } from "./in-memory-data.service";

@NgModule({
    imports: [ 
        BrowserModule, 
        FormsModule, 
        AppRoutingModule, 
        HttpModule,
        InMemoryWebApiModule.forRoot(InMemoryDataService)
    ],
    declarations: [ AppComponent, DashboardComponent, HeroDetailComponent, HeroesComponent, HeroSearchComponent ],
    bootstrap: [ AppComponent ],
    providers: [ HeroService ]
})
export class AppModule {
}