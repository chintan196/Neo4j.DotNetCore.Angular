import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from './notification.service';

/**
 * Injectable error handling service
 */
@Injectable({
  providedIn: 'root'
})
export class HandleErrorService {

  /**
   * Creates an instance of handle error service.
   * @param notificationService 
   */
  constructor(private notificationService: NotificationService) { }

  /**
   * Handles error
   * @param err 
   */
  public handleError (err: HttpErrorResponse) {
    let errorMessage: string;
    let title: string = "Error";
    
    if (err.error instanceof ErrorEvent)
    {
      // A client side error or a network related error occured
      errorMessage = `An error occured: ${err.error.message}`;
    }
    else
    {      
      switch(err.status)
      {
        case 400:
          title = "Bad Request Error";
          break;
        case 401:
          title = "Unauthorized";
          break;
        case 403:
          title = "Insufficient Permission";
          break;
        case 404:
          title = "Not Found";
          break;
        case 412:
          title = "Precondition Failed";
          break;
        case 500:
          title = "Internal Server Error";
          break;
        case 503:
          title = "Service Unavailable";
          break;
        default:
          title = "Unknown Error";
          break;          
      }
      errorMessage = err.error.message ?? `Something went wrong. Please contact administrator.`;
    }
    
    this.notificationService.showError(errorMessage, title);
  }
}