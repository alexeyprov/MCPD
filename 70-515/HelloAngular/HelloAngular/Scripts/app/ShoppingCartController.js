var HelloAngular;
(function (HelloAngular) {
    'use strict';
    var ShoppingCartController = (function () {
        function ShoppingCartController($scope, items) {
            //$scope.title = 'ShoppingCartController';
            $scope.Items = items.GetItems();
            $scope.Discount = 0;
            $scope.Remove = function (index) {
                $scope.Items.splice(index, 1);
            };
            $scope.GetCartTotal = function () {
                var sum = 0;
                $.each($scope.Items, function (_, i) { return sum += i.Price * i.Quantity; });
                return sum;
            };
            $scope.GetGrandTotal = function () {
                return $scope.GetCartTotal() - $scope.Discount;
            };
            $scope.$watch($scope.GetCartTotal, this.OnCartTotalChanged);
            this.Activate();
        }
        ShoppingCartController.prototype.Activate = function () {
        };
        ShoppingCartController.prototype.OnCartTotalChanged = function (newValue, oldValue, scope) {
            scope.Discount = newValue > 100 ? 10 : 0;
        };
        ShoppingCartController.$inject = ["$scope", "items"];
        return ShoppingCartController;
    }());
    HelloAngular.ShoppingCartController = ShoppingCartController;
})(HelloAngular || (HelloAngular = {}));
