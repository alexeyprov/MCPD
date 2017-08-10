import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import "rxjs/add/observable/of";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/switchMap";
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

import { Hero } from "./hero";
import { HeroSearchService } from "./hero-search.service";

@Component ({
    moduleId: module.id,
    selector: "my-hero-search",
    templateUrl: "hero-search.component.html",
    styleUrls: [ "hero-search.component.css" ],
    providers: [ HeroSearchService ]
})
export class HeroSearchComponent implements OnInit {
    private searchTerms: Subject<string>;

    constructor(
        private service: HeroSearchService,
        private router: Router) {
        this.searchTerms = new Subject<string>();
    }

    public Heroes: Observable<Hero[]>;

    public ngOnInit(): void {
        this.Heroes = this.searchTerms
            .debounceTime(300) // wait for 300ms pause in events
            .distinctUntilChanged() // ignore if next search term is same as previous
            .switchMap((term: string) => term ? // switch to new observable each time
                this.service.Search(term) :
                Observable.of<Hero[]>([]))
            .catch(e => {
                console.error("Error while executing hero search", e);
                return Observable.of<Hero[]>([]);
            });
    }

    private OnSearchBoxKeyUp(searchTerm: string): void {
        this.searchTerms.next(searchTerm);
    }

    private OnHeroClicked(hero: Hero): void {
        this.router.navigate(["/detail", hero.id]);
    }
}