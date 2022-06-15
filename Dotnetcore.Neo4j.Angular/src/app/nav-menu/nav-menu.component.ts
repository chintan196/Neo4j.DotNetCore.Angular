import { Component, OnInit } from '@angular/core';
import { DataAccessService } from '../services/data-access.service';

/**
 * Component
 */
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  /**
   * Creates an instance of nav menu component.
   * @param dataAccessService 
   */
  constructor(private dataAccessService: DataAccessService)
  {}

  user: any = {};

  isExpanded = false;

  /**
   * Collapses nav menu component
   */
  collapse() {
    this.isExpanded = false;
  }

  /**
   * Toggles nav menu component
   */
  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  /**
   * on init
   */
  ngOnInit(): void {
    this.dataAccessService.getUser().subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (error) => 
      { 
        console.error(error);
      },
    });
  }
  
}