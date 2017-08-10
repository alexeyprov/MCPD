var HelloAngular;
(function (HelloAngular) {
    var AMail;
    (function (AMail) {
        angular.module("HelloAngularApp", ["ngRoute"])
            .service("messageService", AMail.MessagesService)
            .config(ConfigureRoutes);
        function ConfigureRoutes($routeProvider) {
            $routeProvider
                .when("/", {
                controller: AMail.ListController,
                templateUrl: "/Templates/AMail/List.cshtml"
            })
                .when("/view/:id", {
                controller: AMail.DetailController,
                templateUrl: "/Templates/AMail/Detail.cshtml"
            })
                .otherwise({
                redirectTo: "/"
            });
        }
    })(AMail = HelloAngular.AMail || (HelloAngular.AMail = {}));
})(HelloAngular || (HelloAngular = {}));
