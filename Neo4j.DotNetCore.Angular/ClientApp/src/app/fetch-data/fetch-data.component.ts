import { Component } from '@angular/core';
import { WeatherForecast } from '../models/weatherforecast.model';
import { DataAccessService } from '../services/data-access.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(private dataAccessService: DataAccessService) {

    dataAccessService.getData().subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

  }
}
