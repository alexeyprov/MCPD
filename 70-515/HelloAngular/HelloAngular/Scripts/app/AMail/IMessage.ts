module HelloAngular.AMail {
    export interface IMessage {
        Id: number;

        Sender: string;

        Subject: string;

        Date: Date;

        Recipients: string[];

        Body: string;
    }
}