import { Injectable } from "@angular/core";
import { InMemoryDbService } from "angular-in-memory-web-api";

@Injectable()
export class InMemoryDataService implements InMemoryDbService {
  public createDb() {
    return {
      heroes: [
        { id: 11, Name: "Mr. Nice" },
        { id: 12, Name: "Narco" },
        { id: 13, Name: "Bombasto" },
        { id: 14, Name: "Celeritas" },
        { id: 15, Name: "Magneta" },
        { id: 16, Name: "RubberMan" },
        { id: 17, Name: "Dynama" },
        { id: 18, Name: "Dr IQ" },
        { id: 19, Name: "Magma" },
        { id: 20, Name: "Tornado" }
      ]
    };
  }
}