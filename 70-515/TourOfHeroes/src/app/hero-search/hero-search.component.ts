import { Component, OnInit } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { debounceTime, distinctUntilChanged, switchMap } from "rxjs/operators";

import { Hero } from "../Hero";
import { HeroService } from "../hero.service";

@Component({
  selector: "app-hero-search",
  templateUrl: "./hero-search.component.html",
  styleUrls: ["./hero-search.component.css"]
})
export class HeroSearchComponent implements OnInit {
  private searchTerms: Subject<string>;

  constructor(private service: HeroService) { 
    this.searchTerms = new Subject<string>();
  }

  public Heroes$: Observable<Hero[]>;

  public ngOnInit(): void {
    this.Heroes$ = this.searchTerms.pipe(
        // wait 300ms after each keystroke before considering the term
        debounceTime(300),

        // ignore new term if same as previous term
        distinctUntilChanged(),

        // switch to new search observable each time the term changes
        switchMap(t => this.service.Search(t))
    );
  }

  private OnSearchBoxKeyUp(searchTerm: string): void {
    this.searchTerms.next(searchTerm);
  }
}
