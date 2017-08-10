angular
    .module("HelloAngularApp", ["ngRoute"])
    .controller("ShoppingCartController", HelloAngular.ShoppingCartController)
    .service("items", HelloAngular.ItemsService);
