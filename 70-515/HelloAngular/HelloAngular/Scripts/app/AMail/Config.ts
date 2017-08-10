module HelloAngular.AMail {
    angular.module("HelloAngularApp", ["ngRoute"])
        .service("messageService", MessagesService)
        .config(ConfigureRoutes);
        

    function ConfigureRoutes($routeProvider: ng.route.IRouteProvider) {
        $routeProvider
            .when(
                "/",
                {
                    controller: ListController,
                    templateUrl: "/Templates/AMail/List.cshtml"
                })
            .when(
                "/view/:id",
                {
                    controller: DetailController,
                    templateUrl: "/Templates/AMail/Detail.cshtml"
                })
            .otherwise(
                {
                    redirectTo: "/"
                });
    }
}