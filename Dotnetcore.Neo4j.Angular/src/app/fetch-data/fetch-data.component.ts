import { AfterViewInit, Component, ViewChild, OnInit, Inject} from '@angular/core';
import { Movie } from '../models/movie.model';
import { SearchFilter } from '../models/searchFilter.model';
import { DataAccessService } from '../services/data-access.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, MatSortHeader, Sort } from '@angular/material/sort';
import { EnvironmentConfig, ENV_CONFIG } from "../environment-config.interface";
import { MatDialog , MatDialogConfig} from "@angular/material/dialog";
import { BloomViewComponent } from '../bloom-view/bloom-view.component';
import { AutoCompleteSearchComponent } from '../common/auto-complete-search/auto-complete-search.component';
import { AddNewMovieComponent } from '../add-new-movie/add-new-movie.component';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.scss']
})
export class FetchDataComponent implements OnInit {

  isLoading = false;
  public movies: Movie[] = [];
  dataSource = new MatTableDataSource<Movie>(this.movies);
  displayedColumns: string[] = ['bloomView', 'title', 'released', 'tagLine', 'director', 'producer', 'writer'];

  personSearchUrl = "Persons/SearchByName?searchString=";

  // Paging properties
  totalRows = 0;
  pageSizeOptions: number[] = [25, 50, 100, 200];

  // Bloom URL
  bloomUrl: string = '';

  masterData = {
    personTypes: ["Director","Producer","Writer"]
  };  
  
  filterObject: SearchFilter;
  searchObject: SearchFilter;

  /**
  * View child of fetch data component
  */
  @ViewChild(MatSort, { static: false })sort!: MatSort;

  constructor(@Inject(ENV_CONFIG) private config: EnvironmentConfig, private dataAccessService: DataAccessService, private dialog: MatDialog)
  {
    this.bloomUrl = config.environment.bloomUrl;
  }

  // Paging and sorting related properties
  @ViewChild(MatPaginator, { static: false })paginator!: MatPaginator;
  @ViewChild("personSearch", { static: false })personSearchControl!: AutoCompleteSearchComponent;


  // After view init
  /**
   * after view init
   */
   ngAfterViewInit() {    
    this.paginator.pageIndex = this.filterObject.currentPage;
    this.paginator.length = this.totalRows;
    this.dataSource.paginator = this.paginator;
  }

  // On Init
  /**
   * on init
   */
   ngOnInit(): void {
    this.setInitialFilterObject();
    
    //Load filters
    this.loadFiltersData();

    //Load initial data
    this.loadData();
  }


/**
 * Sets initial filter object
 */
 setInitialFilterObject()
 {
   this.filterObject = {
     title: "",
     released: null,
     person: {},
     personType: "",
     producer: {},
     writer: {},
     director: [],
     currentPage:0,
     pageSize:25,
     sortByField:"released",
     sortOrder:"desc"
   };

   this.searchObject = new SearchFilter(this.filterObject);

   if (this.personSearchControl != null || this.personSearchControl != undefined)
   {
     this.personSearchControl.clearValue(null);
   }
 }

/**
* Sorts data
* @param event 
*/
sortData(event: Sort)
 {
   this.searchObject.sortByField = event.active;
   this.searchObject.sortOrder = event.direction;
   this.loadData();
 }

 /**
  * Pages changed
  * @param event 
  */
 pageChanged(event: PageEvent) {
   this.searchObject.pageSize = event.pageSize;
   this.searchObject.currentPage = event.pageIndex;
   this.loadData();
 }

/**
* Sarchs click event
* @param event 
*/
sarchClickEvent(event: any)
 {
   console.log(this.filterObject);
   console.log(this.searchObject);
   this.searchObject = new SearchFilter(this.filterObject);
   this.loadData();
 }

/**
* Clears click event
* @param event 
*/
clearClickEvent(event: any)
 {
   this.setInitialFilterObject();

   this.sort.sort({ id: '', start: 'desc', disableClear: false });
   this.sort.sort({ id: 'released', start: 'desc', disableClear: false });
   (this.sort.sortables.get('released') as MatSortHeader)._setAnimationTransitionState({ toState: 'active' });

   console.log(this.filterObject);
   console.log(this.searchObject);    
   this.loadData();
 }

 // Main load data method
 /**
  * Loads data
  */
 loadData() {
   this.isLoading = true;
 
   this.dataAccessService.searchMoviesByFilter(this.searchObject).subscribe({
     next: (data) => {
       this.movies = data.data;
       this.totalRows = data.count,
       this.isLoading = false;
     },
     error: (error) => 
     { 
       console.error(error); 
       this.isLoading = false;
     },
   });
 }

   // Method to load filters data
   /**
    * Loads filters data
    */
   loadFiltersData() {    
     this.isLoading = true;   
     // THis can be a server api call
     this.masterData.personTypes = ["Director","Producer","Writer"];
     this.isLoading = false;
   }

  // Method to initialize parameters and open Bloom view 
  /**
   * Opens bloom view
   * @param record 
   */
   openBloomView(record: Movie)
   {
     console.log(record);
 
     var title = record.title;
 
     var searchPhrase = `search=Explore Movie ${title}`;
 
     var url = `${this.bloomUrl}?perspective=UI-Test&run=true&${searchPhrase}`
     
     this.openBloomDialog(record.title, url);
   }
 
   // Method to open bloom view dialog
   /**
    * Opens bloom dialog
    * @param title 
    * @param url 
    */
   openBloomDialog(title: string, url: string) {
     const dialogConfig = this.getDialogConfig();
 
     dialogConfig.data = {
       title: `Visualize movie "${title}" in Neo4j Bloom View`,
       url: url
     };
 
     this.dialog.open(BloomViewComponent, dialogConfig);
   }


  openNewMovieDialog()
  {
    const dialogConfig = this.getDialogConfig();
    this.dialog.open(AddNewMovieComponent, dialogConfig);
  }

  /**
   * Gets dialog config
   * @returns  dielogconfig
   */
  private getDialogConfig() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '50%';
    dialogConfig.minHeight = 'calc(100vh - 400px)';
    dialogConfig.height = 'auto';
    return dialogConfig;
  }

}