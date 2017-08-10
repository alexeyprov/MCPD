import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Hero } from "./hero";
import { HeroService } from "./hero.service";

@Component({
    moduleId: module.id,
    selector: "my-heroes",
    templateUrl: "heroes.component.html",
    styleUrls: [ "heroes.component.css" ]
})
export class HeroesComponent implements OnInit {
    private heroService: HeroService;
    private router: Router;
    
    constructor(heroService: HeroService, router: Router) {
        this.heroService = heroService;
        this.router = router;
    }

    public SelectedHero: Hero;
    public Heroes: Hero[];

    public ngOnInit() {
        this.heroService.GetHeroes()
            .then<Hero[]>(heroes => this.Heroes = heroes);
    }

    private OnHeroClicked(hero: Hero) {
        this.SelectedHero = hero;
    }

    private OnViewDetailsClicked() {
        this.router.navigate(["detail", this.SelectedHero.id]);
    }

    private OnAddHeroClicked(name: string) {
        name = name.trim();
        if (!name) {
            return;
        }

        this.heroService.AddHero(name)
            .then(h => {
                this.Heroes.push(h);
                this.SelectedHero = null;
            });
    }

    private OnDeleteHeroClicked(hero: Hero) {
        this.heroService.DeleteHero(hero.id)
            .then(() => {
                this.Heroes = this.Heroes.filter(h => h !== hero);
                if (this.SelectedHero === hero) {
                    this.SelectedHero = null;
                }
            });
    }
}