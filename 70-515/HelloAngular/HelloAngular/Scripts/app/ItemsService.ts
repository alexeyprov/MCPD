module HelloAngular {
    export class ItemsService implements IItemsService {
        constructor($http: ng.IHttpService) {
        }

        public GetItems(): IOrderItem[] {
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
        }
    }
}