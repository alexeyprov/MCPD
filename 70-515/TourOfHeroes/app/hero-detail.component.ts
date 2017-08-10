import { Component, Input, OnInit } from "@angular/core";
import { Location } from "@angular/common";
import { ActivatedRoute, Params } from "@angular/router";

import { Hero } from "./hero";
import { HeroService } from "./hero.service";

@Component({
    moduleId: module.id,
    selector: "my-hero-detail",
    templateUrl: "hero-detail.component.html",
    styleUrls: [ "hero-detail.component.css" ]
})
export class HeroDetailComponent implements OnInit {
    private route: ActivatedRoute;
    private location: Location;
    private heroService: HeroService;

    constructor(route: ActivatedRoute, location: Location, heroService: HeroService) {
        this.route = route;
        this.location = location;
        this.heroService = heroService;
    }

    @Input()
    public Hero: Hero;

    public ngOnInit() {
        this.route.params.forEach(
            (params: Params) => {
                let id = +params["id"];
                this.heroService.GetSingleHero(id)
                    .then(h => this.Hero = h);
            });
    }

    private Save() {
        this.heroService.UpdateHero(this.Hero)
            .then(() => this.GoBack());
    }

    private GoBack() {
        this.location.back();
    }
}