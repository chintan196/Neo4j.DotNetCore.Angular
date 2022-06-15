import { Component, OnInit, ViewChild } from '@angular/core';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { DataAccessService } from '../services/data-access.service';
/**
 * Component
 */
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  isLoading = false;
  breakpoint: any;
  moviesCount:number = 0;
  personsCount:number = 0;
  lineChartColSpan: number = 0;
  pieChartColSpan: number = 0;
  cardRowHeight: string = "385px";

/**
 * Creates an instance of dashboard component.
 * @param dataAccessService 
 */
constructor(private dataAccessService: DataAccessService) {}

  /**
   * on init
   */
  ngOnInit() {
    this.resizeGrid();
    this.loadData();
  }

  /**
   * Loads data
   */
  loadData() {
    this.isLoading = true;
  
    this.dataAccessService.getStatistics().subscribe(
      {
        next: (data: any) => {
          this.moviesCount = data?.Movies ?? 0;
          this.personsCount = data?.Actors ?? 0;
    
          this.isLoading = false;
        }, 
        error: (error) => 
        { 
          console.error(error); 
          this.isLoading = false;
        }
      }
    );
  }

  /**
   * Determines whether resize on
   * @param event 
   */
  onResize(event: any) {
    this.resizeGrid();
  }

  /**
   * Resizes grid
   */
  resizeGrid()
  {
    if(window.innerWidth <= 1200)
    {
      this.breakpoint = 1;
      this.lineChartColSpan = 1;
      this.pieChartColSpan = 1;
    }
    else
    {
      this.breakpoint = 4;
      this.lineChartColSpan = 3;
      this.pieChartColSpan = 3;
    }
  }
}
