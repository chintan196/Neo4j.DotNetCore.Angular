import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { DataService } from "./data.service";

/**
 * Injectable service for data access
 */
@Injectable({
  providedIn: 'root'
})
export class DataAccessService {
  constructor(private data: DataService) { }

  /**
   * Gets user
   * @returns user 
   */
  getUser(): Observable<any> {
    return this.data
      .get<any>(`user`);
  }

  /**
   * Searchs movies by filter
   * @param filterObject 
   * @returns movies by filter 
   */
  searchMoviesByFilter(filterObject: any): Observable<any> {
    return this.data
      .post<any>(`movies/searchbyfilter`, filterObject);
  }

  /**
   * Gets statistics
   * @returns statistics 
   */
  getStatistics(): Observable<any> {
    return this.data
      .get<any>(`Statistics`);
  }

  /**
   * Calls search api
   * @param url 
   * @returns search api 
   */
  callSearchApi(url: string): Observable<any> {
    return this.data
      .get<any>(url);
  }
}
