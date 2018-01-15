import { Injectable } from "@angular/core";

@Injectable()
export class MessagesService {

  constructor() {
    this.Messages = []; 
  }

  public Messages: string[];

  public AddMessage(message: string) {
      this.Messages.push(message);
  }

  public Clear() {
      this.Messages = [];
  }
}
