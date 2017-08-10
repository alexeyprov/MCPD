var HelloAngular;
(function (HelloAngular) {
    var ItemsService = (function () {
        function ItemsService($http) {
        }
        ItemsService.prototype.GetItems = function () {
            return [
                {
                    Name: "Paint pots",
                    Quantity: 8,
                    Price: 3.95
                },
                {
                    Name: "Polka dots",
                    Quantity: 17,
                    Price: 12.95
                },
                {
                    Name: "Pebbles",
                    Quantity: 5,
                    Price: 6.95
                }
            ];
        };
        return ItemsService;
    }());
    HelloAngular.ItemsService = ItemsService;
})(HelloAngular || (HelloAngular = {}));
