import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-new-movie',
  templateUrl: './add-new-movie.component.html',
  styleUrls: ['./add-new-movie.component.scss']
})
export class AddNewMovieComponent implements OnInit {
  /**
   * Creates an instance of add new movie component.
   * @param dialogRef 
   */
   constructor(private dialogRef: MatDialogRef<AddNewMovieComponent>) {
  }
    
    
  /**
   * on init
   */
  ngOnInit(): void {
  }

  /**
   * Closes add new movie component
   */
  close() {
      this.dialogRef.close();
  }

}
