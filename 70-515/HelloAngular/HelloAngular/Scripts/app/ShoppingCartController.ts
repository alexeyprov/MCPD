module HelloAngular {
    'use strict';

    export class ShoppingCartController {
        static $inject = ["$scope", "items"];

        constructor($scope: IShoppingCartScope, items: IItemsService) {
            //$scope.title = 'ShoppingCartController';
            $scope.Items = items.GetItems();

            $scope.Discount = 0;

            $scope.Remove = (index: number) => {
                $scope.Items.splice(index, 1);
            };

            $scope.GetCartTotal = function() {
                var sum = 0;
                $.each(
                    $scope.Items,
                    (_, i) => sum += i.Price * i.Quantity);
                return sum;
            }

            $scope.GetGrandTotal = function () {
                return $scope.GetCartTotal() - $scope.Discount;
            }

            $scope.$watch($scope.GetCartTotal, this.OnCartTotalChanged);

            this.Activate();
        }

        public Activate() {
        }

        private OnCartTotalChanged(newValue: number, oldValue: number, scope: IShoppingCartScope) {
            scope.Discount = newValue > 100 ? 10 : 0;
        }
    }
}
