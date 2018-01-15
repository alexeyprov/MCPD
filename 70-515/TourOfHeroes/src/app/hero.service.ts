import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { of } from "rxjs/observable/of";
import { catchError, map, tap } from "rxjs/operators";

import { Hero } from "./hero";
import { MessagesService } from "./messages.service";

@Injectable()
export class HeroService {
  private apiUrl: string;
  private httpOptions;

  constructor(private messageService: MessagesService,
              private http: HttpClient) { 
      this.apiUrl = "api/heroes";
      this.httpOptions = {
        headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
      };
  }

  public GetHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.apiUrl).pipe(
        tap(_ => this.Log("Hero list loaded")),
        catchError(this.CreateSelector("GetHeroes", <Hero[]>[])));
  }

  public GetSingleHero(id: number): Observable<Hero> {
    return this.http.get<Hero>(`${this.apiUrl}/${id}`).pipe(
        tap(_ => this.Log(`Hero ${id} loaded`)),
        catchError(this.CreateSelector<Hero>("GetHero", undefined)));
  }

  public UpdateHero(hero: Hero): Observable<any> {
    return this.http.put<Hero>(
			this.apiUrl,
			hero,
			this.httpOptions)
		.pipe(
        	tap(_ => this.Log(`Hero ${hero.id} updated`)),
        	catchError(this.CreateSelector("UpdateHero", hero)));
  }

  public AddHero(name: string): Observable<Hero> {
    return this.http.post<Hero>(
			this.apiUrl, 
			{
				Name: name
			}, 
			this.httpOptions)
		.pipe(
        	tap(_ => this.Log("Hero added")),
        	catchError(this.CreateSelector("AddHero", undefined)));
  }

  public DeleteHero(id: number): Observable<any> {
    return this.http.delete<Hero>(`${this.apiUrl}/${id}`)
		.pipe(
        	tap(_ => this.Log(`Hero ${id} deleted`)),
        	catchError(this.CreateSelector("DeleteHero")));
  }

  public Search(term: string): Observable<Hero[]> {
    term = term.trim();
    if (!term) {
        return of([]);
    }
    return this.http.get<Hero[]>(`${this.apiUrl}/?Name=${term}`).pipe(
        tap(a => this.Log(`Search found ${a.length} hero(es)`)),
        catchError(this.CreateSelector<Hero[]>("Search", [])));
  }


  private Log(message: string) {
    this.messageService.AddMessage(`HeroService: ${message}`);
  }

  private CreateSelector<T>(methodName: string, result?: T) {
    return e => {
        console.error(e);
        this.Log(`${methodName} failed: ${e.message || e}`);
        return of(result);
    }
  }
}