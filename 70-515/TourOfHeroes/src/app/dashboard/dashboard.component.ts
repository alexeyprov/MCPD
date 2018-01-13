import { Component, OnInit } from "@angular/core";

import { Hero } from "../hero";
import { HeroService } from "../hero.service";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.css"]
})
export class DashboardComponent implements OnInit {

  constructor(private heroService: HeroService) {
  }

  public Heroes: Hero[];

  public ngOnInit() {
    this.heroService.GetHeroes()
		.subscribe(h => this.Heroes = h.slice(1, 5));
  }
}