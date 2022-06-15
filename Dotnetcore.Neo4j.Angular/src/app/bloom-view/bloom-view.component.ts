import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { DomSanitizer } from '@angular/platform-browser';
/**
 * Component
 */
@Component({
  selector: 'app-bloom-view',
  templateUrl: './bloom-view.component.html',
  styleUrls: ['./bloom-view.component.scss']
})
export class BloomViewComponent implements OnInit {
  
  title:string;
  url:any;
  
  /**
   * Creates an instance of bloom view component.
   * @param dialogRef 
   * @param data 
   * @param sanitizer 
   */
  constructor(private dialogRef: MatDialogRef<BloomViewComponent>, @Inject(MAT_DIALOG_DATA) data : any, private sanitizer: DomSanitizer) {
      this.title = data.title;
      this.url = this.sanitizer.bypassSecurityTrustResourceUrl(data.url);
  }
    
  /**
   * on init
   */
  ngOnInit(): void {
  }

  /**
   * Closes bloom view component
   */
  close() {
      this.dialogRef.close();
  }
}