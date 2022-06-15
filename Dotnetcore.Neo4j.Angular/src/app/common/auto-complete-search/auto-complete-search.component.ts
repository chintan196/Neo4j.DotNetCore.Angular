import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms'
import { DataAccessService } from '../../services/data-access.service';
import { NotificationService } from '../../services/notification.service';
import { debounceTime, tap, switchMap, finalize } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

/**
 * Component
 */
@Component({
  selector: 'app-auto-complete-search',
  templateUrl: './auto-complete-search.component.html',
  styleUrls: ['./auto-complete-search.component.scss']
})
export class AutoCompleteSearchComponent implements OnInit {
  @Input() searchApiUrl: string = '';
  @Input() controlLabel: string = '';
  @Input() formFieldClass: string = '';
  
  @Input() value: any = {};
  @Output() valueChange = new EventEmitter<any>();

  searchControl = new FormControl();
  filteredObjects: any;
  isLoading = false;
  errorMsg: string = "";
/**
 * Creates an instance of auto complete search component.
 * @param dataAccessService 
 * @param notificationService 
 */
constructor(private dataAccessService: DataAccessService, private notificationService: NotificationService) { }

/**
 * on init
 */
ngOnInit(): void {
    this.searchControl.valueChanges
      .pipe(
        debounceTime(800),
        tap(() => {
          this.errorMsg = "";
          this.filteredObjects = [];
          this.isLoading = true;
          this.value = (this.searchControl && this.searchControl.value && this.searchControl.value.id) ? this.searchControl.value : { id:"", name: this.searchControl.value };
          this.valueChange.emit(this.value);
        }),
        switchMap((text) => { 
            if(text != undefined && text.length > 2)
            {
              return this.dataAccessService.callSearchApi(this.searchApiUrl + text)
              .pipe(
                finalize(() => {
                  this.isLoading = false;
                })
              );
            }
            else
            {
              this.isLoading = false;
              return new Observable<any>();
            }
          }
        )
      )
      .subscribe(data => {
        if (data== undefined) {
          this.notificationService.showError("There was some problem fetching auto search data.")
          this.filteredObjects = [];
        } else {          
          this.filteredObjects = data;
        }
      });
  }

  /**
   * Clears value
   * @param [event] 
   */
  clearValue(event: any = null)
  {
    this.searchControl.setValue(null);
  }

  /**
   * Determines whether selection changed on
   * @param event 
   */
  onSelectionChanged(event: MatAutocompleteSelectedEvent)
  {
    if(event?.option?.value != null || event?.option?.value != null)
    {
      this.value = event?.option?.value ? { id: event?.option?.value.id, name: event?.option?.value.name } : {};
      this.valueChange.emit(this.value);
    }
  }

  /**
   * Displays fn
   * @param object 
   * @returns fn 
   */
  displayFn(object: any): string {
    return object && object.name ? object.name : '';
  }
}