module HelloAngular {
    export interface IShoppingCartScope extends ng.IScope {
        Items: IOrderItem[];

        Discount: number;

        Remove(index: number): void;

        GetCartTotal(): number;

        GetGrandTotal(): number;
    }
}