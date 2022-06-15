import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericDataGridComponent } from './generic-data-grid.component';

describe('GenericDataGridComponent', () => {
  let component: GenericDataGridComponent;
  let fixture: ComponentFixture<GenericDataGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenericDataGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenericDataGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
