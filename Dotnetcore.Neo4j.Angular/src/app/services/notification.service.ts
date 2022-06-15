import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

/**
 * Injectable notification service responsible for toast message notifications
 */
@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  /**
   * Creates an instance of notification service.
   * @param toastr 
   */
  constructor(private toastr: ToastrService) { }

  /**
   * Shows success
   * @param message 
   * @param [title] 
   */
  public showSuccess(message: string, title: string= "Success"){
    this.toastr.success(message, title);
  }

  /**
   * Shows warning
   * @param message 
   * @param [title] 
   */
  public showWarning(message: string, title: string = "Warning"){
    this.toastr.warning(message, title);
  }

  /**
   * Shows error
   * @param message 
   * @param [title] 
   */
  public showError(message: string, title: string = "Error"){
    this.toastr.error(message, title);
  }
}