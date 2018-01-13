import { Injectable } from "@angular/core";
import { Http, Headers } from "@angular/http";
import "rxjs/add/operator/toPromise";

import { Hero } from "./hero";
//import { AllHeroes } from "./mock-heroes";

@Injectable()
export class HeroService {
    private apiUrl: string;
    private http: Http;
    private headers: Headers;

    constructor(http: Http) {
        this.http = http;
        this.apiUrl = "app/heroes";
        this.headers = new Headers({
            "Content-Type": "application/json"
        });
    }

    public GetHeroes(): Promise<Hero[]> {
        //return Promise.resolve(AllHeroes);
        return this.http.get(this.apiUrl)
            .toPromise()
            .then(r => r.json().data as Hero[])
            .catch(this.HandleError);
    }

    public GetSingleHero(id: number): Promise<Hero> {
        return this.GetHeroes()
            .then(heroes => heroes.find(h => h.id === id));
    }

    public UpdateHero(hero: Hero): Promise<Hero> {
        return this.http.put(
                `${this.apiUrl}/${hero.id}`,
                JSON.stringify(hero),
                this.headers)
            .toPromise()
            .then(() => hero)
            .catch(this.HandleError);
    }

    public AddHero(name: string): Promise<Hero> {
        return this.http.post(
                this.apiUrl,
                JSON.stringify({
                    Name: name
                }),
                this.headers)
            .toPromise()
            .then(e => e.json().data)
            .catch(this.HandleError);
    }

    public DeleteHero(id: number): Promise<void> {
        return this.http.delete(`${this.apiUrl}/id`)
            .toPromise()
            .then(e => null)
            .catch(this.HandleError);
    }

    private HandleError(error: any): Promise<any> {
        console.error("An error occurred calling Heroes web API", error);
        return Promise.reject(error.message || error);
    }
}