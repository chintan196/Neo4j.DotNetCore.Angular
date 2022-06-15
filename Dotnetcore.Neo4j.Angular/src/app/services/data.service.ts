import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { EnvironmentConfig, ENV_CONFIG } from "../environment-config.interface";

/**
 * Injectable data service for http calls
 */
@Injectable({
  providedIn: 'root'
})
export class DataService {
  public apiUrl: string;

  /**
   * Creates an instance of data service.
   * @param config 
   * @param http 
   */
  constructor(@Inject(ENV_CONFIG) private config: EnvironmentConfig, private http: HttpClient) {
    this.apiUrl = `${config.environment.baseUrl}`;
  }

  /**
   * Gets data service
   * @template T 
   * @param path 
   * @returns get 
   */
  get<T>(path: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${path}`);
  }

  /**
   * Posts data service
   * @template T 
   * @param path 
   * @param body 
   * @returns post 
   */
  post<T>(path: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${path}`, body);
  }
}