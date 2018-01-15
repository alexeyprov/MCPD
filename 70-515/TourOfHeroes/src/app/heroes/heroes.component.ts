import { Component, OnInit } from "@angular/core";

import { Hero } from "../hero";
import { HeroService } from "../hero.service";

@Component({
  selector: "app-heroes",
  templateUrl: "./heroes.component.html",
  styleUrls: ["./heroes.component.css"]
})
export class HeroesComponent implements OnInit {

  constructor(private heroService: HeroService) { 
  }

  public Heroes: Hero[];

  public ngOnInit() {
    this.heroService.GetHeroes()
		.subscribe(heroes => this.Heroes = heroes);
  }

  private OnAddHeroClicked(name: string) {
    name = name.trim();
    if (!name) {
        return;
    }

    this.heroService.AddHero(name)
        .subscribe(h => this.Heroes.push(h));
  }

  private OnDeleteHeroClicked(hero: Hero) {
    this.heroService.DeleteHero(hero.id)
		.subscribe(() => 
			this.Heroes = this.Heroes.filter(h => h !== hero));
  }
}