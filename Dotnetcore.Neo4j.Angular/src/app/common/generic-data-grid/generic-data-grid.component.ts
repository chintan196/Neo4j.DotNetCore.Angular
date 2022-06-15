import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Required } from '../generic.functions';

/**
 * Component
 */
@Component({
  selector: 'app-generic-data-grid',
  templateUrl: './generic-data-grid.component.html',
  styleUrls: ['./generic-data-grid.component.scss']
})
export class GenericDataGridComponent implements OnInit {

  @Input() maxHeight: number = 250;
  @Input() @Required columns: any[] = [];  
  @Input() @Required data: any[] = [];
  
  @Input() @Required defaultSortColumn: string = '';
  @Input() @Required defaultSortDirection: any;
  
  @Input() pageSize: number = 5;
  @Input() pageSizeOptions: number[] = [5, 10, 20, 50];

  displayedColumns: any[] = [];

  dataSource: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  /**
   * Creates an instance of generic data grid component.
   */
  constructor() {
  }

  /**
   * on init
   */
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.data);
    this.displayedColumns = Object.keys(this.columns).map((key : any) => this.columns[key]);
    this.columns = Object.entries(this.columns);
  }

  /**
   * after view init
   */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
