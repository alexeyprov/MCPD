import { Component, Input, OnInit } from "@angular/core";
import { Location } from "@angular/common";
import { ActivatedRoute } from "@angular/router";

import { Hero } from "../hero";
import { HeroService } from "../hero.service";

@Component({
  selector: "app-hero-detail",
  templateUrl: "./hero-detail.component.html",
  styleUrls: ["./hero-detail.component.css"]
})
export class HeroDetailComponent implements OnInit {

  constructor(
    private heroService: HeroService,
    private route: ActivatedRoute,
    private location: Location) { 
  }

  public Hero: Hero;

  public ngOnInit() {
    const id = +this.route.snapshot.params.id;
    this.heroService.GetSingleHero(id)
      .subscribe(h => this.Hero = h);
  }

  private Save() {
    this.heroService.UpdateHero(this.Hero)
        .subscribe(() => this.GoBack())
  }

  private GoBack() {
    this.location.back();
  }
}
