import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTableModule } from "@angular/material/table";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input"
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list'; 
import { MatTabsModule } from '@angular/material/tabs';

import { LayoutModule } from '@angular/cdk/layout';
import { NgChartsModule } from 'ng2-charts';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { HttpModule } from './http.module';
import { environment } from '../environments/environment';
import { ServerRequestInterceptor } from './server-requests.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BloomViewComponent } from './bloom-view/bloom-view.component';
import { AutoCompleteSearchComponent } from './common/auto-complete-search/auto-complete-search.component';
import { GenericDataGridComponent } from './common/generic-data-grid/generic-data-grid.component';
import { AddNewMovieComponent } from './add-new-movie/add-new-movie.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DashboardComponent,
    FetchDataComponent,
    BloomViewComponent,
    AutoCompleteSearchComponent,
    GenericDataGridComponent,
    AddNewMovieComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ToastrModule.forRoot({
      timeOut: 5000
    }),
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpModule.forRoot({ environment }),
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatDialogModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    LayoutModule,
    NgChartsModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatListModule,
    MatTabsModule
  ],
  providers: [ { provide:  HTTP_INTERCEPTORS, useClass: ServerRequestInterceptor, multi: true } ],
  bootstrap: [AppComponent]
})
export class AppModule { }