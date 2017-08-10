"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/toPromise");
//import { AllHeroes } from "./mock-heroes";
var HeroService = (function () {
    function HeroService(http) {
        this.http = http;
        this.apiUrl = "app/heroes";
        this.headers = new http_1.Headers({
            "Content-Type": "application/json"
        });
    }
    HeroService.prototype.GetHeroes = function () {
        //return Promise.resolve(AllHeroes);
        return this.http.get(this.apiUrl)
            .toPromise()
            .then(function (r) { return r.json().data; })
            .catch(this.HandleError);
    };
    HeroService.prototype.GetSingleHero = function (id) {
        return this.GetHeroes()
            .then(function (heroes) { return heroes.find(function (h) { return h.id === id; }); });
    };
    HeroService.prototype.UpdateHero = function (hero) {
        return this.http.put(this.apiUrl + "/" + hero.id, JSON.stringify(hero), this.headers)
            .toPromise()
            .then(function () { return hero; })
            .catch(this.HandleError);
    };
    HeroService.prototype.AddHero = function (name) {
        return this.http.post(this.apiUrl, JSON.stringify({
            Name: name
        }), this.headers)
            .toPromise()
            .then(function (e) { return e.json().data; })
            .catch(this.HandleError);
    };
    HeroService.prototype.DeleteHero = function (id) {
        return this.http.delete(this.apiUrl + "/id")
            .toPromise()
            .then(function (e) { return null; })
            .catch(this.HandleError);
    };
    HeroService.prototype.HandleError = function (error) {
        console.error("An error occurred calling Heroes web API", error);
        return Promise.reject(error.message || error);
    };
    HeroService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], HeroService);
    return HeroService;
}());
exports.HeroService = HeroService;
//# sourceMappingURL=hero.service.js.map