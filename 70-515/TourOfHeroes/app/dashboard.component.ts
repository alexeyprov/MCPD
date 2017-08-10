import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Hero } from "./hero";
import { HeroService } from "./hero.service";

@Component({
    moduleId: module.id,
    selector: "my-dashboard",
    templateUrl: "dashboard.component.html",
    styleUrls: [ "dashboard.component.css" ]
})
export class DashboardComponent implements OnInit {
    private heroService: HeroService;
    private router: Router;

    constructor(heroService: HeroService, router: Router) {
        this.heroService = heroService;
        this.router = router;
    }

    public Heroes: Hero[];

    public ngOnInit() {
        this.heroService.GetHeroes()
            .then(h => this.Heroes = h.slice(1, 5));
    }

    public GetHeroDetails(hero: Hero) {
        let link = ["detail", hero.id];
        this.router.navigate(link);
    }
}