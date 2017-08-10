var HelloAngular;
(function (HelloAngular) {
    var AMail;
    (function (AMail) {
        var DetailController = (function () {
            function DetailController($scope, $routeParams, messageService) {
                var id = parseInt($routeParams[id]);
                $scope.Message = $.grep(messageService.GetMessages(), function (m) { return m.Id === id; })[0];
            }
            return DetailController;
        }());
        AMail.DetailController = DetailController;
    })(AMail = HelloAngular.AMail || (HelloAngular.AMail = {}));
})(HelloAngular || (HelloAngular = {}));
