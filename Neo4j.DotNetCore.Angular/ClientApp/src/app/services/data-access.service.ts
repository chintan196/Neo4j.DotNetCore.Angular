import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { WeatherForecast } from "../models/weatherforecast.model";
import { DataService } from "./data.service";

@Injectable({
  providedIn: 'root'
})

export class DataAccessService {
  constructor(private data: DataService) { }

  getData(): Observable<WeatherForecast[]> {
    return this.data
      .get<WeatherForecast[]>(`weatherforecast`);
  }
}
