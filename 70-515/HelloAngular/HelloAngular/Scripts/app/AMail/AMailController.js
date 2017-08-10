var HelloAngular;
(function (HelloAngular) {
    var AMail;
    (function (AMail) {
        angular.module("HelloAngularApp", [])
            .controller("AMailController", AMailController);
        var AMailController = (function () {
            function AMailController($scope) {
                $scope.Messages = [
                    {
                        Id: 0,
                        Sender: "jean@somecompany.com",
                        Subject: "Hi there, old friend",
                        Date: new Date(2013, 12, 7, 12, 32),
                        Recipients: ["greg@somecompany.com"],
                        Body: "Hey, we should get together for lunch sometime and catch up."
                            + "There are many things we should collaborate on this year."
                    }, {
                        Id: 1,
                        Sender: "maria@somecompany.com",
                        Subject: "Where did you leave my laptop?",
                        Date: new Date(2013, 12, 7, 8, 15, 12),
                        Recipients: ["greg@somecompany.com"],
                        Body: "I thought you were going to put it in my desk drawer."
                            + "But it does not seem to be there."
                    }, {
                        Id: 2,
                        Sender: "bill@somecompany.com",
                        Subject: "Lost python",
                        Date: new Date(2013, 12, 6, 20, 35, 2),
                        Recipients: ["greg@somecompany.com"],
                        Body: "Nobody panic, but my pet python is missing from her cage."
                            + "She doesn't move too fast, so just call me if you see her."
                    }
                ];
            }
            return AMailController;
        }());
        AMail.AMailController = AMailController;
    })(AMail = HelloAngular.AMail || (HelloAngular.AMail = {}));
})(HelloAngular || (HelloAngular = {}));
