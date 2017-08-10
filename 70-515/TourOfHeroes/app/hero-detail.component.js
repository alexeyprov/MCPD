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
var common_1 = require("@angular/common");
var router_1 = require("@angular/router");
var hero_1 = require("./hero");
var hero_service_1 = require("./hero.service");
var HeroDetailComponent = (function () {
    function HeroDetailComponent(route, location, heroService) {
        this.route = route;
        this.location = location;
        this.heroService = heroService;
    }
    HeroDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var id = +params["id"];
            _this.heroService.GetSingleHero(id)
                .then(function (h) { return _this.Hero = h; });
        });
    };
    HeroDetailComponent.prototype.Save = function () {
        var _this = this;
        this.heroService.UpdateHero(this.Hero)
            .then(function () { return _this.GoBack(); });
    };
    HeroDetailComponent.prototype.GoBack = function () {
        this.location.back();
    };
    __decorate([
        core_1.Input(), 
        __metadata('design:type', hero_1.Hero)
    ], HeroDetailComponent.prototype, "Hero", void 0);
    HeroDetailComponent = __decorate([
        core_1.Component({
            moduleId: module.id,
            selector: "my-hero-detail",
            templateUrl: "hero-detail.component.html",
            styleUrls: ["hero-detail.component.css"]
        }), 
        __metadata('design:paramtypes', [router_1.ActivatedRoute, common_1.Location, hero_service_1.HeroService])
    ], HeroDetailComponent);
    return HeroDetailComponent;
}());
exports.HeroDetailComponent = HeroDetailComponent;
//# sourceMappingURL=hero-detail.component.js.map