import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailedProductComponent } from './detailed-product.component';

describe('DetailedProductComponent', () => {
  let component: DetailedProductComponent;
  let fixture: ComponentFixture<DetailedProductComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetailedProductComponent]
    });
    fixture = TestBed.createComponent(DetailedProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
