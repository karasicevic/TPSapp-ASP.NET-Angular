import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlaceFormComponent } from './place-form.component';

describe('PlaceFormComponent', () => {
  let component: PlaceFormComponent;
  let fixture: ComponentFixture<PlaceFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PlaceFormComponent]
    });
    fixture = TestBed.createComponent(PlaceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
