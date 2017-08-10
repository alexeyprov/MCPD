module HelloAngular.AMail {
    export class DetailController {
        constructor(
            $scope: IDetailScope,
            $routeParams: ng.route.IRouteParamsService,
            messageService: IMessagesService) {

            var id = parseInt($routeParams[id]);
            $scope.Message = $.grep(
                messageService.GetMessages(),
                m => m.Id === id)[0];
        }
    }
}
